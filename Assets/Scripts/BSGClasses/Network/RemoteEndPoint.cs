using System;
using System.Net;
using System.Net.Sockets;

namespace TarkovServerU19.BSGClasses
{
    public class RemoteEndPoint : IPEndPoint
    {
        public RemoteEndPoint(long address, int port) : base(address, port)
        {
            this.temp = base.Serialize();
        }

        public RemoteEndPoint(IPAddress address, int port) : base(address, port)
        {
            this.temp = base.Serialize();
        }

        public override SocketAddress Serialize()
        {
            return this.temp;
        }

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

        public override int GetHashCode()
        {
            return this.temp.GetHashCode();
        }

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

        public SocketAddress temp;
    }
}
