//------------------------------------------------------------------------------
// <zys>
//
// </zys>
//------------------------------------------------------------------------------
using System;
using UnityEngine; 
using System.Collections.Generic;


public class LayerSet
{
	public static string Default = "Default";
	public static string SenceBk = "SenceBk";
	public static string Role = "Role";
	public static string Panel = "Panel";
	public static string UI = "UI";
	public static string Effect = "Effect";
}

public class TagsSet
{
	public static  string FZView = "FZView";
	public static  string outWayDoor = "outWayDoor";
	public static  string killerPlayer = "killerPlayer";
}


public class GameSet
{
	public GameSet ()
	{
	}
//	public static string httpServerStr = "http://59.53.63.25:4001/ServerGate.aspx";
//		public static string httpServerStr = "http://w409544041.e2.luyouxia.net:30375/ServerGate.aspx";//吴敏

	public static string httpServerStr = "http://123.207.71.111:4001/ServerGate.aspx";//新外网
//	public static string httpServerStr = "10.0.4.63:4001/ServerGate.aspx";// neiwang neiwang
//	public static string httpServerStr = "http://superzys.e2.luyouxia.net:30976/ServerGate.aspx";//
//	public static string serverIP = "192.168.1.102";
	public static string serverIP = "127.0.0.1";
	public static int serverPort = 10001;
		
//	public static string photoServerIP = "59.53.63.25";
//	public static string photoServerIP = "10.165.2.49";
	public static string photoServerIP = "123.207.71.111";//新外网
//	public static string photoServerIP = "59.110.16.67";
//	public static string photoServerIP = "10.0.4.204";// 吴茵的电脑
//	public static string photoServerIP = "10.0.4.183";
	public static int photoServerPort = 5055;

	 

	public static string ChanelName = "test";
	public static int clientVersion = 1;

	public delegate void EndWaiteSometion();
	public delegate void EndShowTween(GameObject obj);
	public delegate void ShowMsgEndCall(GameObject obj);
	public delegate void EndLoading();
	public delegate void EndLoadingWar(bool bOK);



	public static bool bActionCardRound = true;

	/// <summary>
	/// The b open new plot for this war.
	/// 此次战斗有没有开启新的关卡;
	/// </summary>
	public static bool bOpenNewPlotForThisWar = true;
	/// <summary>
	/// The gain from war arr.
	/// 战斗中获取的 东西   破关奖励的;
	/// </summary>
	public static  List<object> gainFromWarArr = null;

	/// <summary>
	/// The b from war.
	/// 是不是从战场中来的;
	/// </summary>
	public static bool bFromWar = false;
	/// <summary>
	/// The b in war.
	/// 是否还在打仗中;
	/// </summary>
	public static bool bInWar = false;
	/// <summary>
	/// The b lock war action.
	/// 是否锁定了战斗行为;
	/// </summary>
	public static bool bLockWarAction = false;
	/// <summary>
	/// The b sound on.
	/// 音效开关;
	/// </summary>
	public static bool bSoundOn = true;
	/// <summary>
	/// The b music on.
	/// 音乐开关;
	/// </summary>
	public static bool bMusicOn = true;
	/// <summary>
	/// The b sound on.
	/// 音效大小;
	/// </summary>
	public static float bSoundvolume = 1;
	/// <summary>
	/// The b music on.
	/// 音乐大小;
	/// </summary>
	public static float bMusicOnvolume = 1;

	/// <summary>
	/// The room war pl number.
	/// 房间开战需要的人数;
	/// </summary>
	public static int RoomWarPlNum  = 5;

	/// <summary>
	/// 默认点击第几个模式
	/// </summary>
	public static int RoomType  = 1;

}


