using TarkovServerU19.BSGEnums;

namespace TarkovServerU19.BSGClasses
{
    internal class InitialConenction : AbstactConnection
    {
        public InitialConenction(Connection connection)
                : base(connection)
        {
        }

        public override void Exit()
        {
            UnityEngine.Debug.Log("Exit to the 'Initial' state (address: " + Connection.Address + ")");
        }

        public override void Connect()
        {
            Connection.ChangeState(new ConnectiongConnection(Connection));
            Connection.SendConnect(syn: true, asc: false);
        }

        public override void HandelReceive(MessageSegment message)
        {
            NetworkMessageType type = message.Type;
            if (type == NetworkMessageType.Connect)
            {
                Connection.ChangeState(new ConnectiongConnection(Connection));
                Connection.SendConnect(syn: true, asc: true);
                message.Dispose();
            }
            else
            {
                message.Dispose();
            }
        }
    }
}
