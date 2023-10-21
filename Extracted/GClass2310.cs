// GClass2310
using System.Collections.Generic;
using System.Linq;
using EFT.KcpNetwork;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;

public class GClass2310
{
	private class Class1917
	{
		private List<QosType> list_0 = new List<QosType>();

		private Dictionary<QosType, int> dictionary_0 = new Dictionary<QosType, int>();

		public Class1917(HostTopology topology)
		{
			for (int i = 0; i < topology.DefaultConfig.Channels.Count; i++)
			{
				QosType qOS = topology.DefaultConfig.Channels[i].QOS;
				list_0.Add(qOS);
				if (dictionary_0.ContainsKey(qOS))
				{
					dictionary_0.Add(qOS, i);
				}
			}
		}

		public static NetworkChannel Convert(QosType value)
		{
			switch (value)
			{
			case QosType.Unreliable:
			case QosType.UnreliableFragmented:
			case QosType.UnreliableSequenced:
			case QosType.UnreliableFragmentedSequenced:
				return NetworkChannel.Unreliable;
			default:
				return NetworkChannel.Reliable;
			}
		}

		public static QosType Convert(NetworkChannel value)
		{
			if (value != NetworkChannel.Reliable && value == NetworkChannel.Unreliable)
			{
				return QosType.Unreliable;
			}
			return QosType.Reliable;
		}

		public NetworkChannel ConvertIdentifier(int outsideChannel)
		{
			return Convert(list_0[outsideChannel]);
		}

		public int ConvertIdentifier(NetworkChannel insideNetworkChannel)
		{
			if (dictionary_0.TryGetValue(Convert(insideNetworkChannel), out var value))
			{
				return value;
			}
			return 0;
		}
	}

	private static class Class1918
	{
		public static NetworkMessageType Convert(NetworkEventType value)
		{
			return value switch
			{
				NetworkEventType.DataEvent => NetworkMessageType.Data, 
				NetworkEventType.ConnectEvent => NetworkMessageType.Connect, 
				NetworkEventType.DisconnectEvent => NetworkMessageType.Disconnect, 
				_ => NetworkMessageType.None, 
			};
		}

		public static NetworkEventType Convert(NetworkMessageType value)
		{
			return value switch
			{
				NetworkMessageType.Connect => NetworkEventType.ConnectEvent, 
				NetworkMessageType.Data => NetworkEventType.DataEvent, 
				NetworkMessageType.Disconnect => NetworkEventType.DisconnectEvent, 
				_ => NetworkEventType.Nothing, 
			};
		}
	}

	private Dictionary<int, GClass2298> dictionary_0 = new Dictionary<int, GClass2298>();

	private int int_0;

	private Class1917 class1917_0;

	private int Int32_0 => int_0++;

	public bool IsStarted => dictionary_0.Count > 0;

	public void EarlyUpdate()
	{
	}

	public void LateUpdate()
	{
	}

	private bool method_0(int index, out GClass2298 host, out byte error)
	{
		if (dictionary_0.TryGetValue(index, out host))
		{
			error = 0;
			return true;
		}
		error = 1;
		return false;
	}

	public int AddHost(HostTopology topology, int port, string ip)
	{
		GClass2298 gClass = new GClass2298(topology, Int32_0, port, ip);
		class1917_0 = new Class1917(topology);
		dictionary_0.Add(gClass.Index, gClass);
		return gClass.Index;
	}

	public bool RemoveHost(int index)
	{
		if (method_0(index, out var host, out var _))
		{
			host.Shutdown();
			dictionary_0.Remove(host.Index);
			return true;
		}
		return false;
	}

	public int Connect(int hostId, string address, int port, int specialConnectionId, out byte error)
	{
		if (method_0(hostId, out var host, out error))
		{
			return host.Connect(address, port, out error);
		}
		return 0;
	}

	public bool Disconnect(int hostId, int connectionId, out byte error)
	{
		if (method_0(hostId, out var host, out error))
		{
			return host.Disconnect(connectionId, out error);
		}
		return false;
	}

	public void GetConnectionInfo(int hostId, int connectionId, out string address, out int port, out NetworkID network, out NodeID dstNode, out byte error)
	{
		network = NetworkID.Invalid;
		dstNode = NodeID.Invalid;
		if (method_0(hostId, out var host, out error))
		{
			host.GetConnectionInfo(connectionId, out address, out port, out error);
			return;
		}
		address = null;
		port = 0;
	}

	public int GetRtt(int hostId, int connectionId, out byte error)
	{
		if (method_0(hostId, out var host, out error))
		{
			return host.GetRtt(connectionId, out error);
		}
		return 0;
	}

	public int GetLossPercent(int hostId, int connectionId, out byte error)
	{
		if (method_0(hostId, out var host, out error))
		{
			return host.GetLossPercent(connectionId, out error);
		}
		return 0;
	}

	public int GetLossCount(int hostId, int connectionId, out byte error)
	{
		if (method_0(hostId, out var host, out error))
		{
			return host.GetLossCount(connectionId, out error);
		}
		return 0;
	}

	public NetworkEventType ReceiveFromHost(int hostId, out int connectionId, out int channelId, byte[] buffer, out int bufferSize, out byte error)
	{
		if (method_0(hostId, out var host, out error) && host.Receive(out connectionId, out var type, out var channel, buffer, out bufferSize, out error))
		{
			NetworkEventType result = Class1918.Convert(type);
			channelId = class1917_0.ConvertIdentifier(channel);
			return result;
		}
		connectionId = 0;
		channelId = 0;
		bufferSize = 0;
		return NetworkEventType.Nothing;
	}

	public bool Send(int hostId, int connectionId, int channelId, byte[] buffer, int bufferSize, out byte error)
	{
		if (method_0(hostId, out var host, out error))
		{
			NetworkChannel channel = class1917_0.ConvertIdentifier(channelId);
			return host.Send(connectionId, channel, buffer, bufferSize, out error);
		}
		return false;
	}

	public void Shutdown()
	{
		KeyValuePair<int, GClass2298>[] array = dictionary_0.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			var (key, gClass2) = array[i];
			gClass2.Shutdown();
			dictionary_0.Remove(key);
		}
	}

	public GStruct335 GetStatistics(int hostId, int connectionId)
	{
		if (method_0(hostId, out var host, out var _))
		{
			return host.GetStatistics(connectionId);
		}
		return default(GStruct335);
	}
}
