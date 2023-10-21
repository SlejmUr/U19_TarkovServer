using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TarkovServerU19.BSGClasses;
using TarkovServerU19.BSGEnums;
using TarkovServerU19.Helpers;
using UnityEngine.Networking;

namespace TarkovServerU19.BSGClasses
{
    internal class GClass2299
    {
        // Token: 0x170019E1 RID: 6625
        // (get) Token: 0x0600BDB6 RID: 48566 RVA: 0x00334AB0 File Offset: 0x00332CB0
        public int NextConnectionIndex
        {
            get
            {
                int num = this.int_0;
                this.int_0 = num + 1;
                return num;
            }
        }

        // Token: 0x170019E2 RID: 6626
        // (get) Token: 0x0600BDB7 RID: 48567 RVA: 0x00334ACE File Offset: 0x00332CCE
        public int Index { get; }

        // Token: 0x0600BDB8 RID: 48568 RVA: 0x00334AD8 File Offset: 0x00332CD8
        public GClass2299(HostTopology topology, int index, int port, string ip)
        {
            this.gclass2290_0 = new Configuration
            {
                ConnectionLimit = topology.MaxDefaultConnections,
                PacketSize = (int)topology.DefaultConfig.PacketSize,
                WaitTimeout = topology.DefaultConfig.DisconnectTimeout,
                PingInterval = topology.DefaultConfig.PingTimeout
            };
            this.Index = index;
            this.gclass2311_0 = new Socket(port);
            this.gclass2288_0 = new RemoteEndPoint(IPAddress.Any, port);
            this.byte_0 = new byte[this.gclass2290_0.PacketSize];
        }

        // Token: 0x170019E3 RID: 6627
        // (get) Token: 0x0600BDB9 RID: 48569 RVA: 0x00334BA6 File Offset: 0x00332DA6
        private bool Boolean_0
        {
            get
            {
                return this.dictionary_0.Count >= this.gclass2290_0.ConnectionLimit;
            }
        }

        // Token: 0x0600BDBA RID: 48570 RVA: 0x00334BC3 File Offset: 0x00332DC3
        private bool method_0(int key, out Connection connection, out byte error)
        {
            return this.method_2(this.dictionary_0, key, out connection, out error);
        }

        // Token: 0x0600BDBB RID: 48571 RVA: 0x00334BD4 File Offset: 0x00332DD4
        private bool method_1(EndPoint address, out Connection connection, out byte error)
        {
            int key = Connection.CreateHash(address);
            return this.method_2(this.dictionary_1, key, out connection, out error);
        }

        // Token: 0x0600BDBC RID: 48572 RVA: 0x00334BF7 File Offset: 0x00332DF7
        private bool method_2(Dictionary<int, Connection> dictionary, int key, out Connection connection, out byte error)
        {
            if (dictionary.TryGetValue(key, out connection))
            {
                error = 0;
                return true;
            }
            error = 2;
            return false;
        }

        // Token: 0x0600BDBD RID: 48573 RVA: 0x00334C10 File Offset: 0x00332E10
        private Connection method_3(int index, RemoteEndPoint address)
        {
            Connection connection;
            byte b;
            if (this.method_1(address, out connection, out b))
            {
                return connection;
            }
            connection = new Connection(index, this.gclass2311_0, address, this.gclass2290_0);
            this.dictionary_0.Add(connection.Index, connection);
            this.dictionary_1.Add(connection.Hash, connection);
            return connection;
        }

        // Token: 0x0600BDBE RID: 48574 RVA: 0x00334C65 File Offset: 0x00332E65
        private void method_4(Connection connection)
        {
            this.list_0.Add(connection);
        }

        // Token: 0x0600BDBF RID: 48575 RVA: 0x00334C74 File Offset: 0x00332E74
        private void method_5()
        {
            foreach (Connection connection in this.list_0)
            {
                this.dictionary_0.Remove(connection.Index);
                this.dictionary_1.Remove(connection.Hash);
            }
            this.list_0.Clear();
        }

        // Token: 0x0600BDC0 RID: 48576 RVA: 0x00334CF0 File Offset: 0x00332EF0
        private void method_6(Connection connection)
        {
            this.method_4(connection);
        }

        // Token: 0x0600BDC1 RID: 48577 RVA: 0x00334CF9 File Offset: 0x00332EF9
        public int Connect(int index, string address, int port, out byte error)
        {
            this.gclass2288_0 = new RemoteEndPoint(IPAddress.Parse(address), port);
            Connection connection = this.method_3(index, this.gclass2288_0);
            connection.Connect();
            error = 0;
            return connection.Index;
        }

        // Token: 0x0600BDC2 RID: 48578 RVA: 0x00334D2C File Offset: 0x00332F2C
        public bool Disconnect(int index, out byte error)
        {
            Connection connection;
            if (this.method_0(index, out connection, out error))
            {
                connection.Disconnect();
                return true;
            }
            return false;
        }

        // Token: 0x0600BDC3 RID: 48579 RVA: 0x00334D50 File Offset: 0x00332F50
        public void GetConnectionInfo(int index, out string address, out int port, out byte error)
        {
            Connection connection;
            if (this.method_0(index, out connection, out error))
            {
                connection.GetInformation(out address, out port, out error);
                return;
            }
            address = null;
            port = 0;
        }

        // Token: 0x0600BDC4 RID: 48580 RVA: 0x00334D7C File Offset: 0x00332F7C
        public int GetRtt(int index, out byte error)
        {
            Connection connection;
            if (this.method_0(index, out connection, out error))
            {
                return connection.GetRtt();
            }
            return 0;
        }

        // Token: 0x0600BDC5 RID: 48581 RVA: 0x00334DA0 File Offset: 0x00332FA0
        public int GetLossPercent(int index, out byte error)
        {
            Connection connection;
            if (this.method_0(index, out connection, out error))
            {
                return connection.GetLossPercent();
            }
            return 0;
        }

        // Token: 0x0600BDC6 RID: 48582 RVA: 0x00334DC4 File Offset: 0x00332FC4
        public int GetLossCount(int index, out byte error)
        {
            Connection connection;
            if (this.method_0(index, out connection, out error))
            {
                return connection.GetLossCount();
            }
            return 0;
        }

        // Token: 0x0600BDC7 RID: 48583 RVA: 0x00334DE8 File Offset: 0x00332FE8
        public int EarlyUpdate(float maxSpendTime)
        {
            int num = 0;
            bool flag = false;
            this.method_5();
            this.stopwatch_0.Reset();
            this.stopwatch_0.Start();
            try
            {
                while (this.gclass2311_0.Receive(this.byte_0, out this.int_2, this.gclass2288_0))
                {
                    Connection connection;
                    byte b;
                    if (!this.method_1(this.gclass2288_0, out connection, out b))
                    {
                        if (this.Boolean_0)
                        {
                            continue;
                        }
                        this.ipendPoint_0 = this.gclass2288_0.DeepCopyIPEndPoint();
                        connection = this.method_3(this.NextConnectionIndex, new RemoteEndPoint(this.ipendPoint_0.Address, this.ipendPoint_0.Port));
                    }
                    connection.HandelReceive(this.byte_0, this.int_2);
                    num++;
                    if (StopwatchExtensions.ElapsedSeconds(this.stopwatch_0) >= maxSpendTime)
                    {
                        flag = true;
                        break;
                    }
                }
            }
            catch (Exception arg)
            {
                //this.Logger.LogWarn(string.Format("Error while receiving data from socket: {0}", arg), Array.Empty<object>());
            }
            this.stopwatch_0.Stop();
            foreach (KeyValuePair<int, Connection> kvp in this.dictionary_0)
            {
                int num2;
                Connection connection2;
                kvp.Deconstruct(out num2, out connection2);
                connection2.EarlyUpdate();
            }
            if (!flag)
            {
                return -1;
            }
            return num;
        }

        // Token: 0x0600BDC8 RID: 48584 RVA: 0x00334F44 File Offset: 0x00333144
        public void LateUpdate()
        {
            foreach (KeyValuePair<int, Connection> kvp in this.dictionary_0)
            {
                int num;
                Connection connection;
                kvp.Deconstruct(out num, out connection);
                connection.LateUpdate();
            }
        }

        // Token: 0x0600BDC9 RID: 48585 RVA: 0x00334FA0 File Offset: 0x003331A0
        public bool Send(int index, MessageSegment message, out byte error)
        {
            Connection connection;
            if (this.method_0(index, out connection, out error))
            {
                connection.Send(message);
                return true;
            }
            return false;
        }

        // Token: 0x0600BDCA RID: 48586 RVA: 0x00334FC4 File Offset: 0x003331C4
        public bool Receive(out int index, out MessageSegment message, out byte error)
        {
            foreach (KeyValuePair<int, Connection> kvp in this.dictionary_0)
            {
                int num;
                Connection connection;
                kvp.Deconstruct(out num, out connection);
                Connection connection2 = connection;
                if (connection2.Receive(out message))
                {
                    index = connection2.Index;
                    error = 0;
                    if (message.Type == NetworkMessageType.Disconnect)
                    {
                        this.method_6(connection2);
                    }
                    return true;
                }
            }
            index = 0;
            message = null;
            error = 0;
            return false;
        }

        // Token: 0x0600BDCB RID: 48587 RVA: 0x00335054 File Offset: 0x00333254
        public void Shutdown()
        {
            this.gclass2311_0.Shutdown();
        }

        // Token: 0x0600BDCC RID: 48588 RVA: 0x00335064 File Offset: 0x00333264
        public GStruct335 GetStatistics(int index)
        {
            Connection connection;
            byte b;
            if (this.method_0(index, out connection, out b))
            {
                return connection.GetStatistics();
            }
            return default(GStruct335);
        }

        // Token: 0x0400A980 RID: 43392
        private Dictionary<int, Connection> dictionary_0 = new Dictionary<int, Connection>();

        // Token: 0x0400A981 RID: 43393
        private Dictionary<int, Connection> dictionary_1 = new Dictionary<int, Connection>();


        // Token: 0x0400A983 RID: 43395
        private int int_0;

        // Token: 0x0400A984 RID: 43396
        private Socket gclass2311_0;

        // Token: 0x0400A985 RID: 43397
        private RemoteEndPoint gclass2288_0;

        // Token: 0x0400A986 RID: 43398
        private IPEndPoint ipendPoint_0;

        // Token: 0x0400A987 RID: 43399
        private Configuration gclass2290_0;

        // Token: 0x0400A988 RID: 43400
        private readonly int int_1;

        // Token: 0x0400A989 RID: 43401
        private readonly byte[] byte_0;

        // Token: 0x0400A98A RID: 43402
        private int int_2;

        // Token: 0x0400A98B RID: 43403
        private Stopwatch stopwatch_0 = new Stopwatch();

        // Token: 0x0400A98C RID: 43404
        private List<Connection> list_0 = new List<Connection>();
    }
}
