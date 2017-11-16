using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

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
//			tweener.onComplete = delegate() {
//				Debug.Log("移动完毕事件");
//			};
//			LogoSp.material.DOFade(0,1f).onComplete = delegate() {
//				Debug.Log("褪色完毕事件");
//			};
			
//			TweenAlpha tweenAp = LogoSp.gameObject.AddComponent<TweenAlpha> ();
//			tweenAp.from = LogoSp.alpha;
//			tweenAp.to = 1;
//			tweenAp.delay = 0.4f;
//			tweenAp.duration = 0.7f;
//			tweenAp.eventReceiver = gameObject;
//			tweenAp.callWhenFinished = "EndShowLogo"; 
			EndShowLogo();
		} else {
			GotoMain ();
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
	void GotoMain()
	{
		LoadingController.GetInstance ().GotoOneSence (SenceType.mainSence);
//		LoadingController.GetInstance ().LoadSenceAsyncDone(); = "main";
//		Application.LoadLevelAsync("loading");
		//		Application.LoadLevelAdditiveAsync ("yourScene"); //不删除原场景 情况下  慎用.
	}
}
