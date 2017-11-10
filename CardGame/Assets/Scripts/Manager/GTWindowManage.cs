﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// GameTool window manage.
///  大场景切换除了loaidng 全干掉;
/// </summary>
public class GTWindowManage
{

    private static GameObject m_gameObject;

    private GTWindowManage()
    {

    }

    private static GTWindowManage _instance;

    public static GTWindowManage Instance()
    {
        if (null == _instance)
        {
            _instance = new GTWindowManage();
            Transform MangerBase = GTUIManager.Instance().GetManagerBaseRoot();
            m_gameObject = new GameObject("GTWindowManage");
            m_gameObject.transform.parent = MangerBase;
        }
        return _instance;
    }

    MainRootUI rootTrans;

    public MainRootUI RootTrans
    {
        get
        {
            return rootTrans;
        }
        set
        {
            rootTrans = value;
        }
    }
    Transform RootTransform
    {
        get
        {
            return rootTrans != null ? rootTrans.transform : null;
        }
    }


    /// <summary>
    /// The current sence bk.
    /// 当前场景的背景层;
    /// </summary>
    protected BaseSence curSenceBk;
    /// <summary>
    /// The type of the current sence.
    /// 当前场景类型;
    /// </summary>
    protected SenceType CurSenceType;

    /// <summary>
    /// The boder user interface list.
    /// 场景的UI层;
    /// </summary>
    protected List<BaseBorderUI> borderUIList = new List<BaseBorderUI>();
    /// <summary>
    /// The window list.
    /// 弹出的面板;
    /// </summary>
    protected List<BaseWindow> windowList = new List<BaseWindow>();

    /// <summary>
    /// The object list.
    ///
    /// </summary>
    protected List<BaseUI> objList = new List<BaseUI>();

    public void RemoveOneUI(BaseSence ui)
    {
        ui = null;
    }

    public void RemoveOneUI(BaseBorderUI ui)
    {
        borderUIList.Remove(ui);
    }

    public void RemoveOneUI(BaseWindow ui)
    {
        windowList.Remove(ui);
    }

    public void SenceWillChangeed()
    {
        CloseAllWindows();
    }
    public void SenceChangeed()
    {
        CloseAllWindows();
    }

    void CloseAllWindows()
    {
        for (int i = 0; i < windowList.Count; i++)
        {
            BaseWindow win = windowList[i];
            if (win != null)
            {
                GameObject.Destroy(win.gameObject);
            }
        }
    }
   
 
}