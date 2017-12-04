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
    }

    protected IEnumerator Initialize()
    {
        // 
        AssetBundleManager.SetSourceAssetBundleDirectory();


        Debug.LogError("----zys----   StartCoroutine");
        var request = AssetBundleManager.Initialize();
        if (request != null)
            yield return StartCoroutine(request);

        Debug.LogError("----zys----  end load mainfalst");
    }

    protected IEnumerator LoadRootUI()
    {
        AssetBundleLoadOperationFull request_baseui = AssetBundleManager.LoadAssetBundleAsync("baseui.unity3d");
        if (request_baseui == null)
            yield break;
        yield return StartCoroutine(request_baseui);

        string m_DownloadingError;
        LoadedAssetBundle bundle = AssetBundleManager.GetLoadedAssetBundle("baseui.unity3d", out m_DownloadingError);
        if (bundle != null)
        {
            GameObject staticCanvas = null;
            GameObject effCanvas = null;
            AssetBundleRequest request2 = bundle.m_AssetBundle.LoadAssetAsync("StaticCanvas");
            if (request2 != null)
            {
                GameObject staticCanvasPre = request2.asset as GameObject;
                staticCanvas = GameObject.Instantiate(staticCanvasPre);
            }
            AssetBundleRequest request1 = bundle.m_AssetBundle.LoadAssetAsync("EffCanvas");
            if (request1 != null)
            {
                GameObject effCanvasPre = request1.asset as GameObject;
                effCanvas = GameObject.Instantiate(effCanvasPre);
            }
            GameInit.Instance().SetInitRootUI(staticCanvas, effCanvas);
            yield return StartCoroutine(LoadLoadingUI());
            yield return StartCoroutine(LoadStartSenceUI());
        }
    }

    protected IEnumerator LoadLoadingUI()
    {
        AssetBundleLoadOperationFull request_uiloading = AssetBundleManager.LoadAssetBundleAsync("uiloading.unity3d");
        if (request_uiloading == null)
            yield break;
        yield return StartCoroutine(request_uiloading);
    }
    protected IEnumerator LoadStartSenceUI()
    {
        AssetBundleLoadOperationFull request_StartGame = AssetBundleManager.LoadAssetBundleAsync("UIStartGame.unity3d");
        if (request_StartGame == null)
            yield break;
        yield return StartCoroutine(request_StartGame);
        string m_DownloadingError;
        LoadedAssetBundle bundle =
            AssetBundleManager.GetLoadedAssetBundle("UIStartGame.unity3d", out m_DownloadingError);
        if (bundle != null)
        {
            AssetBundleRequest request1 = bundle.m_AssetBundle.LoadAssetAsync("UIStartGame");
            if (request1 != null)
            {
                GameObject uiPre = request1.asset as GameObject;
                GameObject startUI = GameObject.Instantiate(uiPre);
                startUI.transform.parent = GameInit.Instance().StaticCanvas.transform;
                startUI.transform.localPosition = uiPre.transform.localPosition;
                startUI.transform.localScale = uiPre.transform.localScale;
                
                startUI.AddComponent<LaunchUI>();
            }
        }
    }

    // Use this for initialization
    void Start()
    {
//		GTSenceManage.Instance ().InitUIBase ();
    }

   
}