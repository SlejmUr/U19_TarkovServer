namespace TarkovServerU19.BSGClasses
{
    public class Configuration
    { 
        public const int CONNECTION_LIMIT = 32;
        public int ConnectionLimit = 32;
        public const int PACKET_SIZE = 2500;
        public int PacketSize = 2500;
        public const int CONNECTING_TIMEOUT = 4000;
        public uint ConnectingTimeout = 4000U;
        public const int WAIT_TIMEOUT = 3000;
        public uint WaitTimeout = 3000U;
        public const int DISCONNECT_TIMEOUT = 12000;
        public uint DisconnectTimeout = 12000U;
        public const int PING_INTERVAL = 1000;
        public uint PingInterval = 1000U;
        public const int SEND_WINDOW_SIZE = 256;
        public uint SendWindowSize = 256U;
        public const int RECEIVE_WINDOW_SIZE = 256;
        public uint ReceiveWindowSize = 256U;
        public const int SEND_POOL_LIMIT = 8192;
        public int SendPoolLimit = 8192;
        public const int RECEIVE_POOL_LIMIT = 8192;
        public int ReceivePoolLimit = 8192;
    }
}