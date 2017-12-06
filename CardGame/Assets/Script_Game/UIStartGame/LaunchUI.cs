using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 闪屏;
/// </summary>
public class LaunchUI : MonoBehaviour
{
    private Image LogoSp;

    void Awake()
    {
        Transform find = transform.Find("Logo");
        if (find != null)
        {
            LogoSp = find.GetComponent<Image>();
        }

//		GotoMain ();
        if (LogoSp != null)
        {
//			Time.timeScale = 0;

            //调用DOmove方法来让图片移动  
            //	下面两种写法都可以
//		    Tweener tweener = DOTween.ToAlpha
//		    (
//			    () => LogoSp.color,
//			    (c) => LogoSp.color = c,
//			    1,
//			    1.5f
//		    );

            Tweener tweener = LogoSp.DOFade(1, 1.5f);
            //设置这个Tween不受Time.scale影响
            tweener.SetUpdate(true);
            //设置移动类型
            tweener.SetEase(Ease.Linear);
            tweener.OnComplete(EndShowLogo);

//		    EndShowLogo();
        }
        else
        {
            GotoMain();
        }
    }

    // Use this for initialization
    void Start()
    {
    }

    void EndShowLogo()
    {
        if (false)
        {
        }
        else
        {
            GotoMain();
        }
    }

    void EndShowTip()
    {
        Invoke("GotoMain", 1.7f);
    }

    /// <summary>
    /// 这里去主界面应该通过资源检测  GameUpdataManagerCp;
    /// 资源更新后打开登陆界面;
    /// 点击登陆后才进入GamePreCtrl;
    /// </summary>
    void GotoMain()
    {
        GTSenceManage.Instance().AddLoadingUIToSence<GameUpdataManagerCp>(EndDealOp_ScenePreCtrl,EndAddLoadingUI);
        //添加loading
//        GamePreCtrl preCtrl = GTSenceManage.Instance().AddLoadingUIToSence<GamePreCtrl>(EndDealOp_ScenePreCtrl);
//		LoadingController.GetInstance ().GotoOneSence (SenceType.mainSence);

//		LoadingController.GetInstance ().LoadSenceAsyncDone(); = "main";
//		Application.LoadLevelAsync("loading");
        //		Application.LoadLevelAdditiveAsync ("yourScene"); //不删除原场景 情况下  慎用.
    }

    void EndAddLoadingUI(PreCtrlBase ctrl)
    {
        
    }

    void EndDealOp_ScenePreCtrl()
    {
        Debuger.Log("zys -----  资源更新检测结束");

		GTSenceManage.Instance ().GotoLogonSence ();
    }
}