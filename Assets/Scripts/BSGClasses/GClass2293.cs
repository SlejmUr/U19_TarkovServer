using TarkovServerU19.BSGEnums;

namespace TarkovServerU19.BSGClasses
{
    internal class GClass2293 : GClass2292
    {
        public GClass2293(Connection connection)
                : base(connection)
        {
        }

        public override void Exit()
        {
            //Connection.Logger.LogInfo("Exit to the 'Initial' state (address: " + Connection.Address + ")");
        }

        public override void Connect()
        {
            Connection.ChangeState(new GClass2294(Connection));
            Connection.SendConnect(syn: true, asc: false);
        }

        public override void HandelReceive(MessageSegment message)
        {
            NetworkMessageType type = message.Type;
            if (type == NetworkMessageType.Connect)
            {
                Connection.ChangeState(new GClass2294(Connection));
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
