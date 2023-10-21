using System;
using System.Net;
using System.Net.Sockets;

namespace TarkovServerU19.BSGClasses
{
    public class RemoteEndPoint : IPEndPoint
    {
        // Token: 0x0600BD4F RID: 48463 RVA: 0x00333038 File Offset: 0x00331238
        public RemoteEndPoint(long address, int port) : base(address, port)
        {
            this.temp = base.Serialize();
        }

        // Token: 0x0600BD50 RID: 48464 RVA: 0x0033304E File Offset: 0x0033124E
        public RemoteEndPoint(IPAddress address, int port) : base(address, port)
        {
            this.temp = base.Serialize();
        }

        // Token: 0x0600BD51 RID: 48465 RVA: 0x00333064 File Offset: 0x00331264
        public override SocketAddress Serialize()
        {
            return this.temp;
        }

        // Token: 0x0600BD52 RID: 48466 RVA: 0x0033306C File Offset: 0x0033126C
        public override EndPoint Create(SocketAddress socketAddress)
        {
            if (socketAddress.Family != this.AddressFamily)
            {
                throw new ArgumentException(string.Format("Unsupported socketAddress.AddressFamily: {0}. Expected: {1}", socketAddress.Family, this.AddressFamily));
            }
            if (socketAddress.Size < 8)
            {
                throw new ArgumentException(string.Format("Unsupported socketAddress.Size: {0}. Expected: <8", socketAddress.Size));
            }
            if (socketAddress != this.temp)
            {
                this.temp = socketAddress;
                SocketAddress socketAddress2 = this.temp;
                socketAddress2[0] = (byte)(socketAddress2[0] + 1);
                socketAddress2 = this.temp;
                socketAddress2[0] = (byte)(socketAddress2[0] - 1);
                if (this.temp.GetHashCode() == 0)
                {
                    throw new Exception("SocketAddress GetHashCode() is 0 after ReceiveFrom. Does the m_changed trick not work anymore?");
                }
            }
            return this;
        }

        // Token: 0x0600BD53 RID: 48467 RVA: 0x0033312A File Offset: 0x0033132A
        public override int GetHashCode()
        {
            return this.temp.GetHashCode();
        }

        // Token: 0x0600BD54 RID: 48468 RVA: 0x00333138 File Offset: 0x00331338
        public IPEndPoint DeepCopyIPEndPoint()
        {
            IPAddress address;
            if (this.temp.Family == AddressFamily.InterNetworkV6)
            {
                address = IPAddress.IPv6Any;
            }
            else
            {
                if (this.temp.Family != AddressFamily.InterNetwork)
                {
                    throw new Exception(string.Format("Unexpected SocketAddress family: {0}", this.temp.Family));
                }
                address = IPAddress.Any;
            }
            return (IPEndPoint)new IPEndPoint(address, 0).Create(this.temp);
        }

        // Token: 0x0400A938 RID: 43320
        public SocketAddress temp;
    }
}
