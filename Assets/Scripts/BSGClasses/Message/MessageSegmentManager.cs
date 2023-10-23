using System.Buffers;
using System;
using TarkovServerU19.BSGEnums;
namespace TarkovServerU19.BSGClasses
{
    public class MessageSegmentManager
    {
        public static readonly ObjectGenerator<MessageSegment> MessageSegments = new ObjectGenerator<MessageSegment>(Create, 1024);

        public static readonly ArrayPool<byte> arrayPool = ArrayPool<byte>.Shared;

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
            MessageSegment messageSegment = MessageSegments.Get();
            byte[] array = arrayPool.Rent(count);
            Array.Copy(buffer, offset, array, 0, count);
            messageSegment.Channel = channel;
            messageSegment.Type = type;
            messageSegment.Buffer = new ArraySegment<byte>(array, 0, count);
            return messageSegment;
        }

        public static void Return(MessageSegment messageSegment)
        {
            arrayPool.Return(messageSegment.Buffer.Array);
            MessageSegments.Return(messageSegment);
        }
    }
}