﻿using System;
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
		GTUIManager.Instance();
		ModelManager.Instance ().InitAllModel ();
//		GTUIManager.Instance().InitUIBase ();
//
//		GameDataCenter.Instance ().GetCacthDataToSet ();
	}

/// <summary>
/// 添加显示根节点;
/// </summary>
	public void AddShowRootUI()
	{
		
	}
}

