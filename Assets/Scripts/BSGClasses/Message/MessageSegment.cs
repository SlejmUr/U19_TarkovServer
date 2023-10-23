using System;

namespace TarkovServerU19.BSGClasses
{
    public class MessageSegment : ChannelTypeBuffer, IDisposable
    {
        public void Dispose()
        {
            MessageSegmentManager.Return(this);
        }
    }
}