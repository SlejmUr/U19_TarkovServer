using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace TarkovServerU19.Messages
{
    public class MessageManager
    {
        public static void ConnectionRequest(NetworkMessage msg)
        {
            Console.WriteLine(msg.channelId + " " + msg.msgType + " " + msg.conn.connectionId);

            var connectPackage = msg.ReadMessage<ConnectSentPacket>();
            Console.WriteLine(connectPackage.ToString());
            RejectConnection rejectConnection = new RejectConnection()
            { 
                RealErrorCode = RejectConnection.LocationMissmatch
            };
            msg.conn.Send(148, rejectConnection);
        }

    }
}
