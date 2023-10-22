using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using TarkovServerU19.BSGEnums;
using TarkovServerU19.Helpers;
using UnityEngine.Networking;

namespace TarkovServerU19.BSGClasses
{
    internal class NetworkConnectionManager
    {
        public int NextConnectionIndex
        {
            get
            {
                int num = this.int_0;
                this.int_0 = num + 1;
                return num;
            }
        }

        public int Index { get; }

        public NetworkConnectionManager(HostTopology topology, int index, int port, string ip)
        {
            this.configuration = new Configuration
            {
                ConnectionLimit = topology.MaxDefaultConnections,
                PacketSize = (int)topology.DefaultConfig.PacketSize,
                WaitTimeout = topology.DefaultConfig.DisconnectTimeout,
                PingInterval = topology.DefaultConfig.PingTimeout
            };
            this.Index = index;
            this.socket = new Socket(port);
            this.remoteEndPoint = new RemoteEndPoint(IPAddress.Any, port);
            this.packetToSend = new byte[this.configuration.PacketSize];
        }

        private bool Boolean_0
        {
            get
            {
                return this.ConnectionDictIndex.Count >= this.configuration.ConnectionLimit;
            }
        }

        private bool TryGetConnection(int key, out Connection connection, out byte error)
        {
            return this.GetOutConnection(this.ConnectionDictIndex, key, out connection, out error);
        }

        private bool IsEndPointConnected(EndPoint address, out Connection connection, out byte error)
        {
            int key = Connection.CreateHash(address);
            return this.GetOutConnection(this.ConnectionDictHash, key, out connection, out error);
        }

        private bool GetOutConnection(Dictionary<int, Connection> dictionary, int key, out Connection connection, out byte error)
        {
            if (dictionary.TryGetValue(key, out connection))
            {
                error = 0;
                return true;
            }
            error = 2;
            return false;
        }

        private Connection CreateNewConnection(int index, RemoteEndPoint address)
        {
            Connection connection;
            byte b;
            if (this.IsEndPointConnected(address, out connection, out b))
            {
                return connection;
            }
            connection = new Connection(index, this.socket, address, this.configuration);
            this.ConnectionDictIndex.Add(connection.Index, connection);
            this.ConnectionDictHash.Add(connection.Hash, connection);
            return connection;
        }

        private void AddToConnection(Connection connection)
        {
            this.Connections.Add(connection);
        }

        private void ClearConnections()
        {
            foreach (Connection connection in this.Connections)
            {
                this.ConnectionDictIndex.Remove(connection.Index);
                this.ConnectionDictHash.Remove(connection.Hash);
            }
            this.Connections.Clear();
        }

        private void AddToConnection2(Connection connection)
        {
            this.AddToConnection(connection);
        }

        public int Connect(int index, string address, int port, out byte error)
        {
            this.remoteEndPoint = new RemoteEndPoint(IPAddress.Parse(address), port);
            Connection connection = this.CreateNewConnection(index, this.remoteEndPoint);
            connection.Connect();
            error = 0;
            return connection.Index;
        }

        public bool Disconnect(int index, out byte error)
        {
            Connection connection;
            if (this.TryGetConnection(index, out connection, out error))
            {
                connection.Disconnect();
                return true;
            }
            return false;
        }

        public void GetConnectionInfo(int index, out string address, out int port, out byte error)
        {
            Connection connection;
            if (this.TryGetConnection(index, out connection, out error))
            {
                connection.GetInformation(out address, out port, out error);
                return;
            }
            address = null;
            port = 0;
        }

        public int GetRtt(int index, out byte error)
        {
            Connection connection;
            if (this.TryGetConnection(index, out connection, out error))
            {
                return connection.GetRtt();
            }
            return 0;
        }

        public int GetLossPercent(int index, out byte error)
        {
            Connection connection;
            if (this.TryGetConnection(index, out connection, out error))
            {
                return connection.GetLossPercent();
            }
            return 0;
        }

        public int GetLossCount(int index, out byte error)
        {
            Connection connection;
            if (this.TryGetConnection(index, out connection, out error))
            {
                return connection.GetLossCount();
            }
            return 0;
        }

        public int EarlyUpdate(float maxSpendTime)
        {
            int num = 0;
            bool flag = false;
            this.ClearConnections();
            this.stopwatch.Reset();
            this.stopwatch.Start();
            try
            {
                while (this.socket.Receive(this.packetToSend, out this.int_2, this.remoteEndPoint))
                {
                    Connection connection;
                    byte b;
                    if (!this.IsEndPointConnected(this.remoteEndPoint, out connection, out b))
                    {
                        if (this.Boolean_0)
                        {
                            continue;
                        }
                        this.ipEndPoint = this.remoteEndPoint.DeepCopyIPEndPoint();
                        connection = this.CreateNewConnection(this.NextConnectionIndex, new RemoteEndPoint(this.ipEndPoint.Address, this.ipEndPoint.Port));
                    }
                    connection.HandelReceive(this.packetToSend, this.int_2);
                    num++;
                    if (StopwatchExtensions.ElapsedSeconds(this.stopwatch) >= maxSpendTime)
                    {
                        flag = true;
                        break;
                    }
                }
            }
            catch (Exception arg)
            {
                UnityEngine.Debug.Log(string.Format("Error while receiving data from socket: {0}", arg));
            }
            this.stopwatch.Stop();
            foreach (KeyValuePair<int, Connection> kvp in this.ConnectionDictIndex)
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

        public void LateUpdate()
        {
            foreach (KeyValuePair<int, Connection> kvp in this.ConnectionDictIndex)
            {
                int num;
                Connection connection;
                kvp.Deconstruct(out num, out connection);
                connection.LateUpdate();
            }
        }

        public bool Send(int index, MessageSegment message, out byte error)
        {
            Connection connection;
            if (this.TryGetConnection(index, out connection, out error))
            {
                connection.Send(message);
                return true;
            }
            return false;
        }

        public bool Receive(out int index, out MessageSegment message, out byte error)
        {
            foreach (KeyValuePair<int, Connection> kvp in this.ConnectionDictIndex)
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
                        this.AddToConnection2(connection2);
                    }
                    return true;
                }
            }
            index = 0;
            message = null;
            error = 0;
            return false;
        }

        public void Shutdown()
        {
            this.socket.Shutdown();
        }

        public UDPStats GetStatistics(int index)
        {
            Connection connection;
            byte b;
            if (this.TryGetConnection(index, out connection, out b))
            {
                return connection.GetStatistics();
            }
            return default(UDPStats);
        }

        private Dictionary<int, Connection> ConnectionDictIndex = new Dictionary<int, Connection>();
        private Dictionary<int, Connection> ConnectionDictHash = new Dictionary<int, Connection>();
        private int int_0;
        private Socket socket;
        private RemoteEndPoint remoteEndPoint;
        private IPEndPoint ipEndPoint;
        private Configuration configuration;
        private readonly int int_1;
        private readonly byte[] packetToSend;
        private int int_2;
        private Stopwatch stopwatch = new Stopwatch();
        private List<Connection> Connections = new List<Connection>();
    }
}
