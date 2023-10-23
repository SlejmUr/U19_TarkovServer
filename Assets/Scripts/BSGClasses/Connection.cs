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
        public static int CreateHash(EndPoint address)
        {
            return address.GetHashCode();
        }

        public void ChangeState(AbstactConnection state)
        {
            AbstactConnection gclass = this.abstractConnection;
            if (gclass != null)
            {
                gclass.Exit();
            }
            this.abstractConnection = state;
            this.abstractConnection.Enter();
        }

        public uint CurrentTime
        {
            get
            {
                return (uint)this.stopwatch.ElapsedMilliseconds;
            }
        }

        public string Address
        {
            get
            {
                return this.remoteEndpoint.ToString();
            }
        }

        public Configuration configuration
        {
            get
            {
                return this.internal_Configuration;
            }
        }
        public int Index { get; }

        public int Hash { get; }

        public Connection(int index, Socket socket, RemoteEndPoint socketAddress, Configuration configuration)
        {
            this.abstractConnection = new InitialConenction(this);
            this.Index = index;
            this.Hash = Connection.CreateHash(socketAddress);
            this.socket = socket;
            this.remoteEndpoint = socketAddress;
            this.internal_Configuration = configuration;
            this.stopwatch.Start();
            this.method_0();
        }

        private void method_0()
        {
            this.kcp = new Kcp(0U, new Action<byte[], int>(this.SendUnreliable));
            this.kcp.SetNoDelay(1U, 30U, 3, true);
            this.kcp.SetWindowSize(this.internal_Configuration.SendWindowSize, this.internal_Configuration.ReceiveWindowSize);
            this.kcp.SetMtu(1197U);
        }

        public void Connect()
        {
            UnityEngine.Debug.Log(string.Format("Connect (address: {0})", this.remoteEndpoint));
            this.abstractConnection.Connect();
        }


        public void Disconnect()
        {
            UnityEngine.Debug.Log(string.Format("Disconnect (address: {0})", this.remoteEndpoint));
            this.abstractConnection.Disconnect();
        }

        public void EarlyUpdate()
        {
            this.HandelReceiveReliableFinite();
            this.method_2();
            this.abstractConnection.Update();
        }

        public void LateUpdate()
        {
            this.kcp.Update(this.CurrentTime);
        }

        public void HandlePingReceiving(byte[] buffer, int count)
        {
            this.method_1(BitConverter.ToUInt32(buffer, 0));
        }

        public void HandlePongReceiving(byte[] buffer, int count)
        {
            uint num = BitConverter.ToUInt32(buffer, 0);
            uint num2 = this.CurrentTime - num;
            this.updCommiter.rtt.Set(num2);
        }


        public void ReturnConnect()
        {
            this.ReceiveQueue.Enqueue(MessageSegmentManager.Get(NetworkChannel.Unreliable, NetworkMessageType.Connect, ArraySegments<byte>.Empty));
        }

        public void ReturnDisconnect()
        {
            this.ReceiveQueue.Enqueue(MessageSegmentManager.Get(NetworkChannel.Unreliable, NetworkMessageType.Disconnect, ArraySegments<byte>.Empty));
        }

        public void Clear()
        {
            this.SendQueue.Clear();
            this.kcp.Flush();
            this.kcp.SendClear();
        }

        public void SendConnect(bool syn, bool asc)
        {
            UnityEngine.Debug.Log(string.Format("Send connect (address: {0}, syn: {1}, asc: {2})", this.Address, syn, asc));
            this.SendFinite(MessageSegmentManager.Get(NetworkChannel.Reliable, NetworkMessageType.Connect, new byte[]
            {
                Convert.ToByte(syn),
                Convert.ToByte(asc)
            }));
        }

        public void SendPing()
        {
            this.SendFinite(MessageSegmentManager.Get(NetworkChannel.Unreliable, NetworkMessageType.Ping, BitConverter.GetBytes(this.CurrentTime)));
        }
        private void method_1(uint time)
        {
            this.SendFinite(MessageSegmentManager.Get(NetworkChannel.Unreliable, NetworkMessageType.Pong, BitConverter.GetBytes(time)));
        }

        public void SendDisconnect()
        {
            UnityEngine.Debug.Log(string.Format("Send disconnect (address: {0})", this.Address));
            this.SendFinite(MessageSegmentManager.Get(NetworkChannel.Unreliable, NetworkMessageType.Disconnect, Connection.ArraySegments<byte>.Empty));
        }

        public void HandleDeadLink()
        {
            if (this.kcp.State == -1)
            {
                UnityEngine.Debug.Log(string.Format("Deadlink: a message was retransmitted times without ack. Disconnecting (address: {0})", this.remoteEndpoint));
                this.Disconnect();
            }
        }

        public void HandlePing()
        {
            if (this.CurrentTime - this.time > this.internal_Configuration.PingInterval)
            {
                this.SendPing();
                this.time = this.CurrentTime;
            }
        }

        public void HandleOverflow()
        {

        }

        private void method_2()
        {
            if (this.CurrentTime - this.uint_1 > TimeSpan.FromSeconds(1.0).TotalMilliseconds)
            {
                this.updCommiter.receivedQueue.Set((float)this.kcp.WaitRcv);
                this.updCommiter.sentQueue.Set((float)this.kcp.WaitSnd);
                this.updCommiter.Commit();
                this.uint_1 = this.CurrentTime;
            }
        }

        public void GetInformation(out string address, out int port, out byte error)
        {
            IPEndPoint ipendPoint = this.remoteEndpoint;
            address = ipendPoint.Address.ToString();
            port = ipendPoint.Port;
            error = 0;
        }

        public int GetRtt()
        {
            return Mathf.RoundToInt(this.updCommiter.rtt.averageValue);
        }

        public int GetLossPercent()
        {
            return Mathf.RoundToInt(this.updCommiter.lose.averageLosePercentValue * 100f);
        }

        public int GetLossCount()
        {
            return Mathf.RoundToInt((float)this.updCommiter.lose.averageLoseCountValue);
        }

        public void Send(MessageSegment message)
        {
            this.abstractConnection.Send(message);
        }

        public void SendFinite(MessageSegment message)
        {
            NetworkChannel channel = message.Channel;
            if (channel == NetworkChannel.Reliable)
            {
                this.SendReliable(message);
                return;
            }
            if (channel != NetworkChannel.Unreliable)
            {
                return;
            }
            this.SendUnreliable(message);
        }

        public void SendUnreliable(MessageSegment message)
        {
            try
            {
                int num = 4;
                int num2 = this.byte_1.Length - 4;
                int count = message.Buffer.Count;
                if (count > num2)
                {
                    UnityEngine.Debug.Log(string.Format("Unreliable message size to send exceeded [{0}/{1}] (address: {2})", count, num2, this.remoteEndpoint));
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
                this.SendToSocket(this.byte_1, count2);
                this.updCommiter.unreliableSent.Increment(message.Buffer.Count);
            }
            catch (Exception arg)
            {
                UnityEngine.Debug.Log(string.Format("Error sending an reliable message(address: {0}): {1}", this.remoteEndpoint, arg));
            }
            message.Dispose();
        }

        private void SendReliable(MessageSegment message)
        {
            try
            {
                int num = 1;
                int num2 = this.byte_0.Length - 1;
                int count = message.Buffer.Count;
                if (count > num2)
                {
                    UnityEngine.Debug.Log(string.Format("Reliable message size to send exceeded [{0}/{1}] (address: {2})", count, num2, this.Address));
                    message.Dispose();
                    return;
                }
                this.byte_0[0] = (byte)message.Type;
                Array.Copy(message.Buffer.Array, message.Buffer.Offset, this.byte_0, num, message.Buffer.Count);
                this.kcp.Send(this.byte_0, 0, message.Buffer.Count + num);
                this.updCommiter.reliableSent.Increment(message.Buffer.Count);
            }
            catch (Exception arg)
            {
                UnityEngine.Debug.Log(string.Format("Error initial sending an reliable message(address: {0}): {1}", this.Address, arg));
            }
            message.Dispose();
        }

        private void SendUnreliable(byte[] buffer, int count)
        {
            try
            {
                int count2 = count + 3;
                this.ushort_0 += 1;
                this.byte_1[0] = 1;
                this.byte_1[1] = (byte)this.ushort_0;
                this.byte_1[2] = (byte)(this.ushort_0 >> 8);
                Array.Copy(buffer, 0, this.byte_1, 3, count);
                this.SendToSocket(this.byte_1, count2);
                this.updCommiter.reliableSegmentalSent.Increment(count);
            }
            catch (Exception arg)
            {
                UnityEngine.Debug.Log(string.Format("Error finite sending an unreliable message(address: {0}): {1}", this.Address, arg));
            }
        }

        private void SendToSocket(byte[] buffer, int count)
        {
            try
            {
                this.socket.Send(buffer, 0, count, this.remoteEndpoint);
                this.updCommiter.sent.Increment(count);
            }
            catch (Exception arg)
            {
                UnityEngine.Debug.Log(string.Format("Error sending data to socket (address: {0}): {1}", this.Address, arg));
            }
        }

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
                    this.updCommiter.duplicated.Increment();
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
                            this.updCommiter.disordered.Increment();
                            num2 = 1;
                        }
                    }
                    this.updCommiter.lose.Increment(num2, num2 - 1);
                    if (networkChannel != NetworkChannel.Reliable)
                    {
                        if (networkChannel == NetworkChannel.Unreliable)
                        {
                            this.UnreliableReceive(buffer, count);
                        }
                    }
                    else
                    {
                        this.ReliableReceive(buffer, count);
                    }
                    this.updCommiter.received.Increment(count);
                }
            }
            catch (Exception arg)
            {
                UnityEngine.Debug.Log(string.Format("Error receiving message(address: {0}): {1}", this.Address, arg));
            }
        }

        private void UnreliableReceive(byte[] buffer, int bufferSize)
        {
            try
            {
                int count = bufferSize - 4;
                NetworkChannel channel = (NetworkChannel)buffer[0];
                NetworkMessageType type = (NetworkMessageType)buffer[3];
                this.abstractConnection.HandelReceive(MessageSegmentManager.Get(channel, type, buffer, 4, count));
                this.updCommiter.unreliableReceived.Increment(bufferSize);
            }
            catch (Exception arg)
            {
                UnityEngine.Debug.Log(string.Format("Error receiving an unreliable message(address: {0}): {1}", this.Address, arg));
            }
        }

        private void ReliableReceive(byte[] buffer, int bufferSize)
        {
            try
            {
                int num = bufferSize - 3;
                int num2 = this.kcp.Input(buffer, 3, num);
                if (num2 < 0)
                {
                    UnityEngine.Debug.Log(string.Format("Input failed with error={0} for buffer with length={1} (address: {2})", num2, num, this.Address));
                }
                this.updCommiter.reliableSegmentalReceived.Increment(bufferSize);
            }
            catch (Exception arg)
            {
                UnityEngine.Debug.Log(string.Format("Error initial receiving an reliable message(address: {0}): {1}", this.Address, arg));
            }
        }

        public void HandelReceiveReliableFinite()
        {
            try
            {
                int num;
                while ((num = this.kcp.PeekSize()) > 0)
                {
                    int num2 = this.kcp.Receive(this.byte_0, this.byte_0.Length);
                    if (num2 < 0)
                    {
                        UnityEngine.Debug.Log(string.Format("Receive failed with error={0}. closing connection (address: {1})", num2, this.Address));
                    }
                    NetworkMessageType type = (NetworkMessageType)this.byte_0[0];
                    this.abstractConnection.HandelReceive(MessageSegmentManager.Get(NetworkChannel.Reliable, type, this.byte_0, 1, num - 1));
                    this.updCommiter.reliableReceived.Increment(num);
                }
            }
            catch (Exception arg)
            {
                UnityEngine.Debug.Log(string.Format("Error finite receiving an reliable message(address: {0}): {1}", this.Address, arg));
            }
        }

        public UDPStats GetStatistics()
        {
            return new UDPStats
            {
                Rtt = this.updCommiter.rtt.averageValue,
                Lose = this.updCommiter.lose.averageLosePercentValue,
                Disordered = this.updCommiter.disordered.totalValue,
                Duplicated = this.updCommiter.duplicated.totalValue,
                ReliableReceivedAverage = this.updCommiter.reliableReceived.bytes.averageValue,
                ReliableSentAverage = this.updCommiter.reliableSent.bytes.averageValue,
                UnreliableReceivedAverage = this.updCommiter.unreliableReceived.bytes.averageValue,
                UnreliableSentAverage = this.updCommiter.unreliableSent.bytes.averageValue,
                ReceivedAverage = this.updCommiter.received.bytes.averageValue,
                SentAverage = this.updCommiter.sent.bytes.averageValue,
                ReceivedTotal = this.updCommiter.received.bytes.totalValue,
                SentTotal = this.updCommiter.sent.bytes.totalValue
            };
        }

        private AbstactConnection abstractConnection;
        private Socket socket;
        private RemoteEndPoint remoteEndpoint;
        private Kcp kcp;
        private Configuration internal_Configuration;
        private readonly Stopwatch stopwatch = new Stopwatch();
        private readonly UDPCommiter updCommiter = new UDPCommiter();
        public uint LastReceiveTime;
        private uint time;
        private uint uint_1;
        private readonly byte[] byte_0 = new byte[149224];
        private readonly byte[] byte_1 = new byte[1200];
        private ushort ushort_0;
        private ushort ushort_1 = ushort.MaxValue;
        public Queue<MessageSegment> SendQueue = new Queue<MessageSegment>();
        public readonly Queue<MessageSegment> ReceiveQueue = new Queue<MessageSegment>(16);

        private static class ArraySegments<T>
        {
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
