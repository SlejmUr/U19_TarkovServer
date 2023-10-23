using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace TarkovServerU19.Networking
{
    public class TarkovNetworkManager : NetworkManager
    {
        public void RegisterMessages()
        {
            NetworkServer.RegisterHandler(32, Loaded); //OnConnect
            NetworkServer.RegisterHandler(33, Loaded); //OnDisconnect
            NetworkServer.RegisterHandler(35, Loaded); //MsgType.Ready
            NetworkServer.RegisterHandler(36, Loaded); //MsgType.NotReady
            NetworkServer.RegisterHandler(147, Loaded); //Sending Request to join + OnAcceptResponse
            NetworkServer.RegisterHandler(148, Loaded); //OnRejectResponse (Contains Error in Int32)
            NetworkServer.RegisterHandler(168, Loaded); //battlEye packets
            NetworkServer.RegisterHandler(185, Loaded); //OnPartialCommand things | 0x020012D6 on latest (13.5)
            NetworkServer.RegisterHandler(188, Loaded); //The nightmare (ProfileId, Resourses Json, Customiation Json)
            NetworkServer.RegisterHandler(189, Loaded); //sync progress (to client)
            NetworkServer.RegisterHandler(190, Loaded); //sync progress (from client)

        }
        public static void Loaded(NetworkMessage msg)
        {
            Debug.Log(msg.channelId + " " + msg.msgType + " " + msg.conn.connectionId);
        }

        public override void OnClientConnect(NetworkConnection conn)
        {
            Debug.Log("OnClientConnect");
            base.OnClientConnect(conn);
        }

        public override void OnClientDisconnect(NetworkConnection conn)
        {
            Debug.Log("OnClientDisconnect");
            base.OnClientDisconnect(conn);
        }

        public override void OnServerConnect(NetworkConnection conn)
        {
            Debug.Log("OnServerConnect");
            base.OnServerConnect(conn);
        }

        public override void OnClientError(NetworkConnection conn, int errorCode)
        {
            Debug.Log("OnClientError " + errorCode);
            base.OnClientError(conn, errorCode);
        }
    }
}
