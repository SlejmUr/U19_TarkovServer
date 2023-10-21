using System;
using System.Buffers;
using TarkovServerU19.BSGEnums;

namespace TarkovServerU19.BSGClasses
{
    public static class GClass230
    {
        // Token: 0x0600BE00 RID: 48640 RVA: 0x00335DE3 File Offset: 0x00333FE3
        public static MessageSegment Create()
        {
            return new MessageSegment();
        }

        // Token: 0x0600BE01 RID: 48641 RVA: 0x00335DEA File Offset: 0x00333FEA
        public static MessageSegment Get(NetworkChannel channel, NetworkMessageType type, ArraySegment<byte> buffer)
        {
            return GClass2304.Get(channel, type, buffer.Array, buffer.Offset, buffer.Count);
        }

        // Token: 0x0600BE02 RID: 48642 RVA: 0x00335E08 File Offset: 0x00334008
        public static MessageSegment Get(NetworkChannel channel, NetworkMessageType type, byte[] buffer)
        {
            return GClass2304.Get(channel, type, buffer, 0, buffer.Length);
        }

        // Token: 0x0600BE03 RID: 48643 RVA: 0x00335E18 File Offset: 0x00334018
        public static MessageSegment Get(NetworkChannel channel, NetworkMessageType type, byte[] buffer, int offset, int count)
        {
            MessageSegment messageSegment = GClass2304.gclass1006_0.Get();
            byte[] array = GClass2304.arrayPool_0.Rent(count);
            Array.Copy(buffer, offset, array, 0, count);
            messageSegment.Channel = channel;
            messageSegment.Type = type;
            messageSegment.Buffer = new ArraySegment<byte>(array, 0, count);
            return messageSegment;
        }

        // Token: 0x0600BE04 RID: 48644 RVA: 0x00335E64 File Offset: 0x00334064
        public static void Return(MessageSegment messageSegment)
        {
            GClass2304.arrayPool_0.Return(messageSegment.Buffer.Array, false);
            GClass2304.gclass1006_0.Return(messageSegment);
        }
    }
}