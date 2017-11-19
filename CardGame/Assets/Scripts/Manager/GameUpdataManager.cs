using System;
using System.Collections;
using UnityEngine;
using System.IO;
using ZTools;
using AssetBundles;

/// <summary>
/// Game updata manager.
/// 游戏更新管理控制;
/// UI 与逻辑合在一起;
/// 
/// 连接服务器，看版本一致不;
/// 监测资源，更新资源;
/// </summary>
public class GameUpdataManager : PreCtrlBase
{
	public GameUpdataManager ()
	{
	}

	public override void StarLoadData(params object[] values){
		//不注视掉 有个 还没结束就执行回掉 的bug
		//		if(EndLoadCall != null)
		//		{
		//			EndLoadCall ();
		//		}
	}

	void Awake ()
	{
		
	}
	// Use this for initialization
	void Start ()
	{
		//Debuger.LogError ("zys  loadGameUpdata mag");
		LoadRes ();
		LoadXML ();
		StartCoroutine(LoadData() );
//		LoadData ();
//		GameEnterModel.Instance ().GetClientGameParam ();
	}

	AssetBundle assetbundle = null;

	void LoadRes ()
	{
		
	}


	void LoadXML ()
	{

	}

	IEnumerator  LoadData ()
	{
		yield return StartCoroutine (LoadMainiFast ()); 

		yield return new WaitForEndOfFrame ();

		string saveZipPath = Application.persistentDataPath + "/Data/Data."+LzmaTools.zipName;
		string spriteDir = Application.persistentDataPath + "/Data";
		if (!Directory.Exists (spriteDir)) {
			Directory.CreateDirectory (spriteDir);
		}

		string zipPath = Application.streamingAssetsPath + "/Data/Data."+LzmaTools.zipName;
		Debuger.Log ("zys  get  zip");

		// Don't destroy this gameObject as we depend on it to run the loading script.
//		DontDestroyOnLoad(gameObject);
		yield return  StartCoroutine (CopZIPFile (zipPath, saveZipPath));


		if (EndLoadCall != null) {
			EndLoadCall ();
		}
	}

	IEnumerator CopZIPFile (string filePath, string saveZipPath)
    {
        string fielURLPath = "" + filePath;

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        File.Copy (filePath, saveZipPath, true);
//		yield return null;

#else
#if UNITY_IPHONE
        fielURLPath = "file://"+ filePath;
#endif
		//Debuger.LogError ("zys  start  CopZIPFile "+ filePath);
        
		WWW www = new WWW(fielURLPath);
		yield return www;

		FileStream oneZip = File.Create (saveZipPath);
		//Debuger.LogError ("zys    CopZIPFile Length " + www.bytes.Length);
		oneZip.Write (www.bytes, 0, www.bytes.Length);
		oneZip.Close ();
		//Debuger.LogError ("zys  get  CopZIPFile");
#endif

        //Debuger.LogError ("zys UnCommonZip fun will ;do");
		bool bUnzipDone = UnCommonZip (saveZipPath);

		Debuger.LogError("zys Unzip is " + bUnzipDone);

        if (bUnzipDone) {
//			MsgSet.loadDataFile ();
//			RoleSet.loadDataFile ();
//			ItemsSet.loadDataFile ();
//			PlayerSet.loadDataFile ();
//			SkillSet.loadDataFile ();
//			TrapInfoSet.loadDataFile ();
//			TalentSet.loadDataFile ();
//			MapSet.loadDataFile (); //
//			ParamSet.loadDataFile ();
//			GuideSet.loadDataFile ();
		}
//		yield return null;
		yield return new WaitForEndOfFrame ();
	}
	IEnumerator LoadMainiFast ()
	{
		Debug.LogError ("----zys----  start load mainfalst");
		yield  return StartCoroutine(Initialize() );
		//		StartCoroutine(Initialize() );

		Debug.LogError ("----zys----  back to laod");
//		yield  return StartCoroutine(LoadMap() );
	}
//	protected IEnumerator LoadMap()
//	{
//		//"map_"+ mapID, "SceneMdID_"+ mapID
//		//		AssetBundleLoadAssetOperation request1 = AssetBundleManager.LoadAssetAsync("map_materials", "Addons", typeof(Material) );
//		AssetBundleLoadAssetOperation request1 = AssetBundleManager.LoadAssetAsync("map_1", "SceneMdID_1", typeof(GameObject) );
//		if (request1 == null)
//			yield break;
//		yield return StartCoroutine(request1);
//	}
	/// <summary>
	/// Initialize this instance.
	/// 官方的方式加载 mainfaset 文件;
	/// </summary>
	protected IEnumerator Initialize()
	{
		// Don't destroy this gameObject as we depend on it to run the loading script.
//		DontDestroyOnLoad(gameObject);

		// With this code, when in-editor or using a development builds: Always use the AssetBundle Server
		// (This is very dependent on the production workflow of the project. 
		// 	Another approach would be to make this configurable in the standalone player.)
//		#if DEVELOPMENT_BUILD || UNITY_EDITOR
		//加载streampath 里的;
//		AssetBundleManager.SetDevelopmentAssetBundleServer ();
//		#else
		// Use the following code if AssetBundles are embedded in the project for example via StreamingAssets folder etc:
//		Debug.LogError ("zys set url in file:// ......");
//		if(AssetBundleManager.isNeedURLUpdata)
//		{	
//		//http://10.0.4.139/StreamingAssets/iOS/iOS
//
//		AssetBundleManager.SetSourceAssetBundleURL("http://127.0.0.1/StreamingAssets/");
//		}else{
		AssetBundleManager.SetSourceAssetBundleDirectory();
//		}
//	
		// Or customize the URL based on your deployment or configuration
		//AssetBundleManager.SetSourceAssetBundleURL("http://www.MyWebsite/MyAssetBundles");
//		#endif

		Debug.LogError ("----zys----   StartCoroutine");
		// Initialize AssetBundleManifest which loads the AssetBundleManifest object.
		var request = AssetBundleManager.Initialize();
		if (request != null)
			yield return StartCoroutine(request);


		Debug.LogError ("----zys----  end load mainfalst");
	}

	private bool  UnCommonZip (string path)
	{	
		//Debuger.LogError ("zys UnCommonZip fun");
		//		string spriteDir = Application.persistentDataPath + "/Data";
		if (!File.Exists (path)) {
			Debuger.LogError ("zys error zip null");
			return true;
		}
		try {
			//Debuger.LogError ("zys start lzmu unCompose ==  "+ path);
			string assetbundlePath = Application.persistentDataPath + "/Data";
			string outFilePath = Application.persistentDataPath + "";
			bool isOk =	LzmaTools.DecompressLzma (assetbundlePath, "Data."+LzmaTools.zipName, outFilePath);

#if UNITY_EDITOR

#else
			if(File.Exists(path))
			{
			File.Delete(path);
			}
#endif

		} catch (System.Exception e) {
			Debuger.LogError ("unZIp  error "+e);

			return false;
		}

		return true;
	}
}



