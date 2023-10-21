namespace TarkovServerU19.BSGClasses
{
    internal class GClass2296 : GClass2292
    {
        public GClass2296(Connection connection) : base(connection)
        {
        }
        public override void Enter()
        {
            //Connection.Logger.LogInfo("Enter to the 'Waiting' state (address: " + Connection.Address + ")");
        }

        public override void HandelReceive(MessageSegment message)
        {
            GClass2295 gClass = new GClass2295(Connection);
            Connection.ChangeState(gClass);
            gClass.HandelReceive(message);
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
            Connection.ChangeState(new GClass2297(Connection));
        }

        public override void Send(MessageSegment message)
        {
            Connection.SendQueue.Enqueue(message);
        }

        public void HandelTimeout()
        {
            uint num = Connection.CurrentTime - Connection.LastReceiveTime;
            if (num > Connection.GClass2290_0.DisconnectTimeout)
            {
                //Connection.Logger.LogError($"Timeout: Messages timed out after not receiving any message for {num}ms (address: {Connection.Address})");
                Disconnect();
            }
        }
    }
}