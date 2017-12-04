using System;

/// <summary>
/// Net pack.
/// 消息包结构体;
/// </summary>
public class NetPacket
{
	
	private int m_MessageID;///网络包id;
	private Byte[] m_Buffer;///网络包数括体，包头加包身;
	///定义包头偏移;
	// private static int PACK_HEAD_OFFSET = 0;
	///定义网络包的版本号;
	private static int PACK_VERSION_OFFSET = 2;
	///定义消息的长度包括包头和包身;
	public static int PACK_LENGTH_OFFSET = 3;
	///定义消息包id的偏移量;
	public static int PACK_MESSSAGEID_OFFSET = 7;
	///定义用户数据的偏移;
	private static int PACK_USERDATA_OFFSET = 11;
	///定义服务器ip的偏移量;
	private static int PACK_SERVERID_OFFSET = 15;
	///定义从包头到包身的偏移;
	private static int PACK_MESSAGE_OFFSET = 19;
	public static int PACK_HEAD_SIZE = 19;///定义包头大小;
	public static int PACK_CODE_SIZE = 4;///校验码长度; 

	private static int PACK_VERSION = 1;///定议消息包版本号;
	private static int PACKCodeMsg_VERSION = 3;///定议消息包版本号 加密的消息;


	public NetPacket(int messageid, int bodysize)
	{
		m_MessageID = messageid;


		m_Buffer = new Byte[bodysize + PACK_HEAD_SIZE];
		m_Buffer[0] = 8;
		m_Buffer[1] = 8;

		Byte[] byteArray = BitConverter.GetBytes(PACK_VERSION);
		Array.Copy(byteArray, 0, m_Buffer, PACK_VERSION_OFFSET, byteArray.Length);


		byteArray = BitConverter.GetBytes(bodysize + PACK_HEAD_SIZE);
		Array.Copy(byteArray, 0, m_Buffer, PACK_LENGTH_OFFSET, byteArray.Length);


		byteArray = BitConverter.GetBytes(messageid);
		Array.Copy(byteArray, 0, m_Buffer, PACK_MESSSAGEID_OFFSET, byteArray.Length);


		return;
	}

	public NetPacket(int messageid, int bodysize,bool bCodeMsg)
	{
		m_MessageID = messageid;
		int len = bodysize + PACK_HEAD_SIZE;
		if(bCodeMsg){
			//				len += PACK_CODE_SIZE;
		}
		m_Buffer = new Byte[len];
		m_Buffer[0] = 8;
		m_Buffer[1] = 8;


		Byte[] byteArray =  null;
		if(bCodeMsg)
		{
			byteArray = BitConverter.GetBytes(PACKCodeMsg_VERSION);
		}
		else{
			byteArray = BitConverter.GetBytes(PACK_VERSION);
		}
		Array.Copy(byteArray, 0, m_Buffer, PACK_VERSION_OFFSET, byteArray.Length);


		byteArray = BitConverter.GetBytes(bodysize + PACK_HEAD_SIZE);
		Array.Copy(byteArray, 0, m_Buffer, PACK_LENGTH_OFFSET, byteArray.Length);


		byteArray = BitConverter.GetBytes(messageid);
		Array.Copy(byteArray, 0, m_Buffer, PACK_MESSSAGEID_OFFSET, byteArray.Length);


		return;
	}



	/// <summary>
	/// 判断是不是包头数据;
	/// </summary>
	/// <returns></returns>
	static public bool isPackHead(Byte[] data)
	{

		if (data.Length != PACK_HEAD_SIZE)
		{
			Debuger.LogError("pack head lenght error:" + data.Length);
			return false;
		}

		if (data[0] != 8 || data[1] != 8)
		{
			int messageid = BitConverter.ToInt32(data, PACK_MESSSAGEID_OFFSET);
			Debuger.LogError("pack head  head version is error: messge id:"+messageid);

			string buffer = BitConverter.ToString(data,0);
			Debuger.LogError("packhead buffer:" + buffer);

			return false;
		}
			
		return true;
	}



	/// <summary>
	/// 获取消息的数据，包括包头和包身;
	/// </summary>
	/// <returns></returns>
	public Byte[] getBuffer()
	{
		return m_Buffer;
	}


	/// <summary>
	/// 获取消息id;
	/// </summary>
	/// <returns></returns>
	public int getMessageID()
	{
		return m_MessageID;
	}
	/// <summary>
	/// Gets the req message I.
	/// 获取请求消息的id;
	/// </summary>
	/// <returns>The req message I.</returns>
	public int getReqMessageID()
	{
		return m_MessageID & 0x0ffffff0;
	}
	/// <summary>
	/// Gets the req result.
	/// 获取消息包请求结果;
	/// </summary>
	/// <returns>The req result.</returns>
	public int getReqResult()
	{
		return m_MessageID & 0x0000000f;
	}
	//服务器用的。;
	public void SetResult(int result)
	{
		int msgID = m_MessageID & 0x0ffffff0;
		int backID =  msgID | result;
		m_MessageID = backID;
		Byte[] byteArray =  null;
		byteArray = BitConverter.GetBytes(backID);
		Array.Copy(byteArray, 0, m_Buffer, PACK_MESSSAGEID_OFFSET, byteArray.Length);

	}

	/// <summary>
	/// 获取消息内容;
	/// </summary>
	/// <returns></returns>
	/// 
	/// 
	public Byte[] getBody()
	{
		if (m_Buffer == null || m_Buffer.Length <= PACK_HEAD_SIZE)
		{
			Debuger.LogError("get body error  m_buffer ==null or  buffer.loength=PACK_HEAD_SIZE");
			return null;
		}

		///此处被迫写了一个拷贝;
		Byte[] data = new Byte[m_Buffer.Length - PACK_HEAD_SIZE];
		Array.Copy(m_Buffer, PACK_HEAD_SIZE, data, 0, data.Length);
		return data;
	}
		

	public bool setPackHead(Byte[] data)
	{
		if (data == null || data.Length < PACK_HEAD_SIZE)
		{
			return false;
		}

		if (m_Buffer == null || m_Buffer.Length < PACK_HEAD_SIZE)
		{
			return false;
		}

		Array.Copy(data, 0, m_Buffer, 0, data.Length);
		int messageid = BitConverter.ToInt32(data, PACK_MESSSAGEID_OFFSET);
		m_MessageID = messageid;
		return true;

	}

	public bool setUserData2(int userdata)
	{
		if (m_Buffer == null || m_Buffer.Length < PACK_HEAD_SIZE)
		{
			return false;
		}
		byte[] byteArray = BitConverter.GetBytes(userdata);
		Array.Copy(byteArray, 0, m_Buffer, PACK_SERVERID_OFFSET, byteArray.Length);
		return true;

	}


	public bool setUserData1(int userdata)
	{
		if (m_Buffer == null || m_Buffer.Length < PACK_HEAD_SIZE)
		{
			return false;
		}
		byte[] byteArray = BitConverter.GetBytes(userdata);
		Array.Copy(byteArray, 0, m_Buffer, PACK_USERDATA_OFFSET, byteArray.Length);
		return true;
	}


	public bool setData(Byte[] data)
	{
		if(data==null)
		{
			return true;
		}
		if ( m_Buffer == null || m_Buffer.Length < data.Length + PACK_HEAD_SIZE)
		{
			return false;
		}

		Array.Copy(data, 0, m_Buffer, PACK_MESSAGE_OFFSET, data.Length);
		return true;
	}


	/// <summary>
	/// 返回-1表示是不个无效消息包;
	/// </summary>
	public int playerId
	{
		get
		{
			if (m_Buffer == null || m_Buffer.Length < PACK_HEAD_SIZE)
			{
				return -1;
			}

			return BitConverter.ToInt32(m_Buffer, PACK_USERDATA_OFFSET);
		}

		set
		{
			setUserData1(value);
		}

	}



	public  int Size()
	{
		if (m_Buffer == null)
		{
			return 0;
		}
		else
		{
			return m_Buffer.Length;
		}

	}



	/// <summary>
	/// 返回-1表示无效值;
	/// </summary>
	public int serverId
	{
		get
		{
			if (m_Buffer == null || m_Buffer.Length < PACK_HEAD_SIZE)
			{
				return -1;
			}

			return BitConverter.ToInt32(m_Buffer, PACK_SERVERID_OFFSET);
		}

		set
		{
			setUserData2(value);
		}
	}



	/// <summary>
	/// 返回-1表示是一个无效的值;
	/// </summary>
	public int command
	{
		get
		{
			if (m_Buffer == null || m_Buffer.Length < PACK_HEAD_SIZE)
			{
				return -1;
			}

			return BitConverter.ToInt32(m_Buffer,PACK_MESSSAGEID_OFFSET);
		}

		set
		{
			if (m_Buffer == null || m_Buffer.Length < PACK_HEAD_SIZE)
			{
				return ;
			}

			Byte[] byteArray = BitConverter.GetBytes(value);
			Array.Copy(byteArray, 0, m_Buffer, PACK_MESSSAGEID_OFFSET, byteArray.Length);

		}

	}

}

