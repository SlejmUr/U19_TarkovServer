using System;

namespace TarkovServerU19.BSGClasses
{
    public class MessageSegment : GClass2302, IDisposable
    {
        // Token: 0x0600BE06 RID: 48646 RVA: 0x00335EAE File Offset: 0x003340AE
        public void Dispose()
        {
            GClass2304.Return(this);
        }
    }
}