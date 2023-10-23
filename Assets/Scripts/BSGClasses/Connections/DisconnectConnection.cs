namespace TarkovServerU19.BSGClasses
{
    public class DisconnectConnection : AbstactConnection
    {
        public DisconnectConnection(Connection connection)
            : base(connection)
        {
        }

        public override void Enter()
        {
            UnityEngine.Debug.Log("Enter to the 'Disconnected' state (address: " + Connection.Address + ")");
            Connection.Clear();
        }
    }
}