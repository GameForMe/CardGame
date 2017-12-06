﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssetBundles;
using System.Threading.Tasks;


/// <summary>
/// Sence type.
/// 暂定只有开始的启动场景，加载场景和游戏主场景;
/// </summary>
public enum SenceType
{
    startSence,
    loadingSence,
    mainSence,
    guideSence,
    warSence
}

/// <summary>
/// User interface sence type.
/// 在主场景情况下  各个UIsence;
/// </summary>
public enum UISenceType
{
    logonUISence,
    loadingUISence,
    mainUISence,
    warUISence
}

/// <summary>
/// GameTool sence manage.
/// 场景级  的 UI部分控制管理l
/// 
/// 大场景切换除了loaidng 全干掉;
/// </summary>
public class GTSenceManage : MonoBehaviour
{
    protected GTSenceManage()
    {
    }
//	public static Transform MangerBase;

    protected static GameObject m_gameObject;

    private static GTSenceManage _instance;

    public static GTSenceManage Instance()
    {
        if (_instance == null)
        {
            Transform MangerBase = GTUIManager.Instance().GetManagerBaseRoot();
            m_gameObject = new GameObject("GTSenceManage");
            m_gameObject.transform.parent = MangerBase;
            _instance = m_gameObject.AddComponent<GTSenceManage>();
//			DontDestroyOnLoad (m_gameObject);
        }
        return _instance;
    }

    //	Transform roomTrans;

    /// <summary>
    /// Sets the sence root.
    /// 设置场景的  根目录;
    /// </summary>
    /// <param name="root">Root.</param>
    //	public void SetSenceRoot (Transform  root)
    //	{
    ////		roomTrans = root;
    //	}
    /// <summary>
    /// Gets the root trans.
    /// 当前要显示的根节点 对象;
    /// </summary>
    /// <value>The root trans.</value>
    //	public Transform RootTransform {
    //		get {
    //			return roomTrans != null ? roomTrans : gameObject.transform;
    //		}
    //	}
    void OnDestroy()
    {
//		Debuger.LogError ("GTSenceManage  !!! @@@ ____  被摧毁");
//		GameClientManager.Instance().OnDestroy();
    }

    // Update is called once per frame
    void Update()
    {
//		GameClientManager.Instance().Update();		
    }


    protected List<BaseBorderUI> borderUIList = new List<BaseBorderUI>();

    protected List<BaseBorderUI> borderUIList_ForDie = new List<BaseBorderUI>()
        ; //匹配死亡时, 需关闭的 事项; // 玩家 死亡或 成功逃脱时 , 关闭的相关 ui;

    BaseSence curSence;
    UISenceType curSenceType;

//	BaseUI RootParentUI;
    MainRootUI rootTrans;

    public MainRootUI RootTrans
    {
        get { return rootTrans; }
        set { rootTrans = value; }
    }

    Transform RootTransform
    {
        get { return rootTrans != null ? rootTrans.transform : null; }
    }

//	/// <summary>
//	/// Inits the user interface base.
//	/// 讲根目录显示在舞台上;
//	/// </summary>
//	public void InitUIBase ()
//	{
//		if (rootTrans == null) {
//			GameObject OriginalObj = CatchPoolManage.Instance ().GetOnePrefabsObj ("UI/GUIUI");
//
//			if (OriginalObj != null) {
//				GameObject temObj = GameObject.Instantiate (OriginalObj) as GameObject;
//				temObj.name = "RootUI";
//				DontDestroyOnLoad (temObj);
//
//				if (temObj.GetComponent<BaseUI> () != null) {
//					RootParentUI = temObj.GetComponent<BaseUI> ();
//				} else {
//					RootParentUI = temObj.AddComponent<BaseUI> ();
//				}
//				rootTrans = RootParentUI.transform.Find ("Camera");
//			}
//		}
//	}
    public void SenceWillChangeed()
    {
        CloseAllBorderUI();
    }

    /// <summary>
    /// Sences the change.
    /// 大场景跳转;
    /// </summary>
    public void SenceChangeed()
    {
        if (curSenceType == UISenceType.loadingUISence) //加载场景不用删LoaidngSence;
        {
            CloseAllBorderUI();
            if (curSence != null)
            {
                curSence.CloseUI();
            }
        }
    }

    void CloseAllBorderUI()
    {
        for (int i = 0; i < borderUIList.Count; i++)
        {
            BaseBorderUI win = borderUIList[i];
            if (win != null)
            {
                GameObject.Destroy(win.gameObject);
            }
        }
    }

    /// <summary>
    /// Closes all border U i for die.
    /// 玩家 死亡或 成功逃脱时 , 关闭的相关 ui;
    /// </summary>
    void CloseAllBorderUI_ForDie()
    {
        for (int i = 0; i < borderUIList_ForDie.Count; i++)
        {
            BaseBorderUI win = borderUIList_ForDie[i];
            if (win != null)
            {
                GameObject.Destroy(win.gameObject);
            }
        }
    }

    #region 登录场景控制;

    protected IEnumerator AddLoginUIToSence()
    {
        AssetBundleLoadAssetOperation request =
            AssetBundleManager.LoadAssetAsync("uilogin.unity3d", "UILogin", typeof(GameObject));
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

            startUI.AddComponent<LoginUI>();
        }
    }

    /// <summary>
    /// Gotos the logon sence.
    /// 进入登录场景;
    /// </summary>
    public void GotoLogonSence()
    {
        curSenceType = UISenceType.logonUISence;

        StartCoroutine(AddLoginUIToSence());
//		string m_DownloadingError;
//		LoadedAssetBundle bundle =
//			AssetBundleManager.GetLoadedAssetBundle("uilogin.unity3d", out m_DownloadingError);
//		if (bundle != null)
//		{
//			AssetBundleRequest request1 = bundle.m_AssetBundle.LoadAssetAsync("UILogin");
//			if (request1 != null)
//			{
//				GameObject uiPre = request1.asset as GameObject;
//				GameObject curUI = GameObject.Instantiate(uiPre);
//				curUI.transform.parent = GameInit.Instance().StaticCanvas.transform;
//				curUI.transform.localPosition = uiPre.transform.localPosition;
//				curUI.transform.localScale = uiPre.transform.localScale;
//                
//				curUI.AddComponent<LoginUI>();
////				Destroy(curUI.GetComponent("qiemove"));//删除绑定脚本  
//			}
//		}

//		GameObject OriginalObj = CatchPoolManage.Instance ().GetOnePrefabsObj ("UI/Logon/Prefabs/LogonUI");
//
//		if (OriginalObj != null) {
//
////			LogonUI diaSrc = InstantiateObjFun.AddOneObjToParent<LogonUI> (OriginalObj, RootTransform);		
////			curSence = diaSrc;
//		}
    }

    #endregion


    #region 进入主界面;

    /// <summary>
    /// Gotos the main user interface sence.
    /// 开始进入主ui场景;
    /// 上一个场景需要关闭。--- 关闭需要删除显示 清除ab 缓存;
    /// 
    /// </summary>
    public void GotoMainUISence()
    {
        if (curSence != null)
        {
            curSence.CloseUI();
        }
        curSenceType = UISenceType.loadingUISence;
        AddLoadingUIToSence<PreCtrlMainScene>(StartOpenMainUISence, null);
    }

    protected IEnumerator AddmainUIToSence()
    {
        AssetBundleLoadAssetOperation request =
            AssetBundleManager.LoadAssetAsync("uimain.unity3d", "uimain", typeof(GameObject));
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

            startUI.AddComponent<MainUI>();
        }
    }

    /// <summary>
    /// Starts the open main user interface sence.
    /// 直接加载主场景到界面上;
    /// </summary>
    void StartOpenMainUISence()
    {
        if (curSence != null)
        {
            curSence.CloseUI();
        }
        curSenceType = UISenceType.mainUISence;

        StartCoroutine(AddmainUIToSence());

//		string m_DownloadingError;
//		LoadedAssetBundle bundle =
//			AssetBundleManager.GetLoadedAssetBundle("uimain.unity3d", out m_DownloadingError);
//		if (bundle != null)
//		{
//			AssetBundleRequest request1 = bundle.m_AssetBundle.LoadAssetAsync("UIMain");
//			if (request1 != null)
//			{
//				GameObject uiPre = request1.asset as GameObject;
//				GameObject curUI = GameObject.Instantiate(uiPre);
//				curUI.transform.parent = GameInit.Instance().StaticCanvas.transform;
//				curUI.transform.localPosition = uiPre.transform.localPosition;
//				curUI.transform.localScale = uiPre.transform.localScale;
//                
//				curUI.AddComponent<MainUI>();
//			}
//		}

//		GameObject OriginalObj = CatchPoolManage.Instance ().GetOnePrefabsObj ("UI/MainUI/Prefabs/MainNewUIPanel");
//
//		if (OriginalObj != null) {
////			MainNewUIView diaSrc = InstantiateObjFun.AddOneObjToParent<MainNewUIView> (OriginalObj, RootTransform);
//////			MainUIView diaSrc = InstantiateObjFun.AddOneObjToParent<MainUIView> (OriginalObj, RootTransform);
////			curSence = diaSrc;
//		}
    }

    #endregion

    #region 进入战场界面;

    public delegate void EndAddUI();

    /// <summary>
    /// Gotos the battle user interface sence.
    /// 打开战场界面UI;
    /// 添加摇杆l
    /// 添加QTe;
    /// 添加屠夫接近提示;
    /// 添加暴露位置提醒;
    /// 添加队友信息提示;
    /// 
    /// </summary>
    public void GotoBattleUISence(EndAddUI endCallUI)
    {
        RootTrans.IsOpenListener = false;
        if (curSence != null)
        {
            curSence.CloseUI();
        }
        curSenceType = UISenceType.warUISence;

        AddBattleUIBack();
        AddBattleUI();
    }

    /// <summary>
    /// Adds the battle user interface back.
    /// 添加底层的UI 就是遮罩背景;
    /// </summary>
    void AddBattleUIBack()
    {
        GameObject OriginalObj = CatchPoolManage.Instance().GetOnePrefabsObj("UI/WarUI/Prefabs/WarUIBack");

        if (OriginalObj != null)
        {
//			WarUIBackView diaSrc = InstantiateObjFun.AddOneObjToParent<WarUIBackView> (OriginalObj, RootTransform);
//			borderUIList.Add (diaSrc);
        }
    }

    /// <summary>
    /// Adds the battle U.
    /// 添加战场UI;
    /// 内含  自己基础属性 离开暂停;
    /// </summary>
    void AddBattleUI()
    {
        GameObject OriginalObj = CatchPoolManage.Instance().GetOnePrefabsObj("UI/WarUI/Prefabs/WarUIPanel");

        if (OriginalObj != null)
        {
//			WarUIView diaSrc = InstantiateObjFun.AddOneObjToParent<WarUIView> (OriginalObj, RootTransform);
//			curSence = diaSrc;
        }
    }

    #endregion


    #region 加载数据的laoding data

    Stack isLoadingData = new Stack();

    /// <summary>
    /// Starts the load data.
    /// 开始加载数据;
    /// </summary>
    public void StartLoadData()
    {
        isLoadingData.Push(1);
        Debuger.LogWarning("need liad data  " + isLoadingData.Count);

        StartCoroutine(ShowLoadingDataUI());
    }

    LoadingDataUI LoadingDataUICS;

    IEnumerator ShowLoadingDataUI()
    {
        yield return new WaitForSeconds(1.5f);

//		if (isLoadingData.Count > 0 && LoadingDataUICS == null
//		    && curSenceType != UISenceType.loadingUISence
//		    && !LoadingController.GetInstance ().IsLoading) {
//			GameObject OriginalObj = CatchPoolManage.Instance ().GetOnePrefabsObj ("UI/Loading/Prefabs/LoadingDataUI");
//
//			if (OriginalObj != null) {
//				LoadingDataUICS = InstantiateObjFun.AddOneObjToParent<LoadingDataUI> (OriginalObj, RootTransform);	
//				LoadingDataUICS.EndForceCall = ForceClearWaitData;
//			}
//		}
//		if(LoadingDataUICS != null)
//		{
//			LoadingDataUICS.ResetCheck ();
//		}
    }

    void ForceClearWaitData()
    {
        isLoadingData.Clear();
    }

    /// <summary>
    /// Ends the load data.
    /// 结束加载数据;
    /// </summary>
    public void EndLoadData()
    {
        if (isLoadingData.Count > 0)
        {
            isLoadingData.Pop();
        }

        Debuger.LogWarning("end load data  " + isLoadingData.Count);
        if (isLoadingData.Count == 0 && LoadingDataUICS != null)
        {
            LoadingDataUICS.CloseUI();
        }
    }

    #endregion

    #region 加载的laodingUI

    IEnumerator AddLoadingUI<T>(PreCtrlBase.EndLoading endCall, PreCtrlBase.EndAddLoadingUI addCall,
        params object[] args) where T : PreCtrlBase
    {
        AssetBundleLoadAssetOperation request =
            AssetBundleManager.LoadAssetAsync("uiloading.unity3d", "uiloading", typeof(GameObject));
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

            LoadingUI diaSrc = startUI.AddComponent<LoadingUI>();

            T precCtrl = diaSrc.gameObject.AddComponent<T>();
            precCtrl.loadingUICS = diaSrc;
            precCtrl.EndLoadCall = endCall;
            precCtrl.StarLoadData(args);

            if (addCall != null)
            {
                addCall(precCtrl);
            }
        }
    }


    /// <summary>
    /// Adds the loading user interface to sence.
    /// 添加场景内大loaidng 执行逻辑;
    /// </summary>
    /// <typeparam name="T">The 1st type parameter.</typeparam>
    public void AddLoadingUIToSence<T>(PreCtrlBase.EndLoading endCall, PreCtrlBase.EndAddLoadingUI addCall,
        params object[] args) where T : PreCtrlBase
    {
        StartCoroutine(AddLoadingUI<T>(endCall, addCall, args));
//		AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync("uiloading.unity3d", "uiloading", typeof(GameObject));
//		if (request != null)
//		{
//			StartCoroutine(request);
//			GameObject prefab = request.GetAsset<GameObject>();
//			if (prefab != null)
//			{
//				GameObject startUI = GameObject.Instantiate(prefab);
//				startUI.transform.parent = GameInit.Instance().StaticCanvas.transform;
//				startUI.transform.localPosition = prefab.transform.localPosition;
//				startUI.transform.localScale = prefab.transform.localScale;
//
//				startUI.AddComponent<LoadingUI>();
//			}
//		}

//		string m_DownloadingError;
//		LoadedAssetBundle bundle = AssetBundleManager.GetLoadedAssetBundle("uiloading.unity3d", out m_DownloadingError);
//		if (bundle != null)
//		{
////			GameObject uiloading = null;
//			AssetBundleRequest request = bundle.m_AssetBundle.LoadAssetAsync("uiloading");
//			if (request != null)
//			{
//				GameObject OriginalObj = request.asset as GameObject;
////				uiloading = GameObject.Instantiate(uiloadingPre);
//				
//				curSenceType = UISenceType.loadingUISence;
//				if(LoadingDataUICS != null )
//				{
//					isLoadingData.Clear ();
//					LoadingDataUICS.CloseUI ();
//				}
//				if (curSence != null) {
//					curSence.CloseUI ();
//				}
//				LoadingUI diaSrc = InstantiateObjFun.AddOneObjToParent<LoadingUI> (OriginalObj, GameInit.Instance().StaticCanvas.transform);	
//				curSence = diaSrc;
//
//				T precCtrl = diaSrc.gameObject.AddComponent<T> ();
//				precCtrl.loadingUICS = diaSrc;
//				precCtrl.EndLoadCall = endCall;
//				precCtrl.StarLoadData (args);
//				return  precCtrl;
//			}
//		}


//		GameObject OriginalObj = CatchPoolManage.Instance ().GetOnePrefabsObj ("UI/Loading/Prefabs/LoadingUI");
//
//		if (OriginalObj != null) {
//			curSenceType = UISenceType.loadingUISence;
//			if(LoadingDataUICS != null )
//			{
//				isLoadingData.Clear ();
//				LoadingDataUICS.CloseUI ();
//			}
//			if (curSence != null) {
//				curSence.CloseUI ();
//			}
//			LoadingUI diaSrc = InstantiateObjFun.AddOneObjToParent<LoadingUI> (OriginalObj, RootTransform);	
//			curSence = diaSrc;
//
//			T precCtrl = diaSrc.gameObject.AddComponent<T> ();
//			precCtrl.loadingUICS = diaSrc;
//			precCtrl.EndLoadCall = endCall;
//			precCtrl.StarLoadData (args);
//			return  precCtrl;
//		}
//		return null;
    }

    #endregion
}