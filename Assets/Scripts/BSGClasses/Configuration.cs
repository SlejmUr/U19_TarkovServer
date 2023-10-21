namespace TarkovServerU19.BSGClasses
{
    public class Configuration
    { 
        // Token: 0x0400A939 RID: 43321
        public const int CONNECTION_LIMIT = 32;

        // Token: 0x0400A93A RID: 43322
        public int ConnectionLimit = 32;

        // Token: 0x0400A93B RID: 43323
        public const int PACKET_SIZE = 2500;

        // Token: 0x0400A93C RID: 43324
        public int PacketSize = 2500;

        // Token: 0x0400A93D RID: 43325
        public const int CONNECTING_TIMEOUT = 4000;

        // Token: 0x0400A93E RID: 43326
        public uint ConnectingTimeout = 4000U;

        // Token: 0x0400A93F RID: 43327
        public const int WAIT_TIMEOUT = 3000;

        // Token: 0x0400A940 RID: 43328
        public uint WaitTimeout = 3000U;

        // Token: 0x0400A941 RID: 43329
        public const int DISCONNECT_TIMEOUT = 12000;

        // Token: 0x0400A942 RID: 43330
        public uint DisconnectTimeout = 12000U;

        // Token: 0x0400A943 RID: 43331
        public const int PING_INTERVAL = 1000;

        // Token: 0x0400A944 RID: 43332
        public uint PingInterval = 1000U;

        // Token: 0x0400A945 RID: 43333
        public const int SEND_WINDOW_SIZE = 256;

        // Token: 0x0400A946 RID: 43334
        public uint SendWindowSize = 256U;

        // Token: 0x0400A947 RID: 43335
        public const int RECEIVE_WINDOW_SIZE = 256;

        // Token: 0x0400A948 RID: 43336
        public uint ReceiveWindowSize = 256U;

        // Token: 0x0400A949 RID: 43337
        public const int SEND_POOL_LIMIT = 8192;

        // Token: 0x0400A94A RID: 43338
        public int SendPoolLimit = 8192;

        // Token: 0x0400A94B RID: 43339
        public const int RECEIVE_POOL_LIMIT = 8192;

        // Token: 0x0400A94C RID: 43340
        public int ReceivePoolLimit = 8192;
    }
}