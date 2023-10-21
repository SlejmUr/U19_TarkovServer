using UnityEngine;

namespace TarkovServerU19.BSGClasses
{
    internal class GClass2305
    {// Token: 0x0600BE08 RID: 48648 RVA: 0x00335EC0 File Offset: 0x003340C0
        public void Commit()
        {
            this.lose.Commit();
            this.received.Commit();
            this.sent.Commit();
            this.reliableReceived.Commit();
            this.reliableSent.Commit();
            this.unreliableReceived.Commit();
            this.unreliableSent.Commit();
        }

        // Token: 0x0400A9B6 RID: 43446
        public readonly Lose lose = new Lose();

        // Token: 0x0400A9B7 RID: 43447
        public readonly ReceivedQueue rtt = new ReceivedQueue();

        // Token: 0x0400A9B8 RID: 43448
        public readonly Disordered disordered = new Disordered();

        // Token: 0x0400A9B9 RID: 43449
        public readonly Disordered duplicated = new Disordered();

        // Token: 0x0400A9BA RID: 43450
        public readonly Received received = new Received();

        // Token: 0x0400A9BB RID: 43451
        public readonly Received sent = new Received();

        // Token: 0x0400A9BC RID: 43452
        public readonly Received reliableReceived = new Received();

        // Token: 0x0400A9BD RID: 43453
        public readonly Received reliableSent = new Received();

        // Token: 0x0400A9BE RID: 43454
        public readonly Received reliableSegmentalReceived = new Received();

        // Token: 0x0400A9BF RID: 43455
        public readonly Received reliableSegmentalSent = new Received();

        // Token: 0x0400A9C0 RID: 43456
        public readonly Received unreliableReceived = new Received();

        // Token: 0x0400A9C1 RID: 43457
        public readonly Received unreliableSent = new Received();

        // Token: 0x0400A9C2 RID: 43458
        public readonly ReceivedQueue receivedQueue = new ReceivedQueue();

        // Token: 0x0400A9C3 RID: 43459
        public readonly ReceivedQueue sentQueue = new ReceivedQueue();
    }
}