using  System;

/// <summary>
/// 进去主界面的前置逻辑;
/// 去掉开始游戏前的 assetbundle；
/// 加载主界面的ab;
/// 加载;
/// </summary>
public class GamePreCtrl : PreCtrlBase
{
    /// <summary>
    /// Stars the load data.
    /// 开始加载前置数据;
    /// </summary>
    public override void StarLoadData(params object[] args)
    {
        //不注视掉 有个 还没结束就执行回掉 的bug
    }

    /// <summary>
    /// Ends the load scene object.
    /// 加载完后场景 预设;
    /// </summary>
    public override void EndLoadSceneObj()
    {
    }
}