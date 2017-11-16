using UnityEngine;
using System.Collections;
/// <summary>
/// Loading sence manager.
/// 登录场景根目录;
/// 添加loading 背景显示；
/// 是否需要连接服务器；
/// 是否需要 加载资源。
/// 逻辑处理完了才会跳转场景;
/// </summary>
public class LoadingSenceManager : MonoBehaviour {

	//异步对象
	AsyncOperation async;
	//读取场景的进度，它的取值范围在0 - 1 之间。
	int progress = 0;
	bool isLoaidng = false;
//	LoadingUI loadingUI;
//	public Text TxtTip;

	void Awake()
	{		

	}
	/// <summary>
	/// The is need other check.
	/// 是否需要等待其他的操作;
	/// </summary>
	bool IsNeedOtherCheck = false;
	/// <summary>
	/// The is loading sence.
	/// 是否在加载场景;
	/// </summary>
	bool isLoadingSence = false;

	// Use this for initialization
	void Start () {

		isLoadingSence = true;


		//加载下一个场景到90  等待;
		StartCoroutine(loadScene());
	}
//	/// <summary>
//	/// 游戏资源更新逻辑跑完;
//	/// Ends the deal op game up mag.
//	/// </summary>
//	void EndDealOp_GameUpMag()
//	{
//		IsNeedOtherCheck = false;
//		EndOp_CheckIsEndLoading ();
//	}
	/// <summary>
	/// Loads the scene.
	/// 不自动跳转的话Unity通过加载到0.9不让他加载实现。;
	/// </summary>
	/// <returns>The scene.</returns>
	IEnumerator loadScene()
	{
		isLoaidng = true;
		//异步读取场景。
		// 就是A场景中需要读取的C场景名称。
		string tarSence = LoadingController.GetInstance ().GetCurSenceNameStr() ;

		async = Application.LoadLevelAsync(tarSence);	
		LoadingController.GetInstance ().SetSceneLoadAsync (async);
		async.allowSceneActivation = false;

		while(async.progress < 0.9f) {
			SetLoadingPercentage(async.progress * 100);
			yield return new WaitForEndOfFrame();
		}
		SetLoadingPercentage(100);
		//		yield return new WaitForEndOfFrame();
		yield return new WaitForSeconds(0f);  
		isLoadingSence = false;
		EndOp_CheckIsEndLoading ();

		//读取完毕后返回， 系统会自动进入C场景  async.allowSceneActivation  = true
		//		yield return async;
	}

	void SetLoadingPercentage(float progress)
	{
		Debuger.Log ("loading  " + progress);
//		loadingUI.SetProgress (progress);
		LoadingController.GetInstance().SetLoadingSenceProgress(progress);
	}
	/// <summary>
	/// Ends the op check is end loading.
	/// 每个操作结束后都看下是否还有其他挂起的操作，没有就结束loading;
	/// </summary>
	void EndOp_CheckIsEndLoading(){
		//告诉控制器 场景 准备好了;
		LoadingController.GetInstance ().LoadSenceAsyncDone();

		//加载完场景后如果没有其他需要等待的操作了;
//		if(!IsNeedOtherCheck && !isLoadingSence)
//		{
//			isLoaidng = false;
//			async.allowSceneActivation = true;
//			LoadingController.GetInstance ().EndLoadingChangeSence ();
//		}
	}
}
