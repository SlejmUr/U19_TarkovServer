using System.Net.Sockets;
using System.Net;
using System;
using TarkovServerU19.Helpers;

namespace TarkovServerU19.BSGClasses
{
    public class Socket
    {
        private System.Net.Sockets.Socket socket_0;

        public Socket(int port)
        {
            socket_0 = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket_0.IOControl(-1744830452, new byte[1], null);
            socket_0.ReceiveBufferSize = 11468800;
            socket_0.SendBufferSize = 11468800;
            socket_0.Bind(new IPEndPoint(IPAddress.Any, port));
        }

        public void Send(byte[] buffer, int offset, int size, RemoteEndPoint address)
        {
            socket_0.SendToNonAlloc(buffer, offset, size, SocketFlags.DontRoute, address);
        }

        public bool Receive(byte[] buffer, out int size, RemoteEndPoint address)
        {
            if (socket_0.Poll(0, SelectMode.SelectRead))
            {
                try
                {
                    size = socket_0.ReceiveFromNonAlloc(buffer, 0, buffer.Length, SocketFlags.None, address);
                    return true;
                }
                catch (Exception)
                {
                }
            }
            size = 0;
            return false;
        }

        public void Shutdown()
        {
            socket_0.Close();
        }
    }
}