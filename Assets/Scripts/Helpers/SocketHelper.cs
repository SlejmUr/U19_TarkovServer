using System.Net.Sockets;
using System.Net;
using TarkovServerU19.BSGClasses;

namespace TarkovServerU19.Helpers
{
    public static class SocketHelper
    {
        public static int ReceiveFromNonAlloc(this System.Net.Sockets.Socket socket, byte[] buffer, int offset, int size, SocketFlags socketFlags, RemoteEndPoint remoteEndPoint)
        {
            EndPoint remoteEP = remoteEndPoint;
            return socket.ReceiveFrom(buffer, offset, size, socketFlags, ref remoteEP);
        }

        public static int ReceiveFromNonAlloc(this System.Net.Sockets.Socket socket, byte[] buffer, RemoteEndPoint remoteEndPoint)
        {
            EndPoint remoteEP = remoteEndPoint;
            return socket.ReceiveFrom(buffer, ref remoteEP);
        }

        public static int SendToNonAlloc(this System.Net.Sockets.Socket socket, byte[] buffer, int offset, int size, SocketFlags socketFlags, RemoteEndPoint remoteEndPoint)
        {
            return socket.SendTo(buffer, offset, size, socketFlags, remoteEndPoint);
        }
    }
}
