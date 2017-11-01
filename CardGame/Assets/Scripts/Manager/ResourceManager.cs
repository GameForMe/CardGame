using UnityEngine;
using System.Collections;
using System.IO;
using System;
using AssetBundles;

public class ResourceManager : MonoBehaviour
{
	private static ResourceManager _instance;
	public static ResourceManager Instance()
	{
		if (null == _instance)
			_instance = new ResourceManager();
		return _instance;
	} 

	private AssetBundle shared;

	/// <summary>
	///初始化 加载必须要有的资源;
	/// </summary>
	public void initialize (Action func)
	{
//		if (AppConst.ExampleMode) {
//			//------------------------------------Shared--------------------------------------
//			string uri = Util.DataPath + "shared" + AppConst.ExtName;
//			Debug.LogWarning ("LoadFile::>> " + uri);
//
//			shared = AssetBundle.LoadFromFile (uri);
//#if UNITY_5
//			shared.LoadAsset ("Dialog", typeof(GameObject));
//#else
//                shared.Load("Dialog", typeof(GameObject));
//#endif
//		}
		if (func != null)
			func ();    //资源初始化完成，回调游戏管理器，执行后续操作 
	}

	/// <summary>
	/// 载入素材
	/// </summary>
	public AssetBundle LoadBundle (string name)
	{
		string uri = Util.DataPath + name.ToLower () + AppConst.ExtName;
	
		AssetBundle bundle = AssetBundle.LoadFromFile (uri); //关联数据的素材绑定
		return bundle;
	}

	/// <summary>
	/// 销毁资源
	/// </summary>
	void OnDestroy ()
	{
		if (shared != null)
			shared.Unload (true);
		Debuger.Log ("~ResourceManager was destroy!");
	}

//	public void LoadResBundler()
//	{
//		
//	}
//
//	IEnumerator LoadResBundle(string assetBundleName,string assetName)
//	{
//		AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(assetBundleName, assetName, typeof(GameObject) );
//		if (request == null)
//			yield break;
//		yield return StartCoroutine(request);
//		//		GTResouseManage.Instance ().CatchOneAsset ("map_1");
//		// Get the asset.
//		//		GameObject prefab = request.GetAsset<GameObject> ();
//		//
//		//		if (prefab != null)
//		//			senceObj = GameObject.Instantiate(prefab);
//	}
}
