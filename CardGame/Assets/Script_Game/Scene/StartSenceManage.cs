using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using Script_Game.ScenePre;
using  AssetBundles;

/// <summary>
/// Start sence manage.
/// 闪屏后 连接服务器;
/// 监测资源更新;
/// 
/// </summary>
public class StartSenceManage : MonoBehaviour {


	Image LogoSp;
	void Awake()
	{
		GameInit.Instance ().FirstStartGame ();
		StartCoroutine (LoadMainiFast ());
		return;
		Transform find = transform.Find ("UIRoot/Logo");
		if(find != null)
		{
			LogoSp = find.GetComponent<Image>();
		}

//		GotoMain ();
		if (LogoSp != null) {
//			Time.timeScale = 0;
 
			//调用DOmove方法来让图片移动
			
			DOTween.ToAlpha  
			(  
				()  => LogoSp.color,  
				(c) => LogoSp.color = c,  
				0,   
				0.5f  
			);  
			 
			Tweener tweener = LogoSp.DOFade(0,1.5f);
			//设置这个Tween不受Time.scale影响
			tweener.SetUpdate(true);
			//设置移动类型
			tweener.SetEase(Ease.Linear);
			tweener.OnComplete(EndShowLogo);

			EndShowLogo();
		} else {
			GotoMain ();
		}
	}
	IEnumerator LoadMainiFast ()
	{
		Debug.LogError ("----zys----  start load mainfalst");
		yield  return StartCoroutine(Initialize() );

		Debug.LogError ("----zys----  back to laod");
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
		
		string m_DownloadingError;
		LoadedAssetBundle bundle = AssetBundleManager.GetLoadedAssetBundle("map_" , out m_DownloadingError);
		if (bundle != null)
		{
			
		}
		
	}

	// Use this for initialization
	void Start () {

//		GTSenceManage.Instance ().InitUIBase ();
	}

	void EndShowLogo()
	{
		if (false) {
		
		}else {
			GotoMain ();
		}
	}

	void EndShowTip()
	{

		Invoke("GotoMain",1.7f);
	}
	/// <summary>
	/// 这里去主界面应该通过资源检测;
	/// </summary>
	void GotoMain()
	{
		//添加loading
		GamePreCtrl preCtrl = GTSenceManage.Instance().AddLoadingUIToSence<GamePreCtrl>(EndDealOp_ScenePreCtrl);
//		LoadingController.GetInstance ().GotoOneSence (SenceType.mainSence);
		
//		LoadingController.GetInstance ().LoadSenceAsyncDone(); = "main";
//		Application.LoadLevelAsync("loading");
		//		Application.LoadLevelAdditiveAsync ("yourScene"); //不删除原场景 情况下  慎用.
	}
	
	void EndDealOp_ScenePreCtrl()
	{
		Debuger.Log ("zys -----  1 前置逻辑准备好了");
		
//		GTSenceManage.Instance ().PlanLogonSence ();
		
	}
	
}
