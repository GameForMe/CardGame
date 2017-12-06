using UnityEngine;
using System.Collections;
using AssetBundles;

/// <summary>
/// Start sence manage.
/// 获取内置的显示根;
/// 获取loading 的资源;
/// 闪屏后 连接服务器;
/// 监测资源更新;
/// 
/// </summary>
public class StartSenceManage : MonoBehaviour
{

    void Awake()
    {
        GameInit.Instance().FirstStartGame();
        StartCoroutine(LoadMainiFast());
        return;

    }

    IEnumerator LoadMainiFast()
    {
        Debug.LogError("----zys----  start load mainfalst");
        yield return StartCoroutine(Initialize());

        Debug.LogError("----zys----  back to laod");

        yield return StartCoroutine(LoadRootUI());
        yield return StartCoroutine(LoadRootUI1());

        AssetBundleManager.UnloadAssetBundle("baseui.unity3d");
        AssetBundleManager.UnloadAssetBundle("baseui.unity3d");

        GameInit.Instance().SetInitRootUI(staticCanvas, effCanvas);
        yield return StartCoroutine(LoadLoadingUI());
        yield return StartCoroutine(LoadStartSenceUI());
    }

    protected IEnumerator Initialize()
    {
        AssetBundleManager.IsLoadFromStream = true;
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        AssetBundleManager.SetDevelopmentAssetBundleServer();
#else
		// Use the following code if AssetBundles are embedded in the project for example via StreamingAssets folder etc:
        if(AssetBundleManager.IsLoadFromStream)
        {    
                AssetBundleManager.SetSourceAssetBundleDirectory();
        }else{
        AssetBundleManager.SetSourceAssetBundleURL(Application.dataPath + "/");
        }
		
#endif


        Debug.LogError("----zys----   StartCoroutine");
        var request = AssetBundleManager.Initialize();
        if (request != null)
            yield return StartCoroutine(request);

        Debug.LogError("----zys----  end load mainfalst");
    }
    GameObject staticCanvas = null;
    GameObject effCanvas = null;
    protected IEnumerator LoadRootUI()
    {
        AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync("baseui.unity3d", "StaticCanvas", typeof(GameObject));
        if (request == null)
            yield break;
        yield return StartCoroutine(request);
  
        // Get the asset.
        GameObject prefab = request.GetAsset<GameObject>();

        if (prefab != null)
            staticCanvas = GameObject.Instantiate(prefab);

    }

    protected IEnumerator LoadRootUI1()
    {
        AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync("baseui.unity3d", "EffCanvas", typeof(GameObject));
        if (request == null)
            yield break;
        yield return StartCoroutine(request);
        // Get the asset.
        GameObject prefab = request.GetAsset<GameObject>();

        if (prefab != null)
            effCanvas = GameObject.Instantiate(prefab);
    }
    /// <summary>
    /// 常驻内存不释放;
    /// </summary>
    /// <returns></returns>
    protected IEnumerator LoadLoadingUI()
    {
        AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync("uiloading.unity3d", "uiloading", typeof(GameObject));
        if (request == null)
            yield break;
        yield return StartCoroutine(request);
    }

    protected IEnumerator LoadStartSenceUI()
    {
        AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync("uistartgame.unity3d", "UIStartGame", typeof(GameObject));
        if (request == null)
            yield break;
        yield return StartCoroutine(request);
        GameObject prefab = request.GetAsset<GameObject>();
        if (prefab != null)
        {
            GameObject startUI = GameObject.Instantiate(prefab);
            startUI.transform.parent = GameInit.Instance().StaticCanvas.transform;
            startUI.transform.localPosition = prefab.transform.localPosition;
            startUI.transform.localScale = prefab.transform.localScale;

            startUI.AddComponent<LaunchUI>();
        }
        AssetBundleManager.UnloadAssetBundle("uistartgame.unity3d");
    }
   

}