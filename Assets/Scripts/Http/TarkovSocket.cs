using System;
using WebSocketSharp;

namespace TarkovServerU19.Http
{
    public class TarkovSocket
    {
        private static WebSocket _ws;

        public static EventHandler<MessageEventArgs> OnMessage;

        public static void Start(string id)
        {
            _ws = new WebSocket("ws://172.0.0.1:6969/socket_server/"+ id);
            _ws.Connect();
            _ws.OnMessage += (sender, e) =>
            {
                OnMessage.Invoke(sender, e);
            };
        }

        public static void Stop()
        {
            if (_ws.IsAlive)
                _ws.Close();
        }

        public static void SendMessage(byte[] msg)
        {
            if (_ws.IsAlive)
                _ws.Send(msg);
        }
    }
}
