using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TarkovServerU19.BSGClasses;
using TarkovServerU19.BSGEnums;
using UnityEngine.Networking;
using UnityEngine;
using System.Buffers;

namespace TarkovServerU19.BSGClasses
{
    internal class GClass2298
    {// Token: 0x170019E0 RID: 6624
     // (get) Token: 0x0600BDA9 RID: 48553 RVA: 0x0033460D File Offset: 0x0033280D
        public int Index
        {
            get
            {
                return this.gclass2299_0.Index;
            }
        }

        // Token: 0x0600BDAA RID: 48554 RVA: 0x0033461C File Offset: 0x0033281C
        public GClass2298(HostTopology topology, int index, int port, string ip)
        {
            this.gclass2299_0 = new GClass2299(topology, index, port, ip);
            this.thread_0 = new Thread(new ThreadStart(this.method_0));
            this.thread_0.Name = "NetworkHostThread";
            this.thread_0.Priority = System.Threading.ThreadPriority.Highest;
            this.thread_0.Start();
        }

        // Token: 0x0600BDAB RID: 48555 RVA: 0x003346B4 File Offset: 0x003328B4
        private void method_0()
        {
            Stopwatch stopwatch = new Stopwatch();
            for (; ; )
            {
                stopwatch.Reset();
                stopwatch.Start();
                try
                {
                    this.gclass2299_0.EarlyUpdate(0.5f);
                    GClass2298.Struct705 @struct;
                    byte b;
                    while (this.gclass2300_0.TryDequeue(out @struct))
                    {
                        this.gclass2299_0.Connect(@struct.Connection, @struct.Address, @struct.Port, out b);
                    }
                    int connection;
                    MessageSegment messageSegment;
                    while (this.gclass2299_0.Receive(out connection, out messageSegment, out b))
                    {
                        byte[] array = ArrayPool<byte>.Shared.Rent(messageSegment.Buffer.Count);
                        Array.Copy(messageSegment.Buffer.Array, array, messageSegment.Buffer.Count);
                        this.gclass2300_2.Enqueue(new GClass2298.Struct707
                        {
                            Connection = connection,
                            Type = messageSegment.Type,
                            Channel = messageSegment.Channel,
                            Buffer = new ArraySegment<byte>(array, 0, messageSegment.Buffer.Count)
                        });
                        messageSegment.Dispose();
                    }
                    GClass2298.Struct708 struct2;
                    while (this.gclass2300_3.TryDequeue(out struct2))
                    {
                        this.gclass2299_0.Send(struct2.Connection, GClass2304.Get(struct2.Channel, NetworkMessageType.Data, struct2.Buffer), out b);
                        ArrayPool<byte>.Shared.Return(struct2.Buffer.Array, false);
                    }
                    GClass2298.Struct706 struct3;
                    while (this.gclass2300_1.TryDequeue(out struct3))
                    {
                        this.gclass2299_0.Disconnect(struct3.Connection, out b);
                    }
                    this.gclass2299_0.LateUpdate();
                    goto IL_1EE;
                }
                catch (Exception arg)
                {
                    //this.Logger.LogError(string.Format("Error in thread process: {0}", arg), Array.Empty<object>());
                    goto IL_1EE;
                }
                goto IL_1A5;
            IL_1D4:
                Thread.Sleep(Math.Max(10 - (int)stopwatch.ElapsedMilliseconds, 1));
                continue;
            IL_1A5:
                //this.Logger.LogError(string.Format("Thread processing exceeded the limit [{0}/{1}]", stopwatch.ElapsedMilliseconds, 2000), Array.Empty<object>());
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

        // Token: 0x0600BDAC RID: 48556 RVA: 0x003348E4 File Offset: 0x00332AE4
        public int Connect(string address, int port, out byte error)
        {
            int nextConnectionIndex = this.gclass2299_0.NextConnectionIndex;
            this.gclass2300_0.Enqueue(new GClass2298.Struct705
            {
                Connection = nextConnectionIndex,
                Address = address,
                Port = port
            });
            error = 0;
            return nextConnectionIndex;
        }

        // Token: 0x0600BDAD RID: 48557 RVA: 0x00334930 File Offset: 0x00332B30
        public bool Disconnect(int index, out byte error)
        {
            this.gclass2300_1.Enqueue(new GClass2298.Struct706
            {
                Connection = index
            });
            error = 0;
            return true;
        }

        // Token: 0x0600BDAE RID: 48558 RVA: 0x0033495D File Offset: 0x00332B5D
        public void GetConnectionInfo(int index, out string address, out int port, out byte error)
        {
            this.gclass2299_0.GetConnectionInfo(index, out address, out port, out error);
        }

        // Token: 0x0600BDAF RID: 48559 RVA: 0x0033496F File Offset: 0x00332B6F
        public int GetRtt(int index, out byte error)
        {
            return this.gclass2299_0.GetRtt(index, out error);
        }

        // Token: 0x0600BDB0 RID: 48560 RVA: 0x0033497E File Offset: 0x00332B7E
        public int GetLossPercent(int index, out byte error)
        {
            return this.gclass2299_0.GetLossPercent(index, out error);
        }

        // Token: 0x0600BDB1 RID: 48561 RVA: 0x0033497E File Offset: 0x00332B7E
        public int GetLossCount(int index, out byte error)
        {
            return this.gclass2299_0.GetLossPercent(index, out error);
        }

        // Token: 0x0600BDB2 RID: 48562 RVA: 0x00334990 File Offset: 0x00332B90
        public bool Send(int connection, NetworkChannel channel, byte[] buffer, int bufferSize, out byte error)
        {
            byte[] array = ArrayPool<byte>.Shared.Rent(bufferSize);
            Array.Copy(buffer, array, bufferSize);
            this.gclass2300_3.Enqueue(new GClass2298.Struct708
            {
                Connection = connection,
                Channel = channel,
                Buffer = new ArraySegment<byte>(array, 0, bufferSize)
            });
            error = 0;
            return true;
        }

        // Token: 0x0600BDB3 RID: 48563 RVA: 0x003349EC File Offset: 0x00332BEC
        public bool Receive(out int index, out NetworkMessageType type, out NetworkChannel channel, byte[] buffer, out int bufferSize, out byte error)
        {
            GClass2298.Struct707 @struct;
            if (this.gclass2300_2.TryDequeue(out @struct))
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

        // Token: 0x0600BDB4 RID: 48564 RVA: 0x00334A8A File Offset: 0x00332C8A
        public void Shutdown()
        {
            this.thread_0.Abort();
            this.gclass2299_0.Shutdown();
        }

        // Token: 0x0600BDB5 RID: 48565 RVA: 0x00334AA2 File Offset: 0x00332CA2
        public GStruct335 GetStatistics(int index)
        {
            return this.gclass2299_0.GetStatistics(index);
        }

        // Token: 0x0400A96C RID: 43372
        private const int int_0 = 10;

        // Token: 0x0400A96D RID: 43373
        private const int int_1 = 2000;

        // Token: 0x0400A96E RID: 43374
        private GClass2299 gclass2299_0;

        // Token: 0x0400A96F RID: 43375
        private Thread thread_0;

        // Token: 0x0400A970 RID: 43376
        private readonly GClass2300<GClass2298.Struct705> gclass2300_0 = new GClass2300<GClass2298.Struct705>();

        // Token: 0x0400A971 RID: 43377
        private readonly GClass2300<GClass2298.Struct706> gclass2300_1 = new GClass2300<GClass2298.Struct706>();

        // Token: 0x0400A972 RID: 43378
        private readonly GClass2300<GClass2298.Struct707> gclass2300_2 = new GClass2300<GClass2298.Struct707>();

        // Token: 0x0400A973 RID: 43379
        private readonly GClass2300<GClass2298.Struct708> gclass2300_3 = new GClass2300<GClass2298.Struct708>();


        // Token: 0x02001FDE RID: 8158
        public struct Struct705
        {
            // Token: 0x0400A975 RID: 43381
            public int Connection;

            // Token: 0x0400A976 RID: 43382
            public string Address;

            // Token: 0x0400A977 RID: 43383
            public int Port;
        }

        // Token: 0x02001FDF RID: 8159
        public struct Struct706
        {
            // Token: 0x0400A978 RID: 43384
            public int Connection;
        }

        // Token: 0x02001FE0 RID: 8160
        public struct Struct707
        {
            // Token: 0x0400A979 RID: 43385
            public int Connection;

            // Token: 0x0400A97A RID: 43386
            public NetworkMessageType Type;

            // Token: 0x0400A97B RID: 43387
            public NetworkChannel Channel;

            // Token: 0x0400A97C RID: 43388
            public ArraySegment<byte> Buffer;
        }

        // Token: 0x02001FE1 RID: 8161
        public struct Struct708
        {
            // Token: 0x0400A97D RID: 43389
            public int Connection;

            // Token: 0x0400A97E RID: 43390
            public NetworkChannel Channel;

            // Token: 0x0400A97F RID: 43391
            public ArraySegment<byte> Buffer;
        }
    }
}
