using System.Collections.Generic;
using UnityEngine.Networking;

namespace TarkovServerU19
{
    public class ServerHelper
    {
        public static ConnectionConfig GetConnectionConfig()
        {
            ConnectionConfig connectionConfig = CreateConnection();
            for (int i = 0; i < 102; i++)
            {
                byte Reliable = connectionConfig.AddChannel(QosType.Reliable);
                byte Unreliable = connectionConfig.AddChannel(QosType.Unreliable);
                connectionConfig.MakeChannelsSharedOrder(new List<byte>
            {
                Reliable,
                Unreliable
            });
            }
            return connectionConfig;
        }

        private static ConnectionConfig CreateConnection()
        {
            return new ConnectionConfig
            {
                NetworkDropThreshold = 25,
                OverflowDropThreshold = 25,
                AcksType = ConnectionAcksType.Acks128,
                MaxSentMessageQueueSize = 128,
                DisconnectTimeout = 3000U,
                Channels =
                {
                    new ChannelQOS(QosType.ReliableSequenced),
                    new ChannelQOS(QosType.ReliableSequenced),
                    new ChannelQOS(QosType.ReliableSequenced)
                }
            };
        }
    }
}
