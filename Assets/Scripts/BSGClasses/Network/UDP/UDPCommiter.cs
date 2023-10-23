namespace TarkovServerU19.BSGClasses
{
    public class UDPCommiter
    {
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

        public readonly Lose lose = new Lose();
        public readonly ReceivedQueue rtt = new ReceivedQueue();
        public readonly Disordered disordered = new Disordered();
        public readonly Disordered duplicated = new Disordered();
        public readonly Received received = new Received();
        public readonly Received sent = new Received();
        public readonly Received reliableReceived = new Received();
        public readonly Received reliableSent = new Received();
        public readonly Received reliableSegmentalReceived = new Received();
        public readonly Received reliableSegmentalSent = new Received();
        public readonly Received unreliableReceived = new Received();
        public readonly Received unreliableSent = new Received();
        public readonly ReceivedQueue receivedQueue = new ReceivedQueue();
        public readonly ReceivedQueue sentQueue = new ReceivedQueue();
    }
}