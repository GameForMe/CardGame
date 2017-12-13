using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnterModel  : BaseModel
{

	private GameEnterModel ()
	{		
		InstanceNetWork ();
	}


	/// <summary>
	/// The COMMAND.
	/// 监听对消息id;
	/// </summary>
	private static int[] COMMANDS = new int[] {
		(int)GameMessage.RAV_Req_Logon_NEWUSER
	};


	private void InstanceNetWork ()
	{
		//注册监听消息;/
		MessageRecieveHandle.Instance ().RegisterMessageHandler (COMMANDS, OnRecvMessage);
	}

	void OnRecvMessage (GameMessage msgID, NetPacket packet)
	{
//		switch (msgID)
//		{
//			case GameMessage.RAV_Req_Logon_NEWUSER:
//			{
////				Deal_RegistAcc(packet);
//			}
//				break;
//		}

	}
	/// <summary>
	/// 直接进入游戏不用登陆;
	/// </summary>
	public void LogonGame()
	{
		GTSenceManage.Instance().GotoMainUiSenceFromLogon();
	}
	
}
