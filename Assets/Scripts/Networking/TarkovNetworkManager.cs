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
            Debug.Log("RegisterMessages START");
            NetworkServer.RegisterHandler((short)147, new NetworkMessageDelegate(Loaded)); //Sending Request to join + OnAcceptResponse
            NetworkServer.RegisterHandler((short)148, new NetworkMessageDelegate(Loaded)); //OnRejectResponse (Contains Error in Int32)
            NetworkServer.RegisterHandler((short)168, new NetworkMessageDelegate(Loaded)); //battlEye packets
            NetworkServer.RegisterHandler((short)185, new NetworkMessageDelegate(Loaded)); //OnPartialCommand things | 0x020012D6 on latest (13.5)
            NetworkServer.RegisterHandler((short)188, new NetworkMessageDelegate(Loaded)); //The nightmare (ProfileId, Resourses Json, Customiation Json)
            NetworkServer.RegisterHandler((short)189, new NetworkMessageDelegate(Loaded)); //sync progress (to client)
            NetworkServer.RegisterHandler((short)190, new NetworkMessageDelegate(Loaded)); //sync progress (from client)
            Debug.Log("Registered Messages");

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
