using System.Buffers;
using System;
using TarkovServerU19.BSGEnums;
using TarkovServerU19.Helpers;
namespace TarkovServerU19.BSGClasses
{
    internal class GClass2304
    {
        private static readonly GClass1006<MessageSegment> gclass1006_0 = new GClass1006<MessageSegment>(Create, 1024);

        private static readonly ArrayPool<byte> arrayPool_0 = ArrayPool<byte>.Shared;

        public static MessageSegment Create()
        {
            return new MessageSegment();
        }

        public static MessageSegment Get(NetworkChannel channel, NetworkMessageType type, ArraySegment<byte> buffer)
        {
            return Get(channel, type, buffer.Array, buffer.Offset, buffer.Count);
        }

        public static MessageSegment Get(NetworkChannel channel, NetworkMessageType type, byte[] buffer)
        {
            return Get(channel, type, buffer, 0, buffer.Length);
        }

        public static MessageSegment Get(NetworkChannel channel, NetworkMessageType type, byte[] buffer, int offset, int count)
        {
            MessageSegment messageSegment = gclass1006_0.Get();
            byte[] array = arrayPool_0.Rent(count);
            Array.Copy(buffer, offset, array, 0, count);
            messageSegment.Channel = channel;
            messageSegment.Type = type;
            messageSegment.Buffer = new ArraySegment<byte>(array, 0, count);
            return messageSegment;
        }

        public static void Return(MessageSegment messageSegment)
        {
            arrayPool_0.Return(messageSegment.Buffer.Array);
            gclass1006_0.Return(messageSegment);
        }
    }
}