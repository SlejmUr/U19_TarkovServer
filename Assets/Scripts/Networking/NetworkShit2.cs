using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking.Types;
using UnityEngine.Networking;
using TarkovServerU19.BSGEnums;
using TarkovServerU19.BSGClasses;
using TarkovServerU19.Helpers;

namespace TarkovServerU19.Assets.Scripts.Networking
{
    internal class NetworkShit2
    {
        // Token: 0x170019EE RID: 6638
        // (get) Token: 0x0600BE16 RID: 48662 RVA: 0x003361E4 File Offset: 0x003343E4
        private int Int32_0
        {
            get
            {
                int num = this.int_0;
                this.int_0 = num + 1;
                return num;
            }
        }

        // Token: 0x170019EF RID: 6639
        // (get) Token: 0x0600BE17 RID: 48663 RVA: 0x00336202 File Offset: 0x00334402
        public bool IsStarted
        {
            get
            {
                return this.dictionary_0.Count > 0;
            }
        }

        // Token: 0x0600BE18 RID: 48664 RVA: 0x00014C31 File Offset: 0x00012E31
        public void EarlyUpdate()
        {
        }

        // Token: 0x0600BE19 RID: 48665 RVA: 0x00014C31 File Offset: 0x00012E31
        public void LateUpdate()
        {
        }

        // Token: 0x0600BE1A RID: 48666 RVA: 0x00336212 File Offset: 0x00334412
        private bool method_0(int index, out GClass2298 host, out byte error)
        {
            if (this.dictionary_0.TryGetValue(index, out host))
            {
                error = 0;
                return true;
            }
            error = 1;
            return false;
        }

        // Token: 0x0600BE1B RID: 48667 RVA: 0x0033622C File Offset: 0x0033442C
        public int AddHost(HostTopology topology, int port, string ip)
        {
            GClass2298 gclass = new GClass2298(topology, this.Int32_0, port, ip);
            this.class1917_0 = new Class1917(topology);
            this.dictionary_0.Add(gclass.Index, gclass);
            return gclass.Index;
        }

        // Token: 0x0600BE1C RID: 48668 RVA: 0x0033626C File Offset: 0x0033446C
        public bool RemoveHost(int index)
        {
            GClass2298 gclass;
            byte b;
            if (this.method_0(index, out gclass, out b))
            {
                gclass.Shutdown();
                this.dictionary_0.Remove(gclass.Index);
                return true;
            }
            return false;
        }

        // Token: 0x0600BE1D RID: 48669 RVA: 0x003362A4 File Offset: 0x003344A4
        public int Connect(int hostId, string address, int port, int specialConnectionId, out byte error)
        {
            GClass2298 gclass;
            if (this.method_0(hostId, out gclass, out error))
            {
                return gclass.Connect(address, port, out error);
            }
            return 0;
        }

        // Token: 0x0600BE1E RID: 48670 RVA: 0x003362CC File Offset: 0x003344CC
        public bool Disconnect(int hostId, int connectionId, out byte error)
        {
            GClass2298 gclass;
            return this.method_0(hostId, out gclass, out error) && gclass.Disconnect(connectionId, out error);
        }

        // Token: 0x0600BE1F RID: 48671 RVA: 0x003362F0 File Offset: 0x003344F0
        public void GetConnectionInfo(int hostId, int connectionId, out string address, out int port, out NetworkID network, out NodeID dstNode, out byte error)
        {
            network = NetworkID.Invalid;
            dstNode = NodeID.Invalid;
            GClass2298 gclass;
            if (this.method_0(hostId, out gclass, out error))
            {
                gclass.GetConnectionInfo(connectionId, out address, out port, out error);
                return;
            }
            address = null;
            port = 0;
        }

        // Token: 0x0600BE20 RID: 48672 RVA: 0x00336330 File Offset: 0x00334530
        public int GetRtt(int hostId, int connectionId, out byte error)
        {
            GClass2298 gclass;
            if (this.method_0(hostId, out gclass, out error))
            {
                return gclass.GetRtt(connectionId, out error);
            }
            return 0;
        }

        // Token: 0x0600BE21 RID: 48673 RVA: 0x00336354 File Offset: 0x00334554
        public int GetLossPercent(int hostId, int connectionId, out byte error)
        {
            GClass2298 gclass;
            if (this.method_0(hostId, out gclass, out error))
            {
                return gclass.GetLossPercent(connectionId, out error);
            }
            return 0;
        }

        // Token: 0x0600BE22 RID: 48674 RVA: 0x00336378 File Offset: 0x00334578
        public int GetLossCount(int hostId, int connectionId, out byte error)
        {
            GClass2298 gclass;
            if (this.method_0(hostId, out gclass, out error))
            {
                return gclass.GetLossCount(connectionId, out error);
            }
            return 0;
        }

        // Token: 0x0600BE23 RID: 48675 RVA: 0x0033639C File Offset: 0x0033459C
        public NetworkEventType ReceiveFromHost(int hostId, out int connectionId, out int channelId, byte[] buffer, out int bufferSize, out byte error)
        {
            GClass2298 gclass;
            NetworkMessageType value;
            NetworkChannel insideNetworkChannel;
            if (this.method_0(hostId, out gclass, out error) && gclass.Receive(out connectionId, out value, out insideNetworkChannel, buffer, out bufferSize, out error))
            {
                NetworkEventType result = Class1918.Convert(value);
                channelId = this.class1917_0.ConvertIdentifier(insideNetworkChannel);
                return result;
            }
            connectionId = 0;
            channelId = 0;
            bufferSize = 0;
            return NetworkEventType.Nothing;
        }

        // Token: 0x0600BE24 RID: 48676 RVA: 0x003363EC File Offset: 0x003345EC
        public bool Send(int hostId, int connectionId, int channelId, byte[] buffer, int bufferSize, out byte error)
        {
            GClass2298 gclass;
            if (this.method_0(hostId, out gclass, out error))
            {
                NetworkChannel channel = this.class1917_0.ConvertIdentifier(channelId);
                return gclass.Send(connectionId, channel, buffer, bufferSize, out error);
            }
            return false;
        }

        // Token: 0x0600BE25 RID: 48677 RVA: 0x00336424 File Offset: 0x00334624
        public void Shutdown()
        {
            KeyValuePair<int, GClass2298>[] array = this.dictionary_0.ToArray<KeyValuePair<int, GClass2298>>();
            for (int i = 0; i < array.Length; i++)
            {
                int num;
                GClass2298 gclass;
                array[i].Deconstruct(out num, out gclass);
                int key = num;
                gclass.Shutdown();
                this.dictionary_0.Remove(key);
            }
        }

        // Token: 0x0600BE26 RID: 48678 RVA: 0x00336474 File Offset: 0x00334674
        public GStruct335 GetStatistics(int hostId, int connectionId)
        {
            GClass2298 gclass;
            byte b;
            if (this.method_0(hostId, out gclass, out b))
            {
                return gclass.GetStatistics(connectionId);
            }
            return default(GStruct335);
        }

        // Token: 0x0400A9E2 RID: 43490
        private Dictionary<int, GClass2298> dictionary_0 = new Dictionary<int, GClass2298>();

        // Token: 0x0400A9E3 RID: 43491
        private int int_0;

        // Token: 0x0400A9E4 RID: 43492
        private NetworkShit2.Class1917 class1917_0;

        // Token: 0x02001FF4 RID: 8180
        private class Class1917
        {
            // Token: 0x0600BE28 RID: 48680 RVA: 0x003364B4 File Offset: 0x003346B4
            public Class1917(HostTopology topology)
            {
                for (int i = 0; i < topology.DefaultConfig.Channels.Count; i++)
                {
                    QosType qos = topology.DefaultConfig.Channels[i].QOS;
                    this.list_0.Add(qos);
                    if (this.dictionary_0.ContainsKey(qos))
                    {
                        this.dictionary_0.Add(qos, i);
                    }
                }
            }

            // Token: 0x0600BE29 RID: 48681 RVA: 0x00336536 File Offset: 0x00334736
            public static NetworkChannel Convert(QosType value)
            {
                switch (value)
                {
                    case QosType.Unreliable:
                    case QosType.UnreliableFragmented:
                    case QosType.UnreliableSequenced:
                    case QosType.UnreliableFragmentedSequenced:
                        return NetworkChannel.Unreliable;
                }
                return NetworkChannel.Reliable;
            }

            // Token: 0x0600BE2A RID: 48682 RVA: 0x0033656F File Offset: 0x0033476F
            public static QosType Convert(NetworkChannel value)
            {
                if (value != NetworkChannel.Reliable && value == NetworkChannel.Unreliable)
                {
                    return QosType.Unreliable;
                }
                return QosType.Reliable;
            }

            // Token: 0x0600BE2B RID: 48683 RVA: 0x0033657C File Offset: 0x0033477C
            public NetworkChannel ConvertIdentifier(int outsideChannel)
            {
                return Convert(this.list_0[outsideChannel]);
            }

            // Token: 0x0600BE2C RID: 48684 RVA: 0x00336590 File Offset: 0x00334790
            public int ConvertIdentifier(NetworkChannel insideNetworkChannel)
            {
                int result;
                if (this.dictionary_0.TryGetValue(Convert(insideNetworkChannel), out result))
                {
                    return result;
                }
                return 0;
            }

            // Token: 0x0400A9E5 RID: 43493
            private List<QosType> list_0 = new List<QosType>();

            // Token: 0x0400A9E6 RID: 43494
            private Dictionary<QosType, int> dictionary_0 = new Dictionary<QosType, int>();
        }

        // Token: 0x02001FF5 RID: 8181
        private static class Class1918
        {
            // Token: 0x0600BE2D RID: 48685 RVA: 0x003365B5 File Offset: 0x003347B5
            public static NetworkMessageType Convert(NetworkEventType value)
            {
                switch (value)
                {
                    case NetworkEventType.DataEvent:
                        return NetworkMessageType.Data;
                    case NetworkEventType.ConnectEvent:
                        return NetworkMessageType.Connect;
                    case NetworkEventType.DisconnectEvent:
                        return NetworkMessageType.Disconnect;
                    default:
                        return NetworkMessageType.None;
                }
            }

            // Token: 0x0600BE2E RID: 48686 RVA: 0x003365D0 File Offset: 0x003347D0
            public static NetworkEventType Convert(NetworkMessageType value)
            {
                switch (value)
                {
                    case NetworkMessageType.Connect:
                        return NetworkEventType.ConnectEvent;
                    case NetworkMessageType.Data:
                        return NetworkEventType.DataEvent;
                    case NetworkMessageType.Disconnect:
                        return NetworkEventType.DisconnectEvent;
                }
                return NetworkEventType.Nothing;
            }
        }
    }
}
