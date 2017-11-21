using System;
using UnityEngine;


/// <summary>
/// Loading controller.
///  加载大场景 及跳转;
/// laodingSence到了90% 通知前置程序 prectrl
/// </summary>
public class LoadingController
{

	public LoadingController (){
		
	}

	protected static  LoadingController instance;


	public static LoadingController GetInstance ()
	{
		if (instance == null) {
			instance = new LoadingController ();
		}
		return instance;
	}

	public SenceType preSence;

	string addStr = "";

	protected SenceType curSence ;
	public SenceType CurSence {
		get {
			if(curSence == null)
			{
				return SenceType.startSence;
			}
			return curSence;
		}
	}
	/// <summary>
	/// Gets the type of the sence name by.
	/// 获取场景名字;
	/// </summary>
	/// <returns>The sence name by type.</returns>
	/// <param name="type">Type.</param>
	public string GetSenceNameByType(SenceType type)
	{
		string senceName = "StartSence";
		switch(type)
		{
		case SenceType.startSence:
			senceName = "StartScene";
			break;
		case SenceType.loadingSence:
			senceName = "LoadingScene";
			break;
		case SenceType.mainSence:
			senceName = "MainScene";
			break;
		case SenceType.warSence:
			senceName = "BattleScene";
			break;
		case SenceType.guideSence:
			senceName = "MainScene";
			break;
		}
		return senceName;
	}
	/// <summary>
	/// Gets the current sence name string.
	/// 获取当前场景名称;
	/// </summary>
	/// <returns>The current sence name string.</returns>
	public string GetCurSenceNameStr()
	{
		return GetSenceNameByType (curSence);
	}


	public int num = 0;
	protected bool isLoading = false;

	public bool IsLoading {
		get {
			return isLoading;
		}
		protected set {
			isLoading = value;
		}
	}
	PreCtrlBase loadingPreCS ;
	LoadingUI loadingUI;
	//加载场景的异步;
	AsyncOperation senceLoadAsync;

	bool isSceneReady = false;
	bool isCtrlReady = false;
	/// <summary>
	/// Gotos the one sence.
	/// 跳转去一个场景;
	/// 先去跳转到loaidngSence，然后等到loadingsence 跳转;
	/// </summary>
	/// <param name="type">Type.</param>
	public void GotoOneSence(SenceType type,object param=null)
	{
		if(curSence != null)
		{
			preSence = curSence;	
		}
		isLoading = true;
		isSceneReady = false;

		isCtrlReady = false;
		curSence = type;
		if (curSence == SenceType.mainSence && (preSence == SenceType.startSence || preSence == null)) {
			//第一次的话 ;
//			loadingUI = AddLoadingUI <GameUpdataManager>(EndDealOp_ScenePreCtrl);
		} else {//暂时没有区分战斗还是主界面的前置;
			if (curSence == SenceType.mainSence) {
//				loadingUI = AddLoadingUI<PreCtrlMainScene>(EndDealOp_ScenePreCtrl);	
			} else {
//				loadingUI = AddLoadingUI<PreCtrlWarScene>(EndDealOp_ScenePreCtrl,mapID);
			}

		}

		GTWindowManage.Instance ().SenceWillChangeed ();
		GTSenceManage.Instance ().SenceWillChangeed ();
		AsyncOperation loadSence =  Application.LoadLevelAsync("LoadingScene");	
	}
	/// <summary>
	/// Sets the scene load async.
	/// 设置 正在加载场景的进程;
	/// </summary>
	/// <param name="async">Async.</param>
	public void SetSceneLoadAsync(AsyncOperation async)
	{
		senceLoadAsync = async;
	}
	/// <summary>
	/// Loads the sence async done.
	/// 加载场景的;
	/// </summary>
	public void LoadSenceAsyncDone()
	{
		Debuger.Log ("zys -----  1 场景准备好了");
		isSceneReady = true;
		if(loadingPreCS != null)
		{
			loadingPreCS.EndLoadSceneObj ();
		}
		ChecnIsLoadingEnd ();
	}

	/// <summary>
	/// Ends the deal op game up mag.
	/// 资源更新流程走完了;
	/// </summary>
	void EndDealOp_ScenePreCtrl()
	{
		Debuger.Log ("zys -----  1 前置逻辑准备好了");
		isCtrlReady = true;
		GTSenceManage.Instance ().PlanLogonSence ();
		ChecnIsLoadingEnd ();
	}
	/// <summary>
	/// Sets the loading sence progress.
	/// loadingsence 加载场景的进度l
	/// </summary>
	/// <param name="progress">Progress.</param>
	public void SetLoadingSenceProgress(float progress)
	{
		if(loadingUI != null)
		{
//			loadingUI.SetProgress (progress);
		}
	}
	/// <summary>
	/// Checns the is loading end.
	/// 监测是否整个流程结束;
	/// </summary>
	void ChecnIsLoadingEnd()
	{
		if(isSceneReady && isCtrlReady) 
		{
			isLoading = false;
			if (senceLoadAsync != null) {//让场景继续;
				senceLoadAsync.allowSceneActivation = true;	
			} else {//异常情况 跳转回startsence;
				
			} 
		}
	}

	/// <summary>
	/// Ends the loading after in sence.
	/// 进场景后删除loading
	/// 
	/// </summary>
	public void EndLoadingAfterInSence()
	{
		GTWindowManage.Instance ().SenceChangeed ();
		GTSenceManage.Instance ().SenceChangeed ();
		if(loadingUI != null)
		{
			loadingUI.CloseUI ();
		}
	}


	/// <summary>
	/// Adds the loading U.
	/// 在loading界面添加 显示的loadingUI;
	/// </summary>
	/// <param name="endCall">End call.</param>
	LoadingUI AddLoadingUI<T> (PreCtrlBase.EndLoading endCall, int param = -1) where T : PreCtrlBase
	{
		loadingPreCS = GTSenceManage.Instance().AddLoadingUIToSence<T> (endCall,param);
//		loadingPreCS
		LoadingUI loadingCS = loadingPreCS.gameObject.GetComponent<LoadingUI> ();
		return loadingCS;
	}

	/// <summary>
	/// Forces the go scene.
	/// 强制进入场景;
	/// </summary>
	public void ForceGoScene()
	{
		isCtrlReady = true;
		ChecnIsLoadingEnd ();
	}
}


