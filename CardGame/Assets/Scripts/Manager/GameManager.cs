using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

public class GameManager : MonoBehaviour
{
	protected static bool initialize = false;
	private List<string> downloadFiles = new List<string> ();

	/// <summary>
	/// 初始化游戏管理器
	/// </summary>
	void Awake ()
	{
		Init ();
	}

	/// <summary>
	/// 初始化
	/// </summary>
	void Init ()
	{
		if (AppConst.ExampleMode) {
			InitGui ();
		}

		CheckExtractResource (); //释放资源
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		Application.targetFrameRate = AppConst.GameFrameRate;
	}

	/// <summary>
	/// 初始化GUI
	/// </summary>
	public void InitGui ()
	{

	}

	/// <summary>
	/// 释放资源
	/// </summary>
	public void CheckExtractResource ()
	{
//			Debugger.LogError ("zys datapath is  = "+ Util.DataPath);
		bool isExists = Directory.Exists (Util.DataPath) &&
		                          Directory.Exists (Util.DataPath + "lua/") && File.Exists (Util.DataPath + "files.txt");
		//zys  存在文件配置 或者 打开了调试模式 开始更新资源流程;
		if (isExists || AppConst.DebugMode) {
//				Debugger.LogError (" zys 开始更新 ");
			StartCoroutine (OnUpdateResource ());
//				Debugger.LogError (" zys 更新 协同 返回 ");
			return;   //文件已经解压过了，自己可添加检查文件列表逻辑
		}
//			Debugger.LogError (" zys 开始释放资源 ");
		StartCoroutine (OnExtractResource ());    //启动释放协成 
	}

	IEnumerator OnExtractResource ()
	{
		string dataPath = Util.DataPath;  //数据目录
		string resPath = Util.AppContentPath (); //游戏包资源目录

		if (Directory.Exists (dataPath))
			Directory.Delete (dataPath, true);
		Directory.CreateDirectory (dataPath);

		string infile = resPath + "files.txt";
		string outfile = dataPath + "files.txt";
		if (File.Exists (outfile))
			File.Delete (outfile);

		string message = "正在解包文件:>files.txt";
		Debug.Log (message);


		if (Application.platform == RuntimePlatform.Android) {
			WWW www = new WWW (infile);
			yield return www;

			if (www.isDone) {
				File.WriteAllBytes (outfile, www.bytes);
			}
			yield return 0;
		} else
			File.Copy (infile, outfile, true);
		yield return new WaitForEndOfFrame ();

		//释放所有文件到数据目录
		string[] files = File.ReadAllLines (outfile);
		foreach (var file in files) {
			string[] fs = file.Split ('|');
			infile = resPath + fs [0];  //
			outfile = dataPath + fs [0];

			message = "正在解包文件:>" + fs [0];
			Debug.Log ("正在解包文件:>" + infile);
	

			string dir = Path.GetDirectoryName (outfile);
			if (!Directory.Exists (dir))
				Directory.CreateDirectory (dir);

			if (Application.platform == RuntimePlatform.Android) {
				WWW www = new WWW (infile);
				yield return www;

				if (www.isDone) {
					File.WriteAllBytes (outfile, www.bytes);
				}
				yield return 0;
			} else {
				if (File.Exists (outfile)) {
					File.Delete (outfile);
				}
				File.Copy (infile, outfile, true);
			}
			yield return new WaitForEndOfFrame ();
		}
		message = "解包完成!!!";


		yield return new WaitForSeconds (0.1f);
		message = string.Empty;

		//释放完成，开始启动更新资源
		StartCoroutine (OnUpdateResource ());
	}

	/// <summary>
	/// 启动更新下载，这里只是个思路演示，此处可启动线程下载更新
	/// </summary>
	IEnumerator OnUpdateResource ()
	{
		downloadFiles.Clear ();
		Debug.LogError ("zys ------->>>>-------- 开始更新资源");
		//zys 关闭了更新模式直接 去更新完成后的方法 ;
		if (!AppConst.UpdateMode) {
			ResourceManager.Instance().initialize (OnResourceInited);
			yield break;
		}
		Debug.LogError ("zys ------->>>>--------  准备向服务器取");
		string dataPath = Util.DataPath;  //数据目录
		string url = AppConst.WebUrl;
		string random = DateTime.Now.ToString ("yyyymmddhhmmss");
		string listUrl = url + "files.txt?v=" + random;
		Debug.LogWarning ("LoadUpdate---->>>" + listUrl);

		WWW www = new WWW (listUrl);
		yield return www;
		if (www.error != null) {
			OnUpdateFailed (string.Empty);
			yield break;
		}
		if (!Directory.Exists (dataPath)) {
			Directory.CreateDirectory (dataPath);
		}
		File.WriteAllBytes (dataPath + "files.txt", www.bytes);

		string filesText = www.text;
		string[] files = filesText.Split ('\n');

		string message = string.Empty;
		for (int i = 0; i < files.Length; i++) {
			if (string.IsNullOrEmpty (files [i]))
				continue;
			string[] keyValue = files [i].Split ('|');
			string f = keyValue [0];
			string localfile = (dataPath + f).Trim ();

			string path = Path.GetDirectoryName (localfile);
			if (!Directory.Exists (path)) {
				Directory.CreateDirectory (path);
			}
			string fileUrl = url + keyValue [0] + "?v=" + random;
			bool canUpdate = !File.Exists (localfile);
			if (!canUpdate) {
				string remoteMd5 = keyValue [1].Trim ();
				string localMd5 = Util.md5file (localfile);
				canUpdate = !remoteMd5.Equals (localMd5);
				if (canUpdate)
					File.Delete (localfile);
			}
			if (canUpdate) {   //本地缺少文件
				Debug.Log (fileUrl);
				message = "downloading>>" + fileUrl; 
				/*
                    www = new WWW(fileUrl); yield return www;
                    if (www.error != null) {
                        OnUpdateFailed(path);   //
                        yield break;
                    }
                    File.WriteAllBytes(localfile, www.bytes);
                     * */
				//这里都是资源文件，用线程下载
				BeginDownload (fileUrl, localfile);
				while (!(IsDownOK (localfile))) {
					yield return new WaitForEndOfFrame ();
				}
			}
		}
		yield return new WaitForEndOfFrame ();
		message = "更新完成!!";
		Debug.LogError ("zys ------->>>>--------   资源更新完成");
		ResourceManager.Instance().initialize (OnResourceInited);
	}

	/// <summary>
	/// 是否下载完成
	/// </summary>
	bool IsDownOK (string file)
	{
		return downloadFiles.Contains (file);
	}

	/// <summary>
	/// 线程下载
	/// </summary>
	void BeginDownload (string url, string file)
	{     //线程下载
		object[] param = new object[2] { url, file };

		ThreadEvent ev = new ThreadEvent ();
		ev.Key = NotiConst.UPDATE_DOWNLOAD;
		ev.evParams.AddRange (param);
		ThreadManager.Instance() .AddEvent (ev, OnThreadCompleted);   //线程下载
	}

	/// <summary>
	/// 线程完成
	/// </summary>
	/// <param name="data"></param>
	void OnThreadCompleted (NotiData data)
	{
//		switch (data.evName) {
//		case NotiConst.UPDATE_EXTRACT:  //解压一个完成
//                    //
//			break;
//		case NotiConst.UPDATE_DOWNLOAD: //下载一个完成
//			downloadFiles.Add (data.evParam.ToString ());
//			break;
//		}
	}

	void OnUpdateFailed (string file)
	{
		string message = "更新失败!>" + file;
//		facade.SendMessageCommand (NotiConst.UPDATE_MESSAGE, message);
	}

	/// <summary>
	/// 资源初始化结束
	/// </summary>
	public void OnResourceInited ()
	{
		initialize = true;                          //初始化完 

//		WarModel.Instance ().DataInitDone ();
	}



	/// <summary>
	/// 析构函数
	/// </summary>
	void OnDestroy ()
	{

		Debug.Log ("~GameManager was destroyed");
	}
}
