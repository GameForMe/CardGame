using System;
using System.Collections;
using UnityEngine;
using System.IO;

/// <summary>
/// Http pbj.
/// 提交参数时候的结构体;
/// </summary>
public class HttpPbj
{
	public HttpPbj(string _name, string _value)
	{
		name = _name;
		value = _value;
	}
	public string name = "";
	public string value = "";
}

/// <summary>
/// Http controller.
/// 消息通讯协议库;
/// 开线程与服务器通讯;
/// 返回结果存入对象池，等主线程来轮训;
/// </summary>
public class HttpController
{
	protected GameObject gameObject;
	protected Transform transform;
	//ObjectPool<HttpDataCmp> catchePool;
	protected HttpController ()
	{
		gameObject = new GameObject ("HttpController");
		transform = gameObject.transform;
		MonoBehaviour.DontDestroyOnLoad (gameObject);
		//catchePool = ObjectPoolManager.Instance ().CreatePool<HttpDataCmp> (OnPoolGetElement, OnPoolPushElement);
//		classObjPool.Get ();
//		catchePool.countAll = 10;
	}

	/// <summary>
	/// 当从池子里面获取时
	/// </summary>
	/// <param name="obj"></param>
	void OnPoolGetElement (HttpDataCmp obj)
	{
		Debug.Log ("HttpDataCmp OnPoolGetElement--->>>" + obj);
	}

	/// <summary>
	/// 当放回池子里面时
	/// </summary>
	/// <param name="obj"></param>
	void OnPoolPushElement (HttpDataCmp obj)
	{
		Debug.Log ("OnPoolPushElement HttpDataCmp--->>>" + obj);
	}
	//	protected List n HttpDataCmp httpCmpCatchArr
	private static HttpController m_Instance;

	static public HttpController Instance ()
	{
		if (m_Instance == null) {
			m_Instance = new HttpController ();
		}
		return m_Instance;
	}

	/// <summary>
	/// Sends the message.
	/// 当不需要发送数据时;
	/// </summary>
	/// <param name="msgID">Message I.</param>
	/// <param name="URL">UR.</param>
	public void SendMsgJson (GameMessage msgID, string URL,bool isNeedTipWaite = false)
	{
		Debuger.LogWarning ("向服务器发送 http " + msgID + "  ");
		SendMsgJson (msgID,URL,isNeedTipWaite,null);
	}
	/// <summary>
	/// Sends the message.
	/// 发送结构体过去;
	/// </summary>
	/// <param name="msgID">Message I.</param>
	/// <param name="URL">UR.</param>
	/// <param name="protoSt">Proto st.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public void SendMsg <T> (GameMessage msgID, string URL, T protoSt,bool isNeedTipWaite = false) where T : class, ProtoBuf.IExtensible
	{
		Debuger.LogWarning ("向服务器发送 http " + msgID + "  " );
		using (MemoryStream ms = new MemoryStream ()) {
			bool isNeed = MsgSet.IsNeedTipByMsgID ((int)msgID);
			if(isNeedTipWaite || isNeed)
			{			
				GTSenceManage.Instance ().StartLoadData ();
			}
			new PBMessageSerializer().Serialize (ms, protoSt);
			byte[] bytes = ms.ToArray ();

			NetPacket pack = new NetPacket ((int)msgID, bytes.Length);
			pack.setData (bytes);

			GameObject oneObj = new GameObject("http_"+msgID);
			HttpDataCmp httpCmp = oneObj.AddComponent<HttpDataCmp> ();
			httpCmp.isNeedTipWaite = isNeedTipWaite;
			httpCmp.transform.parent = transform;
		
			httpCmp.GET (msgID,URL,GetDataCallFun,pack);
		}
	}
	/// <summary>
	/// Sends the message.
	/// 用消息ID 获取信息;
	/// </summary>
	/// <param name="msgID">Message I.</param>
	/// <param name="URL">UR.</param>
	public void SendMsg  (GameMessage msgID, string URL,bool isNeedTipWaite = false) 
	{

		bool isNeed = MsgSet.IsNeedTipByMsgID ((int)msgID);
		if(isNeedTipWaite || isNeed)
		{			
			GTSenceManage.Instance ().StartLoadData ();
		}

			NetPacket pack = new NetPacket ((int)msgID, 0);

			GameObject oneObj = new GameObject("http_"+msgID);
			HttpDataCmp httpCmp = oneObj.AddComponent<HttpDataCmp> ();
		httpCmp.isNeedTipWaite = isNeedTipWaite;
			httpCmp.transform.parent = transform;

			httpCmp.GET (msgID,URL,GetDataCallFun,pack);

	}


	/// <summary>
	/// Sends the message.
	/// 带参数l
	/// </summary>
	/// <param name="msgID">Message I.</param>
	/// <param name="URL">UR.</param>
	/// <param name="values">Values.</param>
	public void SendMsgJson (GameMessage msgID, string URL,bool isNeedTipWaite , params HttpPbj[] values)
	{
		bool isNeed = MsgSet.IsNeedTipByMsgID ((int)msgID);
		if(isNeedTipWaite || isNeed)
		{		
				GTSenceManage.Instance ().StartLoadData ();
		}
		//		if(catchePool.countInactive > 0)
		//		{
		//			
		//		}
		//		catchePool
		//		catchePool.Get ();
		GameObject oneObj = new GameObject("http_"+msgID);
		HttpDataCmp httpCmp = oneObj.AddComponent<HttpDataCmp> ();
		httpCmp.isNeedTipWaite = isNeedTipWaite;
		httpCmp.transform.parent = transform;
		httpCmp.GET (msgID,URL,GetDataCallFun,values);
	}

    





	void GetDataCallFun(HttpDataCmp cmp, byte[] text)
	{
		Debuger.LogWarning ("get back http " + cmp.m_msgID + "  ");
		bool isNeed = MsgSet.IsNeedTipByMsgID ((int)cmp.m_msgID);
		if(cmp.isNeedTipWaite || isNeed)
		{
				GTSenceManage.Instance ().EndLoadData ();
		}
		if (text != null ) {
			NetPacket packet = null;
		
			int bodySize = text.Length - NetPacket.PACK_HEAD_SIZE;
			if (bodySize < 0) {
				bodySize = 0;
				Debuger.LogError ("服务器返回的字节 不够包头 "+ cmp.m_msgID.ToString());
				packet = new NetPacket ((int)cmp.m_msgID, 0);
			} else {
				//读取包身数据;
				Byte[] data = new Byte[bodySize];
				Array.Copy (text, NetPacket.PACK_HEAD_SIZE, data, 0, data.Length);
				//读取包头数据;
				Byte[] msgIDData = new Byte[NetPacket.PACK_HEAD_SIZE];
				Array.Copy (text, 0, msgIDData, 0, msgIDData.Length);
				packet = new NetPacket ((int)cmp.m_msgID, data.Length);
				packet.setPackHead (msgIDData);
				packet.setData (data);
			}
				

			MessageRecieveHandle.Instance ().OnRecvMessage (cmp.m_msgID, packet);

		} else {
			Debug.Log ("call back  error");
		}
		MonoBehaviour.Destroy (cmp.gameObject);
	}

}

