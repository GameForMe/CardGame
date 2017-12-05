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
    }
    //protected IEnumerator LoadRootUI()
    //{
    //    AssetBundleLoadOperationFull request_baseui = AssetBundleManager.LoadAssetBundleAsync("baseui.unity3d");
    //    if (request_baseui == null)
    //        yield break;
    //    yield return StartCoroutine(request_baseui);

    //    string m_DownloadingError;
    //    LoadedAssetBundle bundle = AssetBundleManager.GetLoadedAssetBundle("baseui.unity3d", out m_DownloadingError);
    //    if (bundle != null)
    //    {
    //        GameObject staticCanvas = null;
    //        GameObject effCanvas = null;
    //        AssetBundleRequest request2 = bundle.m_AssetBundle.LoadAssetAsync("StaticCanvas");
    //        if (request2 != null)
    //        {
    //            GameObject staticCanvasPre = request2.asset as GameObject;
    //            staticCanvas = GameObject.Instantiate(staticCanvasPre);
    //        }
    //        AssetBundleRequest request1 = bundle.m_AssetBundle.LoadAssetAsync("EffCanvas");
    //        if (request1 != null)
    //        {
    //            GameObject effCanvasPre = request1.asset as GameObject;
    //            effCanvas = GameObject.Instantiate(effCanvasPre);
    //        }
    //        GameInit.Instance().SetInitRootUI(staticCanvas, effCanvas);
    //        yield return StartCoroutine(LoadLoadingUI());
    //        yield return StartCoroutine(LoadStartSenceUI());
    //    }
    //}

//    protected IEnumerator LoadLoadingUI()
//    {
//        AssetBundleLoadOperationFull request_uiloading = AssetBundleManager.LoadAssetBundleAsync("uiloading.unity3d");
//        if (request_uiloading == null)
//            yield break;
//        yield return StartCoroutine(request_uiloading);
//    }
//    protected IEnumerator LoadStartSenceUI1()
//    {
//        AssetBundleLoadOperationFull request_StartGame = AssetBundleManager.LoadAssetBundleAsync("UIStartGame.unity3d");
//        if (request_StartGame == null)
//            yield break;
//        yield return StartCoroutine(request_StartGame);
//        string m_DownloadingError;
//        LoadedAssetBundle bundle =
//            AssetBundleManager.GetLoadedAssetBundle("UIStartGame.unity3d", out m_DownloadingError);
//        if (bundle != null)
//        {
//            AssetBundleRequest request1 = bundle.m_AssetBundle.LoadAssetAsync("UIStartGame");
//            if (request1 != null)
//            {
//                GameObject uiPre = request1.asset as GameObject;
//                GameObject startUI = GameObject.Instantiate(uiPre);
//                startUI.transform.parent = GameInit.Instance().StaticCanvas.transform;
//                startUI.transform.localPosition = uiPre.transform.localPosition;
//                startUI.transform.localScale = uiPre.transform.localScale;
//
//                startUI.AddComponent<LaunchUI>();
//            }
//        }
//    }

    // Use this for initialization
//    void Start()
//    {
//        //		GTSenceManage.Instance ().InitUIBase ();
//    }


}