using System;
using TarkovServerU19.BSGEnums;

namespace TarkovServerU19.BSGClasses
{
    public class ChannelTypeBuffer
    {
        public NetworkChannel Channel;
        public NetworkMessageType Type;
        public ArraySegment<byte> Buffer;
    }
}