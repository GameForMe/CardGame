using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Main user interface pre ctrl.
/// 登录成欧后进入主界面的前置操作；
/// 获取角色信息，
/// 
/// </summary>
public class MainUIPreCtrl : PreCtrlBase
{
    void Awake()
    {
    }

//    private GameUpdataManagerCp gmUpdataMgr = null;

    public override void StarLoadData(params object[] args)
    {
//        if (gmUpdataMgr == null)
//        {
//            gmUpdataMgr = gameObject.AddComponent<GameUpdataManagerCp>();
//            gmUpdataMgr.EndLoadCall = EndCheckAndLoadRes;
//        }
    }

    /// <summary>
    /// 走完资源更新流程;
    /// </summary>
    void EndCheckAndLoadRes()
    {
        EndLoadSceneObj();
    }

    public override void EndLoadSceneObj()
    {
        if (EndLoadCall != null)
        {
            EndLoadCall();
        }
    }
}