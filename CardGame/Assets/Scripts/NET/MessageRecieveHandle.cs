using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// On recv message delegate.
/// 消息广播函数;
/// </summary>
public  delegate  void OnRecvMessageDelegate(GameMessage msgID, NetPacket content);

/// <summary>
/// Message recieve handle.
/// 消息包的转发;
/// 多socket连接体共用此广播,;
/// </summary>
class MessageRecieveHandle
{	


	private Dictionary<int, List<OnRecvMessageDelegate>> mMessageHandlers = new Dictionary<int, List<OnRecvMessageDelegate>>();
		
	private static MessageRecieveHandle m_Instance;
	
	static public MessageRecieveHandle Instance()
	{
		if (m_Instance == null)
		{
			m_Instance = new MessageRecieveHandle();
		}
		return m_Instance;
	}
	/// <summary>
	/// Registers the message handler.
	/// 注册一群消息，一个模块的多个消息可以在一个回调中switch执行;
	/// </summary>
	/// <param name="commands">Commands.</param>
	/// <param name="del">Del.</param>
	public void RegisterMessageHandler(int[] commands, OnRecvMessageDelegate del)
	{
		foreach (int c in commands)
		{
			RegisterMessageHandler (c,del);
		}
	}
	/// <summary>
	/// Registers the message handler.
	/// 注册一个消息的委托;
	/// </summary>
	/// <param name="command">Command.</param>
	/// <param name="del">Del.</param>
	public void RegisterMessageHandler(int command, OnRecvMessageDelegate del)
	{
		List<OnRecvMessageDelegate> handlerList = null;
		
		if (mMessageHandlers.ContainsKey(command))
		{
			mMessageHandlers.TryGetValue(command, out handlerList);
		}
		else
		{
			handlerList = new List<OnRecvMessageDelegate>();
			mMessageHandlers.Add((int)command, handlerList);
		}
		
		if (!handlerList.Contains(del))
		{
			handlerList.Add(del);
		}
	}
	/// <summary>
	/// Uns the register message handler.
	/// 解注册一群消息 一个模块的多个消息公用一个回调可以;
	/// </summary>
	/// <param name="commands">Commands.</param>
	/// <param name="del">Del.</param>
	public void UnRegisterMessageHandler(int[] commands, OnRecvMessageDelegate del)
	{
		foreach (int c in commands)
		{
			UnRegisterMessageHandler (c,del);
		}
	}
	/// <summary>
	/// Uns the register message handler.
	/// </summary>
	/// <param name="command">Command.</param>
	/// <param name="del">Del.</param>
	public void UnRegisterMessageHandler(int command, OnRecvMessageDelegate del)
	{
		if (mMessageHandlers.ContainsKey((int)command))
		{
			List<OnRecvMessageDelegate> handlerList = null;
			mMessageHandlers.TryGetValue((int)command, out handlerList);
			
			if (handlerList != null)
			{
				handlerList.Remove(del);
			}
		}
	}
	//error call back need remove this;
	List<OnRecvMessageDelegate> m_DeletesBuff = new List<OnRecvMessageDelegate>();
	/// <summary>
	/// Raises the recv message event.
	/// 客户端的一个连接socket 的消息广播;
	/// </summary>
	/// <param name="client">Client.</param>
	/// <param name="packet">Packet.</param>
	public void OnRecvMessage(GameMessage _msgID, NetPacket content)
	{
		int msgID = (int)_msgID;
		if (mMessageHandlers.ContainsKey(msgID))
		{
			List<OnRecvMessageDelegate> handlerList = null;
			mMessageHandlers.TryGetValue(msgID, out handlerList);
			
			if (handlerList != null)
			{
				if(m_DeletesBuff.Count != 0)
				{
					m_DeletesBuff.Clear();
				}
				List<OnRecvMessageDelegate> exeList = new List<OnRecvMessageDelegate>(handlerList);
				foreach (OnRecvMessageDelegate del in exeList)
				{
					if (del != null)
					{
						del(_msgID, content);
					}
				}
			}
		}
	}	

}

