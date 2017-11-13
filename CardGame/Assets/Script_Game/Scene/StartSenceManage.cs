using UnityEngine;
using System.Collections;
/// <summary>
/// Start sence manage.
/// 闪屏后 连接服务器;
/// 监测资源更新;
/// 
/// </summary>
public class StartSenceManage : MonoBehaviour {

	Transform Cmp_TipShow;
	GameObject Lab_Title;
	GameObject Lab_Des;
	GameObject LogoSp;
	void Awake()
	{
		GameInit.Instance ().FirstStartGame ();
	
		Transform find = transform.Find ("Camera/LogoSp");
		if(find != null)
		{
			
		}
		find = transform.Find ("Camera/Cmp_TipShow");
		if(find != null)
		{
			find.gameObject.SetActive(false);
			Cmp_TipShow = find ;
		}
		find = transform.Find ("Camera/Cmp_TipShow/Lab_Title");
		if(find != null)
		{
			Lab_Title = find .gameObject;
		}
		find = transform.Find ("Camera/Cmp_TipShow/Lab_Des");
		if(find != null)
		{
			Lab_Des = find .gameObject;
		}
////		GotoMain ();
//		if (LogoSp != null) {
////			TweenAlpha tweenAp = LogoSp.gameObject.AddComponent<TweenAlpha> ();
////			tweenAp.from = LogoSp.alpha;
////			tweenAp.to = 1;
////			tweenAp.delay = 0.4f;
////			tweenAp.duration = 0.7f;
////			tweenAp.eventReceiver = gameObject;
////			tweenAp.callWhenFinished = "EndShowLogo"; 
//			EndShowLogo(null);
//		} else {
//			GotoMain ();
//		}
	}
	// Use this for initialization
	void Start () {

//		GTSenceManage.Instance ().InitUIBase ();
	}

//	void EndShowLogo(TweenAlpha tween)
//	{
////		GotoMain ();
//		if (Cmp_TipShow != null) {
//			LogoSp.gameObject.SetActive (false);
//			TweenAlpha tweenAp = Lab_Title.AddComponent<TweenAlpha> ();
//			tweenAp.from = 0;
//			tweenAp.to = 1;
//			tweenAp.delay = 0f;
//			tweenAp.duration = 1.3f;
//			tweenAp.eventReceiver = gameObject;
//			tweenAp.callWhenFinished = "EndShowTip"; 
//
//			TweenAlpha tweenAp1 = Lab_Des.AddComponent<TweenAlpha> ();
//			tweenAp1.from = 0;
//			tweenAp1.to = 1;
//			tweenAp1.delay = 0f;
//			tweenAp1.duration = 1.3f;
//			Cmp_TipShow.gameObject.SetActive (true);
//		}else {
//			GotoMain ();
//		}
//	}
//
//	void EndShowTip(TweenAlpha tween)
//	{
//
//		Invoke("GotoMain",1.7f);
//	}
//	void GotoMain()
//	{
//		if(Cmp_TipShow != null)
//		{
//			Cmp_TipShow.gameObject.SetActive (false);	
//		}
//		LoadingController.GetInstance ().GotoOneSence (SenceType.mainSence);
////		LoadingSet.LoadingSenceName = "main";
////		Application.LoadLevelAsync("loading");
//		//		Application.LoadLevelAdditiveAsync ("yourScene"); //不删除原场景 情况下  慎用.
//	}
}
