using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AssetBundles;

public class TestAtlasBundle : MonoBehaviour {
    Image img = null;
	// Use this for initialization
	void Start () {
        Transform find = transform.Find("Image");
        if(find  != null )
        {
            img = find.GetComponent<Image>();
        }
        StartCoroutine(LoadResBundle("battleui", "test"));

        //AssetBundleCreateRequest assetbundle = null;

        //   assetbundle = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/battleui");
        //if(assetbundle == null)
        //{
        //    Debug.LogError("加载 assetbundle 失败 ");
        //}
        //else
        //{
        //    AssetBundleRequest spRqq = assetbundle.assetBundle.LoadAssetAsync< Sprite>("test" );
        //    if(spRqq ==null)
        //    {
        //        Debug.LogError(" 加载图片test 失败");
        //    }
        //    else if(img != null)
        //    {
        //        img.sprite = spRqq.asset as Sprite;
        //    }
        //}
    }
    IEnumerator LoadResBundle(string assetBundleName, string assetName)
    {
        AssetBundleManager.SetSourceAssetBundleDirectory();


        Debug.LogError("----zys----   StartCoroutine");
        // Initialize AssetBundleManifest which loads the AssetBundleManifest object.
        var reques1t = AssetBundleManager.Initialize();
        if (reques1t != null)
            yield return StartCoroutine(reques1t);
        //string m_DownloadingError;
        //LoadedAssetBundle  elementBundle = AssetBundleManager.GetLoadedAssetBundle(assetBundleName, out m_DownloadingError);
        //if (elementBundle != null)
        //{
        //    AssetBundleRequest request1 = elementBundle.m_AssetBundle.LoadAssetAsync(assetName);
        //    if (request1 != null)
        //    {
        //        Sprite prefab = request1.asset as Sprite; 
        //        img.sprite = prefab;
        //    }
        //}
        AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(assetBundleName, assetName, typeof(Sprite));
        if (request == null)
            yield break;
        yield return StartCoroutine(request);
        
        Sprite prefab = request.GetAsset<Sprite>();

        GameObject obj = request.GetAsset<GameObject>();

        img.sprite = prefab;

        //AssetBundleCreateRequest assetbundle = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/battleui");

        //AssetBundleRequest spRqq = assetbundle.assetBundle.LoadAssetAsync<Sprite>("test");
        //Sprite prefab = spRqq.asset as Sprite;
        //img.sprite = prefab;
        //yield return new WaitForEndOfFrame();
    }
    // Update is called once per frame
    void Update () {
		
	}
}
