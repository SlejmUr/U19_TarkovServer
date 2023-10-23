using System;
using System.Net;
using TarkovServerU19.BSGClasses;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;

namespace TarkovServerU19.Networking
{
    internal class TarkovNetworkTransport : INetworkTransport
    {
        private static TransportManager transportManager = new TransportManager();

        public bool IsStarted => transportManager.IsStarted;

        public static void EarlyUpdate()
        {
            transportManager.EarlyUpdate();
        }

        public static void LateUpdate()
        {
            transportManager.LateUpdate();
        }

        public int AddHost(HostTopology topology, int port, string ip)
        {
            return transportManager.AddHost(topology, port, ip);
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
            return transportManager.Connect(hostId, address, port, specialConnectionId, out error);
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
            return transportManager.Disconnect(hostId, connectionId, out error);
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
            transportManager.GetConnectionInfo(hostId, connectionId, out address, out port, out network, out dstNode, out error);
        }

        public int GetCurrentRTT(int hostId, int connectionId, out byte error)
        {
            return transportManager.GetRtt(hostId, connectionId, out error);
        }

        public void Init()
        {
            NetworkTransport.Init();
        }

        public void Init(GlobalConfig config)
        {
            NetworkTransport.Init(config);
        }

        public NetworkEventType Receive(out int hostId, out int connectionId, out int channelId, byte[] buffer, int bufferSize, out int receivedSize, out byte error)
        {
            throw new NotImplementedException();
        }

        public NetworkEventType ReceiveFromHost(int hostId, out int connectionId, out int channelId, byte[] buffer, int bufferSize, out int receivedSize, out byte error)
        {
            return transportManager.ReceiveFromHost(hostId, out connectionId, out channelId, buffer, out receivedSize, out error);
        }

        public NetworkEventType ReceiveRelayEventFromHost(int hostId, out byte error)
        {
            throw new NotImplementedException();
        }

        public bool RemoveHost(int hostId)
        {
            return transportManager.RemoveHost(hostId);
        }

        public bool Send(int hostId, int connectionId, int channelId, byte[] buffer, int bufferSize, out byte error)
        {
            return transportManager.Send(hostId, connectionId, channelId, buffer, bufferSize, out error);
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
            transportManager.Shutdown();
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
            return transportManager.GetRtt(hostId, connectionId, out error);
        }

        public static int GetLossPercent(int hostId, int connectionId, out byte error)
        {
            return transportManager.GetLossPercent(hostId, connectionId, out error);
        }

        public static int GetLossCount(int hostId, int connectionId, out byte error)
        {
            return transportManager.GetLossCount(hostId, connectionId, out error);
        }

        public static UDPStats GetStatistics(int hostId, int connectionId)
        {
            return transportManager.GetStatistics(hostId, connectionId);
        }
    }
}
