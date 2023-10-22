using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;

namespace TarkovServerU19.Networking
{
    public class TarkovTransportTest : INetworkTransport
    {
        public bool IsStarted
        {
            get
            {
                return NetworkTransport.IsStarted;
            }
        }

        public int AddHost(HostTopology topology, int port, string ip)
        {
            Debug.Log("AddHost");
            return NetworkTransport.AddHost(topology, port, ip);
        }

        public int AddHostWithSimulator(HostTopology topology, int minTimeout, int maxTimeout, int port)
        {
            Debug.Log("AddHostWithSimulator");
            return NetworkTransport.AddHostWithSimulator(topology, minTimeout, maxTimeout, port);
        }

        public int AddWebsocketHost(HostTopology topology, int port, string ip)
        {
            Debug.Log("AddWebsocketHost");
            return NetworkTransport.AddWebsocketHost(topology, port, ip);
        }

        public int Connect(int hostId, string address, int port, int specialConnectionId, out byte error)
        {
            Debug.Log("Connect");
            return NetworkTransport.Connect(hostId, address, port, specialConnectionId, out error);
        }

        public void ConnectAsNetworkHost(int hostId, string address, int port, NetworkID network, SourceID source, NodeID node, out byte error)
        {
            Debug.Log("ConnectAsNetworkHost");
            NetworkTransport.ConnectAsNetworkHost(hostId, address, port, network, source, node, out error);
        }

        public int ConnectEndPoint(int hostId, EndPoint endPoint, int specialConnectionId, out byte error)
        {
            Debug.Log("ConnectEndPoint");
            return NetworkTransport.ConnectEndPoint(hostId, endPoint, specialConnectionId, out error);
        }

        public int ConnectToNetworkPeer(int hostId, string address, int port, int specialConnectionId, int relaySlotId, NetworkID network, SourceID source, NodeID node, out byte error)
        {
            Debug.Log("ConnectToNetworkPeer");
            return NetworkTransport.ConnectToNetworkPeer(hostId, address, port, specialConnectionId, relaySlotId, network, source, node, out error);
        }

        public int ConnectWithSimulator(int hostId, string address, int port, int specialConnectionId, out byte error, ConnectionSimulatorConfig conf)
        {
            Debug.Log("ConnectWithSimulator");
            return NetworkTransport.ConnectWithSimulator(hostId, address, port, specialConnectionId, out error, conf);
        }

        public bool Disconnect(int hostId, int connectionId, out byte error)
        {
            Debug.Log("Disconnect");
            return NetworkTransport.Disconnect(hostId, connectionId, out error);
        }

        public bool DoesEndPointUsePlatformProtocols(EndPoint endPoint)
        {
            Debug.Log("DoesEndPointUsePlatformProtocols");
            return NetworkTransport.DoesEndPointUsePlatformProtocols(endPoint);
        }

        public void GetBroadcastConnectionInfo(int hostId, out string address, out int port, out byte error)
        {
            Debug.Log("GetBroadcastConnectionInfo");
            NetworkTransport.GetBroadcastConnectionInfo(hostId, out address, out port, out error);
        }

        public void GetBroadcastConnectionMessage(int hostId, byte[] buffer, int bufferSize, out int receivedSize, out byte error)
        {
            Debug.Log("GetBroadcastConnectionMessage");
            NetworkTransport.GetBroadcastConnectionMessage(hostId, buffer, bufferSize, out receivedSize, out error);
        }

        public void GetConnectionInfo(int hostId, int connectionId, out string address, out int port, out NetworkID network, out NodeID dstNode, out byte error)
        {
            Debug.Log("GetConnectionInfo");
            NetworkTransport.GetConnectionInfo(hostId, connectionId, out address, out port, out network, out dstNode, out error);
        }

        public int GetCurrentRTT(int hostId, int connectionId, out byte error)
        {
            Debug.Log("GetCurrentRTT");
            return NetworkTransport.GetCurrentRTT(hostId, connectionId, out error);
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
            return NetworkTransport.Receive(out hostId, out connectionId, out channelId, buffer, bufferSize, out receivedSize, out error);
        }

        public NetworkEventType ReceiveFromHost(int hostId, out int connectionId, out int channelId, byte[] buffer, int bufferSize, out int receivedSize, out byte error)
        {
            Debug.Log("ReceiveFromHost");
            return NetworkTransport.ReceiveFromHost(hostId, out connectionId, out channelId, buffer, bufferSize, out receivedSize, out error);
        }

        public NetworkEventType ReceiveRelayEventFromHost(int hostId, out byte error)
        {
            Debug.Log("ReceiveRelayEventFromHost");
            return NetworkTransport.ReceiveRelayEventFromHost(hostId, out error);
        }

        public bool RemoveHost(int hostId)
        {
            Debug.Log("RemoveHost");
            return NetworkTransport.RemoveHost(hostId);
        }

        public bool Send(int hostId, int connectionId, int channelId, byte[] buffer, int size, out byte error)
        {
            Debug.Log("Send");
            return NetworkTransport.Send(hostId, connectionId, channelId, buffer, size, out error);
        }

        public void SetBroadcastCredentials(int hostId, int key, int version, int subversion, out byte error)
        {
            Debug.Log("SetBroadcastCredentials");
            NetworkTransport.SetBroadcastCredentials(hostId, key, version, subversion, out error);
        }

        public void SetPacketStat(int direction, int packetStatId, int numMsgs, int numBytes)
        {
            Debug.Log("SetPacketStat");
            NetworkTransport.SetPacketStat(direction, packetStatId, numMsgs, numBytes);
        }

        public void Shutdown()
        {
            Debug.Log("Shutdown");
            NetworkTransport.Shutdown();
        }

        public bool StartBroadcastDiscovery(int hostId, int broadcastPort, int key, int version, int subversion, byte[] buffer, int size, int timeout, out byte error)
        {
            Debug.Log("StartBroadcastDiscovery");
            return NetworkTransport.StartBroadcastDiscovery(hostId, broadcastPort, key, version, subversion, buffer, size, timeout, out error);
        }

        public void StopBroadcastDiscovery()
        {
            Debug.Log("StopBroadcastDiscovery");
            NetworkTransport.StopBroadcastDiscovery();
        }
    }
}
