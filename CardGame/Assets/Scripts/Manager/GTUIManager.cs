using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssetBundles;

public class GTUIManager : MonoBehaviour
{
	public GTUIManager ()
	{
	}

	protected static GameObject m_gameObject;

	private static GTUIManager _instance;

	public static GTUIManager Instance ()
	{
		if (_instance == null) {
			m_gameObject = new GameObject ("GTUIManager");
			_instance = m_gameObject.AddComponent<GTUIManager> ();
			DontDestroyOnLoad (m_gameObject);
		}
		return _instance;
	}


	/// <summary>
	/// Gets the manager base root.
	/// 获取ui管理器的 跟节点l
	/// </summary>
	/// <returns>The manager base root.</returns>
	public Transform GetManagerBaseRoot()
	{
		return m_gameObject.transform;
	}

	public  Transform StaticCanvas { get; set;} 
	public  Transform EffCanvas { get; set;} 
	
	public void SetInitRootUI(GameObject staticObj, GameObject effObj)
	{
		StaticCanvas = staticObj.transform;
		EffCanvas = effObj.transform;
	}
	
	public delegate void EndAddUiToCanvas(GameObject obj);
	/// <summary>
	/// 添加预设到画布上;
	/// </summary>
	/// <param name="assetName"></param>
	/// <param name="prefabName"></param>
	/// <param name="isStaticUI"></param>
	/// <param name="endCall"></param>
	/// <returns></returns>
	public IEnumerator AddUiToCanvas(string assetName,string prefabName,bool isStaticUI,EndAddUiToCanvas  endCall)
	{
		AssetBundleLoadAssetOperation request =
			AssetBundleManager.LoadAssetAsync(assetName, prefabName, typeof(GameObject));
		if (request == null)
			yield break;
		yield return StartCoroutine(request);
		GameObject prefab = request.GetAsset<GameObject>();
		GameObject startUI = null;
		if (prefab != null)
		{
			startUI = GameObject.Instantiate(prefab);
			startUI.transform.parent = isStaticUI ? StaticCanvas : EffCanvas;
			startUI.transform.localPosition = prefab.transform.localPosition;
			startUI.transform.localScale = prefab.transform.localScale;
		}
		AssetBundleManager.UnloadAssetBundle("assetName");
		if (endCall != null)
		{
			endCall(startUI);
		}
	}


}


