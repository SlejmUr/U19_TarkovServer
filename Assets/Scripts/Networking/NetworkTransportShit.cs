using System;
using System.Net;
using TarkovServerU19.BSGClasses;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;

namespace TarkovServerU19.Networking
{
    internal class NetworkTransportShit : INetworkTransport
    {
        private static GClass2310 gclass2310_0 = new GClass2310();

        public bool IsStarted => gclass2310_0.IsStarted;

        public static void EarlyUpdate()
        {
            gclass2310_0.EarlyUpdate();
        }

        public static void LateUpdate()
        {
            gclass2310_0.LateUpdate();
        }

        public int AddHost(HostTopology topology, int port, string ip)
        {
            return gclass2310_0.AddHost(topology, port, ip);
        }

        public int AddHostWithSimulator(HostTopology topology, int minTimeout, int maxTimeout, int port)
        {
            throw new NotImplementedException();
        }

        public int AddWebsocketHost(HostTopology topology, int port, string ip)
        {
            throw new NotImplementedException();
        }

        public int Connect(int hostId, string address, int port, int specialConnectionId, out byte error)
        {
            return gclass2310_0.Connect(hostId, address, port, specialConnectionId, out error);
        }

        public void ConnectAsNetworkHost(int hostId, string address, int port, NetworkID network, SourceID source, NodeID node, out byte error)
        {
            throw new NotImplementedException();
        }

        public int ConnectEndPoint(int hostId, EndPoint endPoint, int specialConnectionId, out byte error)
        {
            throw new NotImplementedException();
        }

        public int ConnectToNetworkPeer(int hostId, string address, int port, int specialConnectionId, int relaySlotId, NetworkID network, SourceID source, NodeID node, out byte error)
        {
            throw new NotImplementedException();
        }

        public int ConnectWithSimulator(int hostId, string address, int port, int specialConnectionId, out byte error, ConnectionSimulatorConfig conf)
        {
            throw new NotImplementedException();
        }

        public bool Disconnect(int hostId, int connectionId, out byte error)
        {
            return gclass2310_0.Disconnect(hostId, connectionId, out error);
        }

        public bool DoesEndPointUsePlatformProtocols(EndPoint endPoint)
        {
            throw new NotImplementedException();
        }

        public void GetBroadcastConnectionInfo(int hostId, out string address, out int port, out byte error)
        {
            throw new NotImplementedException();
        }

        public void GetBroadcastConnectionMessage(int hostId, byte[] buffer, int bufferSize, out int receivedSize, out byte error)
        {
            throw new NotImplementedException();
        }

        public void GetConnectionInfo(int hostId, int connectionId, out string address, out int port, out NetworkID network, out NodeID dstNode, out byte error)
        {
            gclass2310_0.GetConnectionInfo(hostId, connectionId, out address, out port, out network, out dstNode, out error);
        }

        public int GetCurrentRTT(int hostId, int connectionId, out byte error)
        {
            return gclass2310_0.GetRtt(hostId, connectionId, out error);
        }

        public void Init()
        {
            NetworkTransport.Init();
        }

        public void Init(GlobalConfig config)
        {
            throw new NotImplementedException();
        }

        public NetworkEventType Receive(out int hostId, out int connectionId, out int channelId, byte[] buffer, int bufferSize, out int receivedSize, out byte error)
        {
            throw new NotImplementedException();
        }

        public NetworkEventType ReceiveFromHost(int hostId, out int connectionId, out int channelId, byte[] buffer, int bufferSize, out int receivedSize, out byte error)
        {
            return gclass2310_0.ReceiveFromHost(hostId, out connectionId, out channelId, buffer, out receivedSize, out error);
        }

        public NetworkEventType ReceiveRelayEventFromHost(int hostId, out byte error)
        {
            throw new NotImplementedException();
        }

        public bool RemoveHost(int hostId)
        {
            return gclass2310_0.RemoveHost(hostId);
        }

        public bool Send(int hostId, int connectionId, int channelId, byte[] buffer, int bufferSize, out byte error)
        {
            return gclass2310_0.Send(hostId, connectionId, channelId, buffer, bufferSize, out error);
        }

        public void SetBroadcastCredentials(int hostId, int key, int version, int subversion, out byte error)
        {
            throw new NotImplementedException();
        }

        public void SetPacketStat(int direction, int packetStatId, int numMsgs, int numBytes)
        {
        }

        public void Shutdown()
        {
            gclass2310_0.Shutdown();
        }

        public bool StartBroadcastDiscovery(int hostId, int broadcastPort, int key, int version, int subversion, byte[] buffer, int size, int timeout, out byte error)
        {
            throw new NotImplementedException();
        }

        public void StopBroadcastDiscovery()
        {
            throw new NotImplementedException();
        }

        public static int GetRtt(int hostId, int connectionId, out byte error)
        {
            return gclass2310_0.GetRtt(hostId, connectionId, out error);
        }

        public static int GetLossPercent(int hostId, int connectionId, out byte error)
        {
            return gclass2310_0.GetLossPercent(hostId, connectionId, out error);
        }

        public static int GetLossCount(int hostId, int connectionId, out byte error)
        {
            return gclass2310_0.GetLossCount(hostId, connectionId, out error);
        }

        public static GStruct335 GetStatistics(int hostId, int connectionId)
        {
            return gclass2310_0.GetStatistics(hostId, connectionId);
        }
    }
}
