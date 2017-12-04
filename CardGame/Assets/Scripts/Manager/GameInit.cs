using System;
using  UnityEngine;

public class GameInit
{
	private GameInit ()
	{
	}

	private static GameInit _instance;

	public static GameInit Instance ()
	{
		if (null == _instance) {
			_instance = new GameInit ();
		}
		return _instance;
	}

	public void FirstStartGame()
	{
		Debuger.EnableLog = true;

//		GameDataCenter.Instance ().GetCacthDataToSet ();
//		HttpController.Instance ();
		ModelManager.Instance ().InitAllModel ();
//		GTUIManager.Instance().InitUIBase ();
//
//		GameDataCenter.Instance ().GetCacthDataToSet ();
	}

	public  Transform StaticCanvas { get; set;} 
	public  Transform EffCanvas { get; set;} 
	
	public void SetInitRootUI(GameObject staticObj, GameObject effObj)
	{
		StaticCanvas = staticObj.transform;
		EffCanvas = effObj.transform;
	}
/// <summary>
/// 添加显示根节点;
/// </summary>
	public void AddShowRootUI()
	{
		
	}
}

