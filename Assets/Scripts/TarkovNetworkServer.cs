using System;
using UnityEngine.Networking;
using UnityEngine;

namespace TarkovServerU19
{
    public class TarkovNetworkServer : NetworkServerSimple
    {

        public override void OnError(int connectionId, byte error)
        {
            Debug.Log("OnError: " + connectionId +  " " + error);
            base.OnError(connectionId, error);
        }

        public override void OnConnected(NetworkConnection conn)
        {
            conn.logNetworkMessages = true;
            Debug.Log("Connected: " + conn.connectionId + " " + conn.address + " " + conn.hostId);
            base.OnConnected(conn);
        }

        public override void OnData(NetworkConnection conn, int receivedSize, int channelId)
        {
            conn.logNetworkMessages = true;
            Debug.Log("OnData:" + conn.connectionId + " " + conn.address + " " + conn.hostId + " " + receivedSize + " " + channelId);
            base.OnData(conn, receivedSize, channelId);
        }

        public override void OnDataError(NetworkConnection conn, byte error)
        {
            Debug.Log("OnDataError: " + conn.connectionId + " " + conn.address + " " + conn.hostId + " " + error);
            base.OnDataError(conn, error);
        }

        public override void OnDisconnected(NetworkConnection conn)
        {
            Debug.Log("Disconnected: " + conn.connectionId + " " + conn.address + " " + conn.hostId);
            base.OnDisconnected(conn);
        }
    }
}