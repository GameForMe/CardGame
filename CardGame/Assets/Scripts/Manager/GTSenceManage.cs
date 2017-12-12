using UnityEngine;
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


    BaseSence curSence;
    BaseSence loadingSence;
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
    /// 关闭当前场景;
    /// </summary>
    void CloseCurSence()
    {
        if (curSence != null)
        {
            curSence.CloseUI();
            GTUIManager.Instance().ClearCurSenceAssetBundle();
        }
    }
    
    public void CloseLoadingUI()
    {
        if (loadingSence != null)
        {
            loadingSence .CloseUI();   
        }
    }
    
    #region 启动界面

    public IEnumerator AddStartSence()
    {
        yield return StartCoroutine(GTUIManager.Instance().AddUiToCanvas("uistartgame.unity3d","UIStartGame",true,false, (GameObject obj) =>
        {
            LaunchUI sence = obj.AddComponent<LaunchUI>();
            curSence = sence;
            if (loadingSence != null)
            {
                loadingSence.CloseUI();
            }
        }));
    }

    #endregion
    
    #region 登录场景控制;



    /// <summary>
    /// Gotos the logon sence.
    /// 进入登录场景;
    /// </summary>
    public void GotoLogonSence()
    {
        curSenceType = UISenceType.logonUISence;

        
        StartCoroutine(GTUIManager.Instance().AddUiToCanvas("uilogin.unity3d","UILogin",true,false, (GameObject obj) =>
        {
            CloseCurSence();
            LoginUI sence = obj.AddComponent<LoginUI>();
            curSence = sence;
            if (loadingSence != null)
            {
                loadingSence.CloseUI();
            }
        }));
        
//        StartCoroutine(AddLoginUIToSence());
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
        curSenceType = UISenceType.loadingUISence;
        AddLoadingUIToSence<PreCtrlMainScene>(true,StartOpenMainUISence, null);
    }

    /// <summary>
    /// Starts the open main user interface sence.
    /// 直接加载主场景到界面上;
    /// </summary>
    void StartOpenMainUISence()
    {
        curSenceType = UISenceType.mainUISence;

        StartCoroutine(GTUIManager.Instance().AddUiToCanvas("uimain.unity3d","uimain",true,true, (GameObject obj) =>
        {
            MainUI sence = obj.AddComponent<MainUI>();

            CloseCurSence();
            if (loadingSence != null)
            {
                loadingSence.CloseUI();
            }
            curSence = sence;
        }));
//        StartCoroutine(AddmainUIToSence());

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
        CloseCurSence();
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


    /// <summary>
    /// 添加场景内大loaidng 执行逻辑;
    /// 
    /// </summary>
    /// <param name="isRemoveCurSence">loading 添加好之后是否删掉当前场景</param>
    /// <param name="endCall"></param>
    /// <param name="addCall"></param>
    /// <param name="args"></param>
    /// <typeparam name="T"></typeparam>
    public void AddLoadingUIToSence<T>(bool isRemoveCurSence, PreCtrlBase.EndLoading endCall, PreCtrlBase.EndAddLoadingUI addCall,
        params object[] args) where T : PreCtrlBase
    {
//        StartCoroutine(AddLoadingUI<T>(endCall, addCall, args));
        if (loadingSence != null)
        {  
            if (isRemoveCurSence)
            {
                CloseCurSence();
            }
            T precCtrl = loadingSence.gameObject .AddComponent<T>();
            precCtrl.loadingUICS = (LoadingUI)loadingSence;
            precCtrl.EndLoadCall = endCall;
            precCtrl.StarLoadData(args);
            if (addCall != null)
            {
                addCall(precCtrl);
            }
        }
        else
        {
            StartCoroutine(GTUIManager.Instance().AddUiToCanvas("uiloading.unity3d","uiloading",true, false,(GameObject obj) =>
            {
                if ( isRemoveCurSence)
                {
                    CloseCurSence();
                }
                LoadingUI diaSrc = obj.AddComponent<LoadingUI>();
            
                T precCtrl = obj.AddComponent<T>();
                precCtrl.loadingUICS = diaSrc;
                precCtrl.EndLoadCall = endCall;
                precCtrl.StarLoadData(args);
                loadingSence = diaSrc;
                if (addCall != null)
                {
                    addCall(precCtrl);
                }
            }));
        }
//	
    }



    #endregion
}