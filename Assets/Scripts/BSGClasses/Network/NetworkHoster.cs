using System;
using System.Diagnostics;
using System.Threading;
using TarkovServerU19.BSGEnums;
using UnityEngine.Networking;
using System.Buffers;

namespace TarkovServerU19.BSGClasses
{
    public class NetworkHoster
    {

        public int Index
        {
            get
            {
                return this.networkConnectionManager.Index;
            }
        }

        public NetworkHoster(HostTopology topology, int index, int port, string ip)
        {
            this.networkConnectionManager = new NetworkConnectionManager(topology, index, port, ip);
            this.thread_0 = new Thread(new ThreadStart(this.MainThread));
            this.thread_0.Name = "NetworkHostThread";
            this.thread_0.Priority = System.Threading.ThreadPriority.Highest;
            this.thread_0.Start();
        }

        private void MainThread()
        {
            Stopwatch stopwatch = new Stopwatch();
            for (; ; )
            {
                stopwatch.Reset();
                stopwatch.Start();
                try
                {
                    this.networkConnectionManager.EarlyUpdate(0.5f);
                    ConnectIPPort_Struct @struct;
                    byte b;
                    while (this.ConIPPort.TryDequeue(out @struct))
                    {
                        this.networkConnectionManager.Connect(@struct.Connection, @struct.Address, @struct.Port, out b);
                    }
                    int connection;
                    MessageSegment messageSegment;
                    while (this.networkConnectionManager.Receive(out connection, out messageSegment, out b))
                    {
                        byte[] array = ArrayPool<byte>.Shared.Rent(messageSegment.Buffer.Count);
                        Array.Copy(messageSegment.Buffer.Array, array, messageSegment.Buffer.Count);
                        this.ConTCB.Enqueue(new ConnectionTypeChannelBuffer
                        {
                            Connection = connection,
                            Type = messageSegment.Type,
                            Channel = messageSegment.Channel,
                            Buffer = new ArraySegment<byte>(array, 0, messageSegment.Buffer.Count)
                        });
                        messageSegment.Dispose();
                    }
                    ConnectionChannelBuffer struct2;
                    while (this.ConCB.TryDequeue(out struct2))
                    {
                        this.networkConnectionManager.Send(struct2.Connection, MessageSegmentManager.Get(struct2.Channel, NetworkMessageType.Data, struct2.Buffer), out b);
                        ArrayPool<byte>.Shared.Return(struct2.Buffer.Array, false);
                    }
                    ConnectionID_strcut struct3;
                    while (this.ConId.TryDequeue(out struct3))
                    {
                        this.networkConnectionManager.Disconnect(struct3.Connection, out b);
                    }
                    this.networkConnectionManager.LateUpdate();
                    goto IL_1EE;
                }
                catch (Exception arg)
                {
                    UnityEngine.Debug.Log(string.Format("Error in thread process: {0}", arg));
                    goto IL_1EE;
                }
                goto IL_1A5;
            IL_1D4:
                Thread.Sleep(Math.Max(10 - (int)stopwatch.ElapsedMilliseconds, 1));
                continue;
            IL_1A5:
                UnityEngine.Debug.Log(string.Format("Thread processing exceeded the limit [{0}/{1}]", stopwatch.ElapsedMilliseconds, 2000));
                goto IL_1D4;
            IL_1EE:
                stopwatch.Stop();
                if (stopwatch.ElapsedMilliseconds > 2000L)
                {
                    goto IL_1A5;
                }
                goto IL_1D4;
            }
        }

        public int Connect(string address, int port, out byte error)
        {
            int nextConnectionIndex = this.networkConnectionManager.NextConnectionIndex;
            this.ConIPPort.Enqueue(new ConnectIPPort_Struct
            {
                Connection = nextConnectionIndex,
                Address = address,
                Port = port
            });
            error = 0;
            return nextConnectionIndex;
        }

        public bool Disconnect(int index, out byte error)
        {
            this.ConId.Enqueue(new NetworkHoster.ConnectionID_strcut
            {
                Connection = index
            });
            error = 0;
            return true;
        }

        public void GetConnectionInfo(int index, out string address, out int port, out byte error)
        {
            this.networkConnectionManager.GetConnectionInfo(index, out address, out port, out error);
        }

        public int GetRtt(int index, out byte error)
        {
            return this.networkConnectionManager.GetRtt(index, out error);
        }

        public int GetLossPercent(int index, out byte error)
        {
            return this.networkConnectionManager.GetLossPercent(index, out error);
        }

        public int GetLossCount(int index, out byte error)
        {
            return this.networkConnectionManager.GetLossPercent(index, out error);
        }

        public bool Send(int connection, NetworkChannel channel, byte[] buffer, int bufferSize, out byte error)
        {
            byte[] array = ArrayPool<byte>.Shared.Rent(bufferSize);
            Array.Copy(buffer, array, bufferSize);
            this.ConCB.Enqueue(new ConnectionChannelBuffer
            {
                Connection = connection,
                Channel = channel,
                Buffer = new ArraySegment<byte>(array, 0, bufferSize)
            });
            error = 0;
            return true;
        }

        public bool Receive(out int index, out NetworkMessageType type, out NetworkChannel channel, byte[] buffer, out int bufferSize, out byte error)
        {
            ConnectionTypeChannelBuffer @struct;
            if (this.ConTCB.TryDequeue(out @struct))
            {
                index = @struct.Connection;
                type = @struct.Type;
                channel = @struct.Channel;
                Array.Copy(@struct.Buffer.Array, @struct.Buffer.Offset, buffer, 0, @struct.Buffer.Count);
                bufferSize = @struct.Buffer.Count;
                ArrayPool<byte>.Shared.Return(@struct.Buffer.Array, false);
                error = 0;
                return true;
            }
            index = 0;
            type = NetworkMessageType.None;
            channel = NetworkChannel.None;
            bufferSize = 0;
            error = 0;
            return false;
        }

        public void Shutdown()
        {
            this.thread_0.Abort();
            this.networkConnectionManager.Shutdown();
        }

        public UDPStats GetStatistics(int index)
        {
            return this.networkConnectionManager.GetStatistics(index);
        }

        private const int int_0 = 10;
        private const int int_1 = 2000;
        private NetworkConnectionManager networkConnectionManager;
        private Thread thread_0;
        private readonly HeadTailEnumerable<ConnectIPPort_Struct> ConIPPort = new HeadTailEnumerable<ConnectIPPort_Struct>();
        private readonly HeadTailEnumerable<ConnectionID_strcut> ConId = new HeadTailEnumerable<ConnectionID_strcut>();
        private readonly HeadTailEnumerable<ConnectionTypeChannelBuffer> ConTCB = new HeadTailEnumerable<ConnectionTypeChannelBuffer>();
        private readonly HeadTailEnumerable<ConnectionChannelBuffer> ConCB = new HeadTailEnumerable<ConnectionChannelBuffer>();

        public struct ConnectIPPort_Struct
        {
            public int Connection;
            public string Address;
            public int Port;
        }

        public struct ConnectionID_strcut
        {
            public int Connection;
        }

        public struct ConnectionTypeChannelBuffer
        {
            public int Connection;
            public NetworkMessageType Type;
            public NetworkChannel Channel;
            public ArraySegment<byte> Buffer;
        }

        public struct ConnectionChannelBuffer
        {
            public int Connection;
            public NetworkChannel Channel;
            public ArraySegment<byte> Buffer;
        }
    }
}
