using System;
using System.Collections.Generic;
using System.Linq;
using TarkovServerU19.BSGEnums;
using TarkovServerU19.Helpers;
using UnityEngine.Networking.Types;
using UnityEngine.Networking;
using UnityEngine;

namespace TarkovServerU19.BSGClasses
{
    internal class TransportManager
    {
        private class QosManager
        {
            private List<QosType> QosTypes = new List<QosType>();

            private Dictionary<QosType, int> QosDict = new Dictionary<QosType, int>();

            public QosManager(HostTopology topology)
            {
                for (int i = 0; i < topology.DefaultConfig.Channels.Count; i++)
                {
                    QosType qOS = topology.DefaultConfig.Channels[i].QOS;
                    QosTypes.Add(qOS);
                    if (QosDict.ContainsKey(qOS))
                    {
                        QosDict.Add(qOS, i);
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
                return Convert(QosTypes[outsideChannel]);
            }

            public int ConvertIdentifier(NetworkChannel insideNetworkChannel)
            {
                if (QosDict.TryGetValue(Convert(insideNetworkChannel), out var value))
                {
                    return value;
                }
                return 0;
            }
        }

        private static class NetworkTypeConverter
        {
            public static NetworkMessageType Convert(NetworkEventType value)
            {

                switch (value)
                {
                    case NetworkEventType.DataEvent:
                        return NetworkMessageType.Data;
                    case NetworkEventType.ConnectEvent:
                        return NetworkMessageType.Connect;
                    case NetworkEventType.DisconnectEvent:
                        return NetworkMessageType.Disconnect;
                    default:
                        return NetworkMessageType.None;
                }
            }

            public static NetworkEventType Convert(NetworkMessageType value)
            {
                switch (value)
                {
                    case NetworkMessageType.Connect:
                        return NetworkEventType.ConnectEvent;
                    case NetworkMessageType.Data:
                        return NetworkEventType.DataEvent;
                    case NetworkMessageType.Disconnect:
                        return NetworkEventType.DisconnectEvent;
                    default:
                        return NetworkEventType.Nothing;
                }
            }
        }

        private Dictionary<int, NetworkHoster> IndexToHoster = new Dictionary<int, NetworkHoster>();

        private int hostCount;

        private QosManager qosManager;

        private int Hosts => hostCount++;

        public bool IsStarted => IndexToHoster.Count > 0;

        public void EarlyUpdate()
        {
        }

        public void LateUpdate()
        {
        }

        private bool GetHost(int index, out NetworkHoster host, out byte error)
        {
            if (IndexToHoster.TryGetValue(index, out host))
            {
                error = 0;
                return true;
            }
            error = 1;
            return false;
        }

        public int AddHost(HostTopology topology, int port, string ip)
        {
            NetworkHoster gClass = new NetworkHoster(topology, Hosts, port, ip);
            qosManager = new QosManager(topology);
            IndexToHoster.Add(gClass.Index, gClass);
            return gClass.Index;
        }

        public bool RemoveHost(int index)
        {
            if (GetHost(index, out var host, out var _))
            {
                host.Shutdown();
                IndexToHoster.Remove(host.Index);
                return true;
            }
            return false;
        }

        public int Connect(int hostId, string address, int port, int specialConnectionId, out byte error)
        {
            if (GetHost(hostId, out var host, out error))
            {
                return host.Connect(address, port, out error);
            }
            return 0;
        }

        public bool Disconnect(int hostId, int connectionId, out byte error)
        {
            if (GetHost(hostId, out var host, out error))
            {
                return host.Disconnect(connectionId, out error);
            }
            return false;
        }

        public void GetConnectionInfo(int hostId, int connectionId, out string address, out int port, out NetworkID network, out NodeID dstNode, out byte error)
        {
            network = NetworkID.Invalid;
            dstNode = NodeID.Invalid;
            if (GetHost(hostId, out var host, out error))
            {
                host.GetConnectionInfo(connectionId, out address, out port, out error);
                return;
            }
            address = null;
            port = 0;
        }

        public int GetRtt(int hostId, int connectionId, out byte error)
        {
            if (GetHost(hostId, out var host, out error))
            {
                return host.GetRtt(connectionId, out error);
            }
            return 0;
        }

        public int GetLossPercent(int hostId, int connectionId, out byte error)
        {
            if (GetHost(hostId, out var host, out error))
            {
                return host.GetLossPercent(connectionId, out error);
            }
            return 0;
        }

        public int GetLossCount(int hostId, int connectionId, out byte error)
        {
            if (GetHost(hostId, out var host, out error))
            {
                return host.GetLossCount(connectionId, out error);
            }
            return 0;
        }

        public NetworkEventType ReceiveFromHost(int hostId, out int connectionId, out int channelId, byte[] buffer, out int bufferSize, out byte error)
        {
            if (GetHost(hostId, out var host, out error) && host.Receive(out connectionId, out var type, out var channel, buffer, out bufferSize, out error))
            {
                NetworkEventType result = NetworkTypeConverter.Convert(type);
                channelId = qosManager.ConvertIdentifier(channel);
                return result;
            }
            connectionId = 0;
            channelId = 0;
            bufferSize = 0;
            return NetworkEventType.Nothing;
        }

        public bool Send(int hostId, int connectionId, int channelId, byte[] buffer, int bufferSize, out byte error)
        {
            if (GetHost(hostId, out var host, out error))
            {
                NetworkChannel channel = qosManager.ConvertIdentifier(channelId);
                return host.Send(connectionId, channel, buffer, bufferSize, out error);
            }
            return false;
        }

        public void Shutdown()
        {
            KeyValuePair<int, NetworkHoster>[] array = IndexToHoster.ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                var (key, gClass2) = array[i];
                gClass2.Shutdown();
                IndexToHoster.Remove(key);
            }
        }

        public UDPStats GetStatistics(int hostId, int connectionId)
        {
            if (GetHost(hostId, out var host, out var _))
            {
                return host.GetStatistics(connectionId);
            }
            return default(UDPStats);
        }
    }
}
