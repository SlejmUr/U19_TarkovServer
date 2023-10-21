namespace TarkovServerU19.BSGClasses
{
    internal class GClass2297 : GClass2292
    {
        public GClass2297(Connection connection)
            : base(connection)
        {
        }

        public override void Enter()
        {
            //Connection.Logger.LogInfo("Enter to the 'Disconnected' state (address: " + Connection.Address + ")");
            Connection.Clear();
        }
    }
}