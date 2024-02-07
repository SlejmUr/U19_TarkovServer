using UnityEngine;
using UnityEngine.Networking;
using TarkovServerU19.Messages;
using System.IO;
using System;

namespace TarkovServerU19.Networking
{
    public class TarkovNetworkManager : NetworkManager
    {
        public void RegisterMessages()
        {
            NetworkServer.RegisterHandler((short)MsgTypeEnum.ConnectionRequest, new NetworkMessageDelegate(MessageManager.ConnectionRequest)); //Sending Request to join + OnAcceptResponse
            NetworkServer.RegisterHandler((short)148, new NetworkMessageDelegate(Loaded)); //OnRejectResponse (Contains Error in Int32)
            NetworkServer.RegisterHandler((short)168, new NetworkMessageDelegate(Loaded)); //battlEye packets
            NetworkServer.RegisterHandler((short)185, new NetworkMessageDelegate(Loaded)); //OnPartialCommand things | 0x020012D6 on latest (13.5)
            NetworkServer.RegisterHandler((short)188, new NetworkMessageDelegate(Loaded)); //The nightmare (ProfileId, Resourses Json, Customiation Json)
            NetworkServer.RegisterHandler((short)189, new NetworkMessageDelegate(Loaded)); //sync progress (to client)
            NetworkServer.RegisterHandler((short)190, new NetworkMessageDelegate(Loaded)); //sync progress (from client)

        }
        public static void Loaded(NetworkMessage msg)
        {
            Debug.Log(msg.channelId + " " + msg.msgType + " " + msg.conn.connectionId);
            var bytes = msg.reader.ReadBytes(msg.reader.Length);
            File.WriteAllBytes($"{msg.channelId}_{msg.msgType}_{msg.conn.connectionId}_{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}",bytes);
        }
    }
}
