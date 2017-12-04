using System;
using System.IO;


public class NetSet
{
	public NetSet ()
	{
	}

	/// <summary>
	/// Deserializes the proto buffer.
	/// 反序列化，将套节字数据转成消息包;
	/// </summary>
	/// <returns>The proto buffer.</returns>
	/// <param name="packet">Packet.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static T DeserializeProtoBuf<T> (NetPacket packet) where T : class, ProtoBuf.IExtensible
	{
		Byte[] buffer = packet.getBuffer ();
		if (buffer == null || (buffer.Length-NetPacket.PACK_HEAD_SIZE) < 0) {
			return null;
		}
		using (MemoryStream ms = new MemoryStream(buffer,NetPacket.PACK_HEAD_SIZE,buffer.Length-NetPacket.PACK_HEAD_SIZE)) {              

			return new PBMessageSerializer().Deserialize(ms, null, typeof(T)) as T;
		}
	}

	// ===  /===== 通用     
	/// <summary>
	/// Gets the net packet.
	/// 根据消息ID和包体内容封装结构;
	/// </summary>
	/// <param name="msgID">Message I.</param>
	/// <param name="result">Result.</param>
	/// <param name="protoSt">Proto st.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public static NetPacket GetNetPacket<T> (GameMessage msgID, MessageANS result, T protoSt) where T : class, ProtoBuf.IExtensible
	{
		using (MemoryStream ms = new MemoryStream ()) {

			new PBMessageSerializer().Serialize (ms, protoSt);
			byte[] bytes = ms.ToArray ();

			NetPacket pack = new NetPacket ((int)msgID, bytes.Length);
			pack.SetResult ((int)result);
			pack.setData (bytes);

			return pack;
		}
	}

}

