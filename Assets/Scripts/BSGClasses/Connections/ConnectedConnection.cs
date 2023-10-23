using TarkovServerU19.BSGEnums;

namespace TarkovServerU19.BSGClasses
{
    internal class ConnectedConnection : AbstactConnection
    {
        public ConnectedConnection(Connection connection)
            : base(connection)
        {
        }

        public override void Enter()
        {
            UnityEngine.Debug.Log("Enter to the 'Connected' state (address: " + Connection.Address + ")");
            Connection.LastReceiveTime = Connection.CurrentTime;
            method_0();
        }

        public override void HandelReceive(MessageSegment message)
        {
            switch (message.Type)
            {
                default:
                    message.Dispose();
                    break;
                case NetworkMessageType.Ping:
                    Connection.HandlePingReceiving(message.Buffer.Array, message.Buffer.Count);
                    message.Dispose();
                    break;
                case NetworkMessageType.Pong:
                    Connection.HandlePongReceiving(message.Buffer.Array, message.Buffer.Count);
                    message.Dispose();
                    break;
                case NetworkMessageType.Data:
                    Connection.ReceiveQueue.Enqueue(message);
                    break;
                case NetworkMessageType.Disconnect:
                    UnityEngine.Debug.Log("Receive disconnect (address: " + Connection.Address + ")");
                    Connection.ReturnDisconnect();
                    Connection.ChangeState(new DisconnectConnection(Connection));
                    message.Dispose();
                    break;
            }
        }

        public override void Update()
        {
            HandelTimeout();
            Connection.HandlePing();
            Connection.HandleDeadLink();
            Connection.HandleOverflow();
            method_0();
        }

        public override void Disconnect()
        {
            Connection.SendDisconnect();
            Connection.ReturnDisconnect();
            Connection.ChangeState(new DisconnectConnection(Connection));
        }

        public override void Send(MessageSegment message)
        {
            Connection.SendFinite(message);
        }

        public void HandelTimeout()
        {
            uint num = Connection.CurrentTime - Connection.LastReceiveTime;
            if (num >= Connection.configuration.WaitTimeout)
            {
                UnityEngine.Debug.Log($"Timeout: Messages timed out after not receiving any message for {num}ms (address: {Connection.Address})");
                Connection.ChangeState(new WaitingConnection(Connection));
            }
        }

        private void method_0()
        {
            while (Connection.SendQueue.Count > 0)
            {
                MessageSegment message = Connection.SendQueue.Dequeue();
                Connection.SendFinite(message);
            }
        }
    }
}