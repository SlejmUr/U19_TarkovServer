using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TarkovServerU19.BSGEnums;
using UnityEngine;
using kcp2k;

namespace TarkovServerU19.BSGClasses
{
    public class Connection
    {
        // Token: 0x0600BD59 RID: 48473 RVA: 0x0033327E File Offset: 0x0033147E
        public static int CreateHash(EndPoint address)
        {
            return address.GetHashCode();
        }

        // Token: 0x0600BD5A RID: 48474 RVA: 0x00333286 File Offset: 0x00331486
        public void ChangeState(GClass2292 state)
        {
            GClass2292 gclass = this.gclass2292_0;
            if (gclass != null)
            {
                gclass.Exit();
            }
            this.gclass2292_0 = state;
            this.gclass2292_0.Enter();
        }

        // Token: 0x170019DA RID: 6618
        // (get) Token: 0x0600BD5B RID: 48475 RVA: 0x003332AB File Offset: 0x003314AB
        public uint CurrentTime
        {
            get
            {
                return (uint)this.stopwatch_0.ElapsedMilliseconds;
            }
        }

        // Token: 0x170019DB RID: 6619
        // (get) Token: 0x0600BD5C RID: 48476 RVA: 0x003332B9 File Offset: 0x003314B9
        public string Address
        {
            get
            {
                return this.gclass2288_0.ToString();
            }
        }

        // Token: 0x170019DC RID: 6620
        // (get) Token: 0x0600BD5D RID: 48477 RVA: 0x003332C6 File Offset: 0x003314C6
        public Configuration GClass2290_0
        {
            get
            {
                return this.gclass2290_0;
            }
        }

        // Token: 0x170019DD RID: 6621
        // (get) Token: 0x0600BD5E RID: 48478 RVA: 0x003332CE File Offset: 0x003314CE
        public int Index { get; }

        // Token: 0x170019DE RID: 6622
        // (get) Token: 0x0600BD5F RID: 48479 RVA: 0x003332D6 File Offset: 0x003314D6
        public int Hash { get; }

        // Token: 0x0600BD60 RID: 48480 RVA: 0x003332E0 File Offset: 0x003314E0
        public Connection(int index, Socket socket, RemoteEndPoint socketAddress, Configuration configuration)
        {
            this.gclass2292_0 = new GClass2293(this);
            this.Index = index;
            this.Hash = Connection.CreateHash(socketAddress);
            this.gclass2311_0 = socket;
            this.gclass2288_0 = socketAddress;
            this.gclass2290_0 = configuration;
            this.stopwatch_0.Start();
            this.method_0();
        }

        // Token: 0x0600BD61 RID: 48481 RVA: 0x003333A0 File Offset: 0x003315A0
        private void method_0()
        {
            this.kcp_0 = new Kcp(0U, new Action<byte[], int>(this.method_4));
            this.kcp_0.SetNoDelay(1U, 30U, 3, true);
            this.kcp_0.SetWindowSize(this.gclass2290_0.SendWindowSize, this.gclass2290_0.ReceiveWindowSize);
            this.kcp_0.SetMtu(1197U);
        }

        // Token: 0x0600BD62 RID: 48482 RVA: 0x00333406 File Offset: 0x00331606
        public void Connect()
        {
            ////this.Logger.LogInfo(string.Format("Connect (address: {0})", this.gclass2288_0), Array.Empty<object>());
            this.gclass2292_0.Connect();
        }

        // Token: 0x0600BD63 RID: 48483 RVA: 0x00333433 File Offset: 0x00331633
        public void Disconnect()
        {
            ////this.Logger.LogInfo(string.Format("Disconnect (address: {0})", this.gclass2288_0), Array.Empty<object>());
            this.gclass2292_0.Disconnect();
        }

        // Token: 0x0600BD64 RID: 48484 RVA: 0x00333460 File Offset: 0x00331660
        public void EarlyUpdate()
        {
            this.HandelReceiveReliableFinite();
            this.method_2();
            this.gclass2292_0.Update();
        }

        // Token: 0x0600BD65 RID: 48485 RVA: 0x00333479 File Offset: 0x00331679
        public void LateUpdate()
        {
            this.kcp_0.Update(this.CurrentTime);
        }

        // Token: 0x0600BD66 RID: 48486 RVA: 0x0033348C File Offset: 0x0033168C
        public void HandlePingReceiving(byte[] buffer, int count)
        {
            this.method_1(BitConverter.ToUInt32(buffer, 0));
        }

        // Token: 0x0600BD67 RID: 48487 RVA: 0x0033349C File Offset: 0x0033169C
        public void HandlePongReceiving(byte[] buffer, int count)
        {
            uint num = BitConverter.ToUInt32(buffer, 0);
            uint num2 = this.CurrentTime - num;
            this.gclass2305_0.rtt.Set(num2);
        }

        // Token: 0x0600BD68 RID: 48488 RVA: 0x003334CD File Offset: 0x003316CD
        public void ReturnConnect()
        {
            this.ReceiveQueue.Enqueue(GClass2304.Get(NetworkChannel.Unreliable, NetworkMessageType.Connect, Connection.Class1913<byte>.Empty));
        }

        // Token: 0x0600BD69 RID: 48489 RVA: 0x003334E6 File Offset: 0x003316E6
        public void ReturnDisconnect()
        {
            this.ReceiveQueue.Enqueue(GClass2304.Get(NetworkChannel.Unreliable, NetworkMessageType.Disconnect, Connection.Class1913<byte>.Empty));
        }

        // Token: 0x0600BD6A RID: 48490 RVA: 0x003334FF File Offset: 0x003316FF
        public void Clear()
        {
            this.SendQueue.Clear();
            this.kcp_0.Flush();
            this.kcp_0.SendClear();
        }

        // Token: 0x0600BD6B RID: 48491 RVA: 0x00333524 File Offset: 0x00331724
        public void SendConnect(bool syn, bool asc)
        {
            ////this.Logger.LogInfo(string.Format("Send connect (address: {0}, syn: {1}, asc: {2})", this.gclass2288_0, syn, asc), Array.Empty<object>());
            this.SendFinite(GClass2304.Get(NetworkChannel.Reliable, NetworkMessageType.Connect, new byte[]
            {
            Convert.ToByte(syn),
            Convert.ToByte(asc)
            }));
        }

        // Token: 0x0600BD6C RID: 48492 RVA: 0x00333582 File Offset: 0x00331782
        public void SendPing()
        {
            this.SendFinite(GClass2304.Get(NetworkChannel.Unreliable, NetworkMessageType.Ping, BitConverter.GetBytes(this.CurrentTime)));
        }

        // Token: 0x0600BD6D RID: 48493 RVA: 0x0033359C File Offset: 0x0033179C
        private void method_1(uint time)
        {
            this.SendFinite(GClass2304.Get(NetworkChannel.Unreliable, NetworkMessageType.Pong, BitConverter.GetBytes(time)));
        }

        // Token: 0x0600BD6E RID: 48494 RVA: 0x003335B1 File Offset: 0x003317B1
        public void SendDisconnect()
        {
            ////this.Logger.LogInfo(string.Format("Send disconnect (address: {0})", this.gclass2288_0), Array.Empty<object>());
            this.SendFinite(GClass2304.Get(NetworkChannel.Unreliable, NetworkMessageType.Disconnect, Connection.Class1913<byte>.Empty));
        }

        // Token: 0x0600BD6F RID: 48495 RVA: 0x003335E5 File Offset: 0x003317E5
        public void HandleDeadLink()
        {
            if (this.kcp_0.State == -1)
            {
                ////this.Logger.LogError(string.Format("Deadlink: a message was retransmitted times without ack. Disconnecting (address: {0})", this.gclass2288_0), Array.Empty<object>());
                this.Disconnect();
            }
        }

        // Token: 0x0600BD70 RID: 48496 RVA: 0x0033361B File Offset: 0x0033181B
        public void HandlePing()
        {
            if (this.CurrentTime - this.uint_0 > this.gclass2290_0.PingInterval)
            {
                this.SendPing();
                this.uint_0 = this.CurrentTime;
            }
        }

        // Token: 0x0600BD71 RID: 48497 RVA: 0x0033364C File Offset: 0x0033184C
        public void HandleOverflow()
        {

        }

        // Token: 0x0600BD72 RID: 48498 RVA: 0x00333708 File Offset: 0x00331908
        private void method_2()
        {
            if (this.CurrentTime - this.uint_1 > TimeSpan.FromSeconds(1.0).TotalMilliseconds)
            {
                this.gclass2305_0.receivedQueue.Set((float)this.kcp_0.WaitRcv);
                this.gclass2305_0.sentQueue.Set((float)this.kcp_0.WaitSnd);
                this.gclass2305_0.Commit();
                this.uint_1 = this.CurrentTime;
            }
        }

        // Token: 0x0600BD73 RID: 48499 RVA: 0x0033378C File Offset: 0x0033198C
        public void GetInformation(out string address, out int port, out byte error)
        {
            IPEndPoint ipendPoint = this.gclass2288_0;
            address = ipendPoint.Address.ToString();
            port = ipendPoint.Port;
            error = 0;
        }

        // Token: 0x0600BD74 RID: 48500 RVA: 0x003337B8 File Offset: 0x003319B8
        public int GetRtt()
        {
            return Mathf.RoundToInt(this.gclass2305_0.rtt.averageValue);
        }

        // Token: 0x0600BD75 RID: 48501 RVA: 0x003337CF File Offset: 0x003319CF
        public int GetLossPercent()
        {
            return Mathf.RoundToInt(this.gclass2305_0.lose.averageLosePercentValue * 100f);
        }

        // Token: 0x0600BD76 RID: 48502 RVA: 0x003337EC File Offset: 0x003319EC
        public int GetLossCount()
        {
            return Mathf.RoundToInt((float)this.gclass2305_0.lose.averageLoseCountValue);
        }

        // Token: 0x0600BD77 RID: 48503 RVA: 0x00333804 File Offset: 0x00331A04
        public void Send(MessageSegment message)
        {
            this.gclass2292_0.Send(message);
        }

        // Token: 0x0600BD78 RID: 48504 RVA: 0x00333814 File Offset: 0x00331A14
        public void SendFinite(MessageSegment message)
        {
            NetworkChannel channel = message.Channel;
            if (channel == NetworkChannel.Reliable)
            {
                this.method_3(message);
                return;
            }
            if (channel != NetworkChannel.Unreliable)
            {
                return;
            }
            this.SendUnreliable(message);
        }

        // Token: 0x0600BD79 RID: 48505 RVA: 0x00333840 File Offset: 0x00331A40
        public void SendUnreliable(MessageSegment message)
        {
            try
            {
                int num = 4;
                int num2 = this.byte_1.Length - 4;
                int count = message.Buffer.Count;
                if (count > num2)
                {
                    ////this.Logger.LogError(string.Format("Unreliable message size to send exceeded [{0}/{1}] (address: {2})", count, num2, this.gclass2288_0), Array.Empty<object>());
                    message.Dispose();
                    return;
                }
                this.ushort_0 += 1;
                this.byte_1[0] = (byte)message.Channel;
                this.byte_1[1] = (byte)this.ushort_0;
                this.byte_1[2] = (byte)(this.ushort_0 >> 8);
                this.byte_1[3] = (byte)message.Type;
                Array.Copy(message.Buffer.Array, message.Buffer.Offset, this.byte_1, num, message.Buffer.Count);
                int count2 = message.Buffer.Count + num;
                this.method_5(this.byte_1, count2);
                this.gclass2305_0.unreliableSent.Increment(message.Buffer.Count);
            }
            catch (Exception arg)
            {
                ////this.Logger.LogError(string.Format("Error sending an reliable message(address: {0}): {1}", this.gclass2288_0, arg), Array.Empty<object>());
            }
            message.Dispose();
        }

        // Token: 0x0600BD7A RID: 48506 RVA: 0x0033399C File Offset: 0x00331B9C
        private void method_3(MessageSegment message)
        {
            try
            {
                int num = 1;
                int num2 = this.byte_0.Length - 1;
                int count = message.Buffer.Count;
                if (count > num2)
                {
                    ////this.Logger.LogError(string.Format("Reliable message size to send exceeded [{0}/{1}] (address: {2})", count, num2, this.gclass2288_0), Array.Empty<object>());
                    message.Dispose();
                    return;
                }
                this.byte_0[0] = (byte)message.Type;
                Array.Copy(message.Buffer.Array, message.Buffer.Offset, this.byte_0, num, message.Buffer.Count);
                this.kcp_0.Send(this.byte_0, 0, message.Buffer.Count + num);
                this.gclass2305_0.reliableSent.Increment(message.Buffer.Count);
            }
            catch (Exception arg)
            {
                ////this.Logger.LogError(string.Format("Error initial sending an reliable message(address: {0}): {1}", this.gclass2288_0, arg), Array.Empty<object>());
            }
            message.Dispose();
        }

        // Token: 0x0600BD7B RID: 48507 RVA: 0x00333AB0 File Offset: 0x00331CB0
        private void method_4(byte[] buffer, int count)
        {
            try
            {
                int count2 = count + 3;
                this.ushort_0 += 1;
                this.byte_1[0] = 1;
                this.byte_1[1] = (byte)this.ushort_0;
                this.byte_1[2] = (byte)(this.ushort_0 >> 8);
                Array.Copy(buffer, 0, this.byte_1, 3, count);
                this.method_5(this.byte_1, count2);
                this.gclass2305_0.reliableSegmentalSent.Increment(count);
            }
            catch (Exception arg)
            {
                ////this.Logger.LogError(string.Format("Error finite sending an unreliable message(address: {0}): {1}", this.gclass2288_0, arg), Array.Empty<object>());
            }
        }

        // Token: 0x0600BD7C RID: 48508 RVA: 0x00333B5C File Offset: 0x00331D5C
        private void method_5(byte[] buffer, int count)
        {
            try
            {
                this.gclass2311_0.Send(buffer, 0, count, this.gclass2288_0);
                this.gclass2305_0.sent.Increment(count);
            }
            catch (Exception arg)
            {
                ////this.Logger.LogWarn(string.Format("Error sending data to socket (address: {0}): {1}", this.gclass2288_0, arg), Array.Empty<object>());
            }
        }

        // Token: 0x0600BD7D RID: 48509 RVA: 0x00333BC4 File Offset: 0x00331DC4
        public bool Receive(out MessageSegment message)
        {
            if (this.ReceiveQueue.Count > 0)
            {
                message = this.ReceiveQueue.Dequeue();
                return true;
            }
            message = null;
            return false;
        }

        // Token: 0x0600BD7E RID: 48510 RVA: 0x00333BE8 File Offset: 0x00331DE8
        public void HandelReceive(byte[] buffer, int count)
        {
            this.LastReceiveTime = this.CurrentTime;
            try
            {
                NetworkChannel networkChannel = (NetworkChannel)buffer[0];
                ushort num = (ushort)(0 | buffer[1]);
                num |= (ushort)(buffer[2] << 8);
                if (this.ushort_1 == num)
                {
                    this.gclass2305_0.duplicated.Increment();
                }
                else
                {
                    int num2;
                    if (this.ushort_1 < num)
                    {
                        num2 = (int)(num - this.ushort_1);
                        this.ushort_1 = num;
                    }
                    else
                    {
                        num2 = (int)(num - this.ushort_1 + ushort.MaxValue);
                        if (num2 < 32767)
                        {
                            this.ushort_1 = num;
                        }
                        else
                        {
                            this.gclass2305_0.disordered.Increment();
                            num2 = 1;
                        }
                    }
                    this.gclass2305_0.lose.Increment(num2, num2 - 1);
                    if (networkChannel != NetworkChannel.Reliable)
                    {
                        if (networkChannel == NetworkChannel.Unreliable)
                        {
                            this.method_6(buffer, count);
                        }
                    }
                    else
                    {
                        this.method_7(buffer, count);
                    }
                    this.gclass2305_0.received.Increment(count);
                }
            }
            catch (Exception arg)
            {
                ////this.Logger.LogError(string.Format("Error receiving message(address: {0}): {1}", this.gclass2288_0, arg), Array.Empty<object>());
            }
        }

        // Token: 0x0600BD7F RID: 48511 RVA: 0x00333CFC File Offset: 0x00331EFC
        private void method_6(byte[] buffer, int bufferSize)
        {
            try
            {
                int count = bufferSize - 4;
                NetworkChannel channel = (NetworkChannel)buffer[0];
                NetworkMessageType type = (NetworkMessageType)buffer[3];
                this.gclass2292_0.HandelReceive(GClass2304.Get(channel, type, buffer, 4, count));
                this.gclass2305_0.unreliableReceived.Increment(bufferSize);
            }
            catch (Exception arg)
            {
                ////this.Logger.LogError(string.Format("Error receiving an unreliable message(address: {0}): {1}", this.gclass2288_0, arg), Array.Empty<object>());
            }
        }

        // Token: 0x0600BD80 RID: 48512 RVA: 0x00333D74 File Offset: 0x00331F74
        private void method_7(byte[] buffer, int bufferSize)
        {
            try
            {
                int num = bufferSize - 3;
                int num2 = this.kcp_0.Input(buffer, 3, num);
                if (num2 < 0)
                {
                    //this.Logger.LogError(string.Format("Input failed with error={0} for buffer with length={1} (address: {2})", num2, num, this.gclass2288_0), Array.Empty<object>());
                }
                this.gclass2305_0.reliableSegmentalReceived.Increment(bufferSize);
            }
            catch (Exception arg)
            {
                ////this.Logger.LogError(string.Format("Error initial receiving an reliable message(address: {0}): {1}", this.gclass2288_0, arg), Array.Empty<object>());
            }
        }

        // Token: 0x0600BD81 RID: 48513 RVA: 0x00333E0C File Offset: 0x0033200C
        public void HandelReceiveReliableFinite()
        {
            try
            {
                int num;
                while ((num = this.kcp_0.PeekSize()) > 0)
                {
                    int num2 = this.kcp_0.Receive(this.byte_0, this.byte_0.Length);
                    if (num2 < 0)
                    {
                        //this.Logger.LogError(string.Format("Receive failed with error={0}. closing connection (address: {1})", num2, this.gclass2288_0), Array.Empty<object>());
                    }
                    NetworkMessageType type = (NetworkMessageType)this.byte_0[0];
                    this.gclass2292_0.HandelReceive(GClass2304.Get(NetworkChannel.Reliable, type, this.byte_0, 1, num - 1));
                    this.gclass2305_0.reliableReceived.Increment(num);
                }
            }
            catch (Exception arg)
            {
                //this.Logger.LogError(string.Format("Error finite receiving an reliable message(address: {0}): {1}", this.gclass2288_0, arg), Array.Empty<object>());
            }
        }

        // Token: 0x0600BD82 RID: 48514 RVA: 0x00333EE0 File Offset: 0x003320E0
        public UDPStats GetStatistics()
        {
            return new UDPStats
            {
                Rtt = this.gclass2305_0.rtt.averageValue,
                Lose = this.gclass2305_0.lose.averageLosePercentValue,
                Disordered = this.gclass2305_0.disordered.totalValue,
                Duplicated = this.gclass2305_0.duplicated.totalValue,
                ReliableReceivedAverage = this.gclass2305_0.reliableReceived.bytes.averageValue,
                ReliableSentAverage = this.gclass2305_0.reliableSent.bytes.averageValue,
                UnreliableReceivedAverage = this.gclass2305_0.unreliableReceived.bytes.averageValue,
                UnreliableSentAverage = this.gclass2305_0.unreliableSent.bytes.averageValue,
                ReceivedAverage = this.gclass2305_0.received.bytes.averageValue,
                SentAverage = this.gclass2305_0.sent.bytes.averageValue,
                ReceivedTotal = this.gclass2305_0.received.bytes.totalValue,
                SentTotal = this.gclass2305_0.sent.bytes.totalValue
            };
        }

        // Token: 0x0400A951 RID: 43345
        private GClass2292 gclass2292_0;

        // Token: 0x0400A953 RID: 43347
        private Socket gclass2311_0;

        // Token: 0x0400A954 RID: 43348
        private RemoteEndPoint gclass2288_0;

        // Token: 0x0400A955 RID: 43349
        private Kcp kcp_0;

        // Token: 0x0400A956 RID: 43350
        private Configuration gclass2290_0;

        // Token: 0x0400A957 RID: 43351
        private readonly Stopwatch stopwatch_0 = new Stopwatch();

        // Token: 0x0400A958 RID: 43352
        private readonly GClass2305 gclass2305_0 = new GClass2305();

        // Token: 0x0400A959 RID: 43353
        public uint LastReceiveTime;

        // Token: 0x0400A95A RID: 43354
        private uint uint_0;

        // Token: 0x0400A95B RID: 43355
        private uint uint_1;

        // Token: 0x0400A95C RID: 43356
        private const int int_0 = 1;

        // Token: 0x0400A95D RID: 43357
        private const int int_1 = 2;

        // Token: 0x0400A95E RID: 43358
        private const int int_2 = 1;

        // Token: 0x0400A95F RID: 43359
        private readonly byte[] byte_0 = new byte[149224];

        // Token: 0x0400A960 RID: 43360
        private readonly byte[] byte_1 = new byte[1200];

        // Token: 0x0400A961 RID: 43361
        private const int int_3 = 1;

        // Token: 0x0400A962 RID: 43362
        private const int int_4 = 30;

        // Token: 0x0400A963 RID: 43363
        private const int int_5 = 3;

        // Token: 0x0400A964 RID: 43364
        [CompilerGenerated]
        private readonly int int_6;

        // Token: 0x0400A965 RID: 43365
        [CompilerGenerated]
        private readonly int int_7;

        // Token: 0x0400A966 RID: 43366
        private ushort ushort_0;

        // Token: 0x0400A967 RID: 43367
        private ushort ushort_1 = ushort.MaxValue;

        // Token: 0x0400A968 RID: 43368
        public Queue<MessageSegment> SendQueue = new Queue<MessageSegment>();

        // Token: 0x0400A969 RID: 43369
        public readonly Queue<MessageSegment> ReceiveQueue = new Queue<MessageSegment>(16);

        // Token: 0x02001FD5 RID: 8149
        private static class Class1913<T>
        {
            // Token: 0x170019DF RID: 6623
            // (get) Token: 0x0600BD83 RID: 48515 RVA: 0x00334032 File Offset: 0x00332232
            public static ArraySegment<T> Empty
            {
                get
                {
                    return new ArraySegment<T>(Array.Empty<T>(), 0, 0);
                }
            }
        }
    }
}
