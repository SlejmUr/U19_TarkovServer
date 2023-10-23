using System;
using TarkovServerU19.BSGEnums;

namespace TarkovServerU19.BSGClasses
{
    internal class ConnectiongConnection : AbstactConnection
    {
        private float float_0;

        public ConnectiongConnection(Connection connection)
            : base(connection)
        {
        }

        public override void Enter()
        {
            UnityEngine.Debug.Log("Enter to the 'Connecting' state (address: " + Connection.Address + ")");
            float_0 = Connection.CurrentTime;
        }

        public override void HandelReceive(MessageSegment message)
        {
            switch (message.Type)
            {
                default:
                    message.Dispose();
                    break;
                case NetworkMessageType.Data:
                    Connection.ReceiveQueue.Enqueue(message);
                    break;
                case NetworkMessageType.Connect:
                    Connection.ChangeState(new ConnectedConnection(Connection));
                    if (Convert.ToBoolean(message.Buffer.Array[0]))
                    {
                        Connection.SendConnect(syn: false, asc: true);
                    }
                    Connection.ReturnConnect();
                    message.Dispose();
                    break;
            }
        }

        public override void Update()
        {
            HandelTimeout();
        }

        public override void Send(MessageSegment message)
        {
            Connection.SendQueue.Enqueue(message);
        }

        public void HandelTimeout()
        {
            float num = (float)Connection.CurrentTime - float_0;
            if (num > (float)Connection.configuration.ConnectingTimeout)
            {
                UnityEngine.Debug.Log($"Timeout: Connection timed out after not receiving any message for {num}ms (address: {Connection.Address})");
                Disconnect();
            }
        }

        public override void Disconnect()
        {
            Connection.ReturnDisconnect();
            Connection.ChangeState(new DisconnectConnection(Connection));
        }
    }
}