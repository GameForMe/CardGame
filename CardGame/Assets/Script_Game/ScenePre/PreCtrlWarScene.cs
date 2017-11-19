using System;
using UnityEngine;
using AssetBundles;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Pre ctrl war scene.
/// 进入战场前的数据准备工作,
/// 全体模型及场景加载完毕方能进战场;
/// 位置等数据的初始化;
/// 
/// res
/// 加载公用战场资源assetbundle
/// 并加载 动态变化  传入的 ;
/// </summary>
public class PreCtrlWarScene : PreCtrlBase 
{

	/// <summary>
	/// The is can all in.
	/// 是否可以全部进入了;
	/// </summary>
	bool isCanAllIn = false;
	bool isSelfCanIn = false;
	int mapID = -1;
	/// <summary>
	/// Stars the load data.
	/// 开始加载前置数据;
	/// 1 玩家坐标初始化;
	/// 2 所有玩家都加载完成l
	/// </summary>
	public override void StarLoadData(params object[] args){


		//注册全部准备好进入战场的委托l
//		PhotoNetController.Instance ().allLoadAndGo = AllReadyAndGo;
		//生成坐标;
//		RoomModel.Instance ().rand_posi___inRoom ();
	}
	/// <summary>
	/// Alls the ready and go.
	/// 圈都加载好了。可以进去了;
	/// </summary>
	void AllReadyAndGo()
	{
		isCanAllIn = true;
		CheckIsOutWaite ();
	}
	/// <summary>
	/// Ends the load scene object.
	/// 加载完后场景 预设;
	/// 等待接收全体进入指令;
	/// </summary>
	public override void EndLoadSceneObj(){
		//场景加载完后 异步加载  assetbundle;
		if(mapID >= 0){
			int loadiD = mapID == 0 ? 1 : mapID;
			StartCoroutine(InstantiateGameObjectAsync ("map_"+ loadiD, "SceneMdID_"+ loadiD) );	
		}
		else{
			endLoadRes ();
		}
	}
	protected IEnumerator InstantiateGameObjectAsync (string assetBundleName, string assetName)
	{
		yield return new WaitForEndOfFrame();

		endLoadRes ();
	}
	void endLoadRes()
	{
		isSelfCanIn = true;

		CheckIsOutWaite ();
	}


	/// <summary>
	/// Checks the is out waite.
	/// 检测是否完成所有准备工作，进入战场;
	/// </summary>
	void CheckIsOutWaite()
	{
		if(isSelfCanIn )
		{
			if (EndLoadCall != null) {
				EndLoadCall ();
			} else {

			}
		}
	}

}


