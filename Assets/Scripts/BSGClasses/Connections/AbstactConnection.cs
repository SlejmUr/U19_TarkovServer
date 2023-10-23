namespace TarkovServerU19.BSGClasses
{
    public abstract class AbstactConnection
    {
        protected AbstactConnection(Connection connection)
        {
            this.Connection = connection;
        }
        public virtual void Enter()
        {
        }
        public virtual void Exit()
        {
        }
        public virtual void HandelReceive(MessageSegment message)
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Connect()
        {
        }
        public virtual void Disconnect()
        {
        }

        public virtual void Send(MessageSegment message)
        {
        }
        public Connection Connection;
    }

}
