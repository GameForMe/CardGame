using System;
using UnityEngine;

/// <summary>
/// Pre ctrl base.
/// 前置逻辑基类;
/// </summary>
public class PreCtrlBase :MonoBehaviour
{
	public LoadingUI loadingUICS;

	public delegate void EndLoading();
	public delegate void EndAddLoadingUI(PreCtrlBase ctrl);
	/// <summary>
	/// The end load call.
	/// 执行完回调才函数;
	/// </summary>
	EndLoading endLoadCall;


	public EndLoading EndLoadCall {
		get {
			return endLoadCall;
		}
		set {
			endLoadCall = value;
		}
	}

	public PreCtrlBase ()
	{
	}
	/// <summary>
	/// Stars the load data.
	/// 开始加载前置数据;
	/// </summary>
	public virtual void StarLoadData(params object[] args){
		//不注视掉 有个 还没结束就执行回掉 的bug
//		if(EndLoadCall != null)
//		{
//			EndLoadCall ();
//		}
	}
	/// <summary>
	/// Ends the load scene object.
	/// 加载完后场景 预设;
	/// </summary>
	public virtual void EndLoadSceneObj(){
	}


}

