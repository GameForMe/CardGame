using UnityEngine;
using System;
using System.Collections;
/// <summary>
/// Http data compare.
/// http请求基础组件。负责消息交互;   类似于socket消息的一个闭环;
/// </summary>
public class HttpDataCmp : MonoBehaviour {

	public delegate void HttpBackCall(HttpDataCmp httpCmp,byte[] txt);
	/// <summary>
	/// The is need tip waite.
	/// 是否需要提示等待;
	/// </summary>
	public bool isNeedTipWaite =false;
//	protected GameMessage m_msgID;
	private WWW m_www;
	private HttpBackCall m_CallFun = null;
	private bool m_bIsBeginRequest = false;
	private bool m_bIsDone = true;
	//取了多少次数ju ;
	int getDataCount =0;
	public GameMessage m_msgID{ get ; private set;}

	public bool IsBeginRequest
	{
		get { return m_bIsBeginRequest; }
		set { m_bIsBeginRequest = value; }
	}

	public bool IsDone
	{
		get { return m_bIsDone; }
		set { m_bIsDone = value; }
	}


	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}
	#region 字节流模式;
	NetPacket m_packet;
	public void GET(GameMessage msgID, string url, HttpBackCall callFun,NetPacket packet)
	{
		if (m_bIsDone)
		{
			m_msgID = msgID;
			m_CallFun = callFun;
			m_packet = packet;
			byte[] data = packet.getBuffer ();
			StartCoroutine(loadData(url,data));
		}
	}

	IEnumerator loadData(string url,byte[] data)
	{
		m_bIsBeginRequest = true;
		m_bIsDone = false;	
		m_www  = new WWW(url, data);  

		yield return m_www;
		getDataCount++;
		//等爆头;
		if(m_www.bytes.Length < NetPacket.PACK_HEAD_SIZE)
		{
			Debuger.LogError (" --- 包头 不够"+ m_msgID);
			byte[] dataSend = m_packet.getBuffer ();
			StartCoroutine(loadData(url,dataSend));
			yield return m_www;
		}
		else if(m_www.bytes.Length >= NetPacket.PACK_HEAD_SIZE) {
			//等包身;
			Byte[] msgIDData = new Byte[NetPacket.PACK_HEAD_SIZE];
			Array.Copy (m_www.bytes, 0, msgIDData, 0, msgIDData.Length);
			int len = BitConverter.ToInt32(msgIDData, NetPacket.PACK_LENGTH_OFFSET);
			if(m_www.bytes.Length < len)
			{
				Debuger.LogError (" --- body  不够"+ m_msgID);
				yield return m_www;
			}
			byte[] backStr = null;
			if (m_www.error != null) {
				Debuger.Log ("error " + m_www.error );
			} else {
				backStr = m_www.bytes;
			}
			if (null != m_CallFun) {
				int msgID = BitConverter.ToInt32(msgIDData, NetPacket.PACK_MESSSAGEID_OFFSET);
				Debuger.Log (" --- 够 了 "+ m_msgID);
				int msgReal = msgID & 0x0ffffff0; 
				if (msgReal != (int)m_msgID) {
					if (getDataCount >= 3) {
						GTWindowManage.Instance ().OpenDialog_Tip ("网络环境异常,请检查网络后重新启动游戏.");
						yield return m_www;
					} else {
						Debuger.LogError (" --- 发包是 " + m_msgID + " " + (int)m_msgID + "  回包 " + msgReal);
						byte[] dataSend = m_packet.getBuffer ();
						StartCoroutine (loadData (url, dataSend));
						yield return m_www;
					}

				} else {
					m_CallFun (this, backStr);
				}
			}
			m_bIsDone = true;
			m_bIsBeginRequest = false;
		}



	}
	#endregion

	#region 表单格式;
	public void GET(GameMessage msgID, string url, HttpBackCall callFun,params HttpPbj[] values)
	{
		if (m_bIsDone)
		{
			m_msgID = msgID;
			m_CallFun = callFun;
			WWWForm 		form = new WWWForm(); 	
			form.AddField("act", (int)msgID);  

			if(values != null && values.Length > 0)
			{			
				for(int i=0;i<values.Length;i++)
				{
					HttpPbj obj = values[i];
					form.AddField(obj.name, obj.value);  
				}
			}
			StartCoroutine(loadData(url,form));
		}
	}
	IEnumerator loadData(string url,WWWForm form)
	{
		m_bIsBeginRequest = true;
		m_bIsDone = false;

		if (form != null) {
			m_www  = new WWW(url, form);  
		} else {
			m_www = new WWW (url);
		}  

		yield return m_www;
//		//等爆头;
//		if(m_www.bytes.Length < NetPacket.PACK_HEAD_SIZE)
//		{
//			yield return m_www;
//		}
//		//等包身;
//		Byte[] msgIDData = new Byte[NetPacket.PACK_HEAD_SIZE];
//		Array.Copy (m_www.bytes, 0, msgIDData, 0, msgIDData.Length);
//		int len = BitConverter.ToInt32(msgIDData, NetPacket.PACK_LENGTH_OFFSET);
//		if(m_www.bytes.Length < len)
//		{
//			yield return m_www;
//		}

		byte[] backStr = null;
		if (m_www.error != null) {
			Debuger.Log ("error " + m_www.error );
		} else {
			backStr = m_www.bytes;
		}
		if (null != m_CallFun)
			m_CallFun(this,backStr);
		m_bIsDone = true;
		m_bIsBeginRequest = false;
	}
	#endregion
	// Update is called once per frame
//	void Update () 
//	{
//		if (m_bIsBeginRequest)
//		{
//			if (m_www.isDone)
//			{
//				if (null != m_CallFun)
//					m_CallFun(this.m_www.text);
//				m_bIsDone = true;
//				m_bIsBeginRequest = false;
//			}
//		}
//	}
}
