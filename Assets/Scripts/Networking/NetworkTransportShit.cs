using System;
using System.Net;
using TarkovServerU19.BSGClasses;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;

namespace TarkovServerU19.Networking
{
    internal class NetworkTransportShit : INetworkTransport
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
            Debug.Log("AddHost");
            return transportManager.AddHost(topology, port, ip);
        }

        public int AddHostWithSimulator(HostTopology topology, int minTimeout, int maxTimeout, int port)
        {
            Debug.Log("AddHostWithSimulator");
            throw new NotImplementedException();
        }

        public int AddWebsocketHost(HostTopology topology, int port, string ip)
        {
            Debug.Log("AddWebsocketHost");
            throw new NotImplementedException();
        }

        public int Connect(int hostId, string address, int port, int specialConnectionId, out byte error)
        {
            Debug.Log("Connect");
            return transportManager.Connect(hostId, address, port, specialConnectionId, out error);
        }

        public void ConnectAsNetworkHost(int hostId, string address, int port, NetworkID network, SourceID source, NodeID node, out byte error)
        {
            Debug.Log("ConnectAsNetworkHost");
            throw new NotImplementedException();
        }

        public int ConnectEndPoint(int hostId, EndPoint endPoint, int specialConnectionId, out byte error)
        {
            Debug.Log("ConnectEndPoint");
            throw new NotImplementedException();
        }

        public int ConnectToNetworkPeer(int hostId, string address, int port, int specialConnectionId, int relaySlotId, NetworkID network, SourceID source, NodeID node, out byte error)
        {
            Debug.Log("ConnectToNetworkPeer");
            throw new NotImplementedException();
        }

        public int ConnectWithSimulator(int hostId, string address, int port, int specialConnectionId, out byte error, ConnectionSimulatorConfig conf)
        {
            Debug.Log("ConnectWithSimulator");
            throw new NotImplementedException();
        }

        public bool Disconnect(int hostId, int connectionId, out byte error)
        {
            Debug.Log("Disconnect");
            return transportManager.Disconnect(hostId, connectionId, out error);
        }

        public bool DoesEndPointUsePlatformProtocols(EndPoint endPoint)
        {
            Debug.Log("DoesEndPointUsePlatformProtocols");
            throw new NotImplementedException();
        }

        public void GetBroadcastConnectionInfo(int hostId, out string address, out int port, out byte error)
        {
            Debug.Log("GetBroadcastConnectionInfo");
            throw new NotImplementedException();
        }

        public void GetBroadcastConnectionMessage(int hostId, byte[] buffer, int bufferSize, out int receivedSize, out byte error)
        {
            Debug.Log("GetBroadcastConnectionMessage");
            throw new NotImplementedException();
        }

        public void GetConnectionInfo(int hostId, int connectionId, out string address, out int port, out NetworkID network, out NodeID dstNode, out byte error)
        {
            Debug.Log("GetConnectionInfo");
            transportManager.GetConnectionInfo(hostId, connectionId, out address, out port, out network, out dstNode, out error);
        }

        public int GetCurrentRTT(int hostId, int connectionId, out byte error)
        {
            Debug.Log("GetCurrentRTT");
            return transportManager.GetRtt(hostId, connectionId, out error);
        }

        public void Init()
        {
            Debug.Log("Init");
            NetworkTransport.Init();
        }

        public void Init(GlobalConfig config)
        {
            Debug.Log("Init2");
            NetworkTransport.Init(config);
        }

        public NetworkEventType Receive(out int hostId, out int connectionId, out int channelId, byte[] buffer, int bufferSize, out int receivedSize, out byte error)
        {
            Debug.Log("Receive");
            throw new NotImplementedException();
        }

        public NetworkEventType ReceiveFromHost(int hostId, out int connectionId, out int channelId, byte[] buffer, int bufferSize, out int receivedSize, out byte error)
        {
            Debug.Log("ReceiveFromHost");
            return transportManager.ReceiveFromHost(hostId, out connectionId, out channelId, buffer, out receivedSize, out error);
        }

        public NetworkEventType ReceiveRelayEventFromHost(int hostId, out byte error)
        {
            Debug.Log("ReceiveRelayEventFromHost");
            throw new NotImplementedException();
        }

        public bool RemoveHost(int hostId)
        {
            Debug.Log("RemoveHost");
            return transportManager.RemoveHost(hostId);
        }

        public bool Send(int hostId, int connectionId, int channelId, byte[] buffer, int bufferSize, out byte error)
        {
            Debug.Log("Send");
            return transportManager.Send(hostId, connectionId, channelId, buffer, bufferSize, out error);
        }

        public void SetBroadcastCredentials(int hostId, int key, int version, int subversion, out byte error)
        {
            Debug.Log("SetBroadcastCredentials");
            throw new NotImplementedException();
        }

        public void SetPacketStat(int direction, int packetStatId, int numMsgs, int numBytes)
        {
            Debug.Log("SetPacketStat");
        }

        public void Shutdown()
        {
            Debug.Log("Shutdown");
            transportManager.Shutdown();
        }

        public bool StartBroadcastDiscovery(int hostId, int broadcastPort, int key, int version, int subversion, byte[] buffer, int size, int timeout, out byte error)
        {
            Debug.Log("StartBroadcastDiscovery");
            throw new NotImplementedException();
        }

        public void StopBroadcastDiscovery()
        {
            Debug.Log("StopBroadcastDiscovery");
            throw new NotImplementedException();
        }

        public static int GetRtt(int hostId, int connectionId, out byte error)
        {
            Debug.Log("GetRtt");
            return transportManager.GetRtt(hostId, connectionId, out error);
        }

        public static int GetLossPercent(int hostId, int connectionId, out byte error)
        {
            Debug.Log("GetLossPercent");
            return transportManager.GetLossPercent(hostId, connectionId, out error);
        }

        public static int GetLossCount(int hostId, int connectionId, out byte error)
        {
            Debug.Log("GetLossCount");
            return transportManager.GetLossCount(hostId, connectionId, out error);
        }

        public static UDPStats GetStatistics(int hostId, int connectionId)
        {
            Debug.Log("GetStatistics");
            return transportManager.GetStatistics(hostId, connectionId);
        }
    }
}
