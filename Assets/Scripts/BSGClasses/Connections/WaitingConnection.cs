namespace TarkovServerU19.BSGClasses
{
    internal class WaitingConnection : AbstactConnection
    {
        public WaitingConnection(Connection connection) : base(connection)
        {
        }
        public override void Enter()
        {
            UnityEngine.Debug.Log("Enter to the 'Waiting' state (address: " + Connection.Address + ")");
        }

        public override void HandelReceive(MessageSegment message)
        {
            ConnectedConnection connectedCon = new ConnectedConnection(Connection);
            Connection.ChangeState(connectedCon);
            connectedCon.HandelReceive(message);
        }

        public override void Update()
        {
            HandelTimeout();
            Connection.HandlePing();
        }

        public override void Disconnect()
        {
            Connection.SendDisconnect();
            Connection.ReturnDisconnect();
            Connection.ChangeState(new DisconnectConnection(Connection));
        }

        public override void Send(MessageSegment message)
        {
            Connection.SendQueue.Enqueue(message);
        }

        public void HandelTimeout()
        {
            uint num = Connection.CurrentTime - Connection.LastReceiveTime;
            if (num > Connection.configuration.DisconnectTimeout)
            {
                UnityEngine.Debug.Log($"Timeout: Messages timed out after not receiving any message for {num}ms (address: {Connection.Address})");
                Disconnect();
            }
        }
    }
}