using System;
using UnityEngine;
using UnityEngine.Networking;

namespace TarkovServerU19
{
    public class ServerManager : NetworkBehaviour
    {
        public TarkovNetworkServer server;

		[SerializeField]
		public string ServerIP;

        [SerializeField]
        public int ServerPort;


		public void SetServerIP(string IP)
		{
			ServerIP = IP;
		}


        public void SetServerPort(string port)
        {
            ServerPort = int.Parse(port);
        }

        public void StartServer()
        {
            try
            {
                Debug.developerConsoleVisible = true;
                LogFilter.current = LogFilter.FilterLevel.Developer;
                var tarkovConfig = ServerHelper.GetConnectionConfig();
                HostTopology hostTopology = new HostTopology(tarkovConfig, 100);
                server = new TarkovNetworkServer();
                server.Configure(hostTopology);
                server.Initialize();
				//NetworkManager.activeTransport = new Networking.NetworkTransportShit();
                NetworkManager.activeTransport.Init();
                var isListen = server.Listen(ServerIP, ServerPort);
                Console.WriteLine(isListen);
                Debug.Log("Server Started");
                server.RegisterHandler(32, Loaded); //OnConnect
                server.RegisterHandler(33, Loaded); //OnDisconnect
                server.RegisterHandler(35, Loaded); //MsgType.Ready
                server.RegisterHandler(36, Loaded); //MsgType.NotReady
                server.RegisterHandler(147, Loaded); //Sending Request to join + OnAcceptResponse
                server.RegisterHandler(148, Loaded); //OnRejectResponse (Contains Error in Int32)
                server.RegisterHandler(168, Loaded); //battlEye packets
                server.RegisterHandler(185, Loaded); //OnPartialCommand things | 0x020012D6 on latest (13.5)
                server.RegisterHandler(188, Loaded); //The nightmare (ProfileId, Resourses Json, Customiation Json)
                server.RegisterHandler(189, Loaded); //sync progress (to client)
                server.RegisterHandler(190, Loaded); //sync progress (from client)
                
                /*
				server.RegisterHandler(151, new NetworkMessageDelegate(this.method_14)); //spwaning world
				server.RegisterHandler(152, new NetworkMessageDelegate(this.interface7_0.WorldUnspawn)); //unspawing world
				server.RegisterHandler(191, new NetworkMessageDelegate(this.interface7_0.SubWorldSpawnLoot)); //SubWorldSpawnLoot
				server.RegisterHandler(192, new NetworkMessageDelegate(this.interface7_0.SubWorldSpawnSearchLoot)); //SubWorldSpawnSearchLoot
				server.RegisterHandler(154, new NetworkMessageDelegate(this.interface7_0.SubWorldUnspawn)); //SubWorldUnspawn
				server.RegisterHandler(156, new NetworkMessageDelegate(this.method_21)); //PlayerUnspawn
				server.RegisterHandler(158, new NetworkMessageDelegate(this.method_23)); //ObserverUnspawn
				server.RegisterHandler(160, new NetworkMessageDelegate(this.interface7_0.DeathInventorySync)); //DeathInventorySync
				server.RegisterHandler(155, new Action<NetworkConnection, NetworkReader>(this.method_20)); //spawn player
				server.RegisterHandler(157, new Action<NetworkConnection, NetworkReader>(this.method_22)); //ObserverSpawn

				server.RegisterHandler(170, new NetworkMessageDelegate(this.method_10)); //big methods processing
				server.RegisterHandler(171, new NetworkMessageDelegate(this.gclass2201_0.OnSpawnObservedPlayer)); //OnSpawnObservedPlayer
				server.RegisterHandler(172, new NetworkMessageDelegate(this.gclass2201_0.OnSpawnObservedPlayers)); //OnSpawnObservedPlayers
				server.RegisterHandler(175, new NetworkMessageDelegate(this.gclass2201_0.OnChangeFramerate)); //OnChangeFramerate
				server.RegisterHandler(173, new NetworkMessageDelegate(this.gclass2201_0.OnSnapshotObservedPlayers)); //OnSnapshotObservedPlayers
				server.RegisterHandler(174, new NetworkMessageDelegate(this.gclass2201_0.OnCommandsObservedPlayers)); //OnCommandsObservedPlayers
				*/
                RegisterCommandDelegate(typeof(Test), -1337, Test.LogMe); //testing
                /*
			NetworkBehaviour.RegisterCommandDelegate(typeof(AbstractGameSession), -1723132743, new NetworkBehaviour.CmdDelegate(InvokeCmdCmdSpawn));
			int_1 = 740792038;
			NetworkBehaviour.RegisterCommandDelegate(typeof(AbstractGameSession), int_1, new NetworkBehaviour.CmdDelegate(InvokeCmdCmdRespawn));
			int_2 = -1220356686;
			NetworkBehaviour.RegisterCommandDelegate(typeof(AbstractGameSession), int_2, new NetworkBehaviour.CmdDelegate(InvokeCmdCmdStartGame));
			int_3 = 1792897173;
			NetworkBehaviour.RegisterCommandDelegate(typeof(AbstractGameSession), int_3, new NetworkBehaviour.CmdDelegate(InvokeCmdCmdStartGameAfterTeleport));
			int_4 = 273195288;
			NetworkBehaviour.RegisterCommandDelegate(typeof(AbstractGameSession), int_4, new NetworkBehaviour.CmdDelegate(InvokeCmdCmdRestartGameInitiate));
			int_5 = -1501005473;
			NetworkBehaviour.RegisterCommandDelegate(typeof(AbstractGameSession), int_5, new NetworkBehaviour.CmdDelegate(InvokeCmdCmdRestartGame));
			int_6 = -40021267;
			NetworkBehaviour.RegisterCommandDelegate(typeof(AbstractGameSession), int_6, new NetworkBehaviour.CmdDelegate(InvokeCmdCmdGameStarted));
			int_7 = -750099178;
			NetworkBehaviour.RegisterCommandDelegate(typeof(AbstractGameSession), int_7, new NetworkBehaviour.CmdDelegate(InvokeCmdCmdStopGame));
			int_8 = 463608476;
			NetworkBehaviour.RegisterCommandDelegate(typeof(AbstractGameSession), int_8, new NetworkBehaviour.CmdDelegate(InvokeCmdCmdSyncGameTime));
			int_9 = -1035840717;
			NetworkBehaviour.RegisterCommandDelegate(typeof(AbstractGameSession), int_9, new NetworkBehaviour.CmdDelegate(InvokeCmdCmdDevelopRequestBot));
			int_10 = 1432950484;
			NetworkBehaviour.RegisterCommandDelegate(typeof(AbstractGameSession), int_10, new NetworkBehaviour.CmdDelegate(InvokeCmdCmdDevelopRequestBotZones));
			int_11 = 930653927;
			NetworkBehaviour.RegisterCommandDelegate(typeof(AbstractGameSession), int_11, new NetworkBehaviour.CmdDelegate(InvokeCmdCmdDevelopRequestBotGroups));
			int_12 = -1581543574;
			NetworkBehaviour.RegisterCommandDelegate(typeof(AbstractGameSession), int_12, new NetworkBehaviour.CmdDelegate(InvokeCmdCmdDevelopmentSpawnBotRequest));
			int_13 = 102630535;
			NetworkBehaviour.RegisterCommandDelegate(typeof(AbstractGameSession), int_13, new NetworkBehaviour.CmdDelegate(InvokeCmdCmdDevelopmentSpawnBotOnServer));
			int_14 = -349255409;
			NetworkBehaviour.RegisterCommandDelegate(typeof(AbstractGameSession), int_14, new NetworkBehaviour.CmdDelegate(InvokeCmdCmdDevelopmentSpawnBotOnClient));
			int_15 = -1733636721;
			NetworkBehaviour.RegisterCommandDelegate(typeof(AbstractGameSession), int_15, new NetworkBehaviour.CmdDelegate(InvokeCmdCmdDisconnectAcceptedOnClient));
			int_16 = 1240699829;
			NetworkBehaviour.RegisterCommandDelegate(typeof(AbstractGameSession), int_16, new NetworkBehaviour.CmdDelegate(InvokeCmdCmdWorldSpawnConfirm));
			int_17 = -1317447737;
			NetworkBehaviour.RegisterCommandDelegate(typeof(AbstractGameSession), int_17, new NetworkBehaviour.CmdDelegate(InvokeCmdCmdSpawnConfirm));
			int_18 = 810388720;
			NetworkBehaviour.RegisterCommandDelegate(typeof(AbstractGameSession), int_18, new NetworkBehaviour.CmdDelegate(InvokeCmdCmdReportVoipAbuse));
			int_19 = 905971479;
			NetworkBehaviour.RegisterCommandDelegate(typeof(AbstractGameSession), int_19, new NetworkBehaviour.CmdDelegate(InvokeCmdCmdPlayerEffectsPause));
			int_20 = -65034947;
			NetworkBehaviour.RegisterCommandDelegate(typeof(AbstractGameSession), int_20, new NetworkBehaviour.CmdDelegate(InvokeCmdCmdOnPlayerKeeperStatisticsChanged));
			int_21 = -942910572;
			NetworkBehaviour.RegisterCommandDelegate(typeof(AbstractGameSession), int_21, new NetworkBehaviour.CmdDelegate(InvokeCmdCmdGetRadiotransmitterData));
			int_22 = -1952818640;
			NetworkBehaviour.RegisterRpcDelegate(typeof(AbstractGameSession), int_22, new NetworkBehaviour.CmdDelegate(InvokeRpcRpcGameSpawned));
			int_23 = 2117859815;
			NetworkBehaviour.RegisterRpcDelegate(typeof(AbstractGameSession), int_23, new NetworkBehaviour.CmdDelegate(InvokeRpcRpcGameMatching));
			int_24 = -1157222870;
			NetworkBehaviour.RegisterRpcDelegate(typeof(AbstractGameSession), int_24, new NetworkBehaviour.CmdDelegate(InvokeRpcRpcGameStarting));
			int_25 = 1572370779;
			NetworkBehaviour.RegisterRpcDelegate(typeof(AbstractGameSession), int_25, new NetworkBehaviour.CmdDelegate(InvokeRpcRpcGameStartingWithTeleport));
			int_26 = -1838445225;
			NetworkBehaviour.RegisterRpcDelegate(typeof(AbstractGameSession), int_26, new NetworkBehaviour.CmdDelegate(InvokeRpcRpcGameStarted));
			int_27 = 94275293;
			NetworkBehaviour.RegisterRpcDelegate(typeof(AbstractGameSession), int_27, new NetworkBehaviour.CmdDelegate(InvokeRpcRpcGameRestarting));
			int_28 = -1243884988;
			NetworkBehaviour.RegisterRpcDelegate(typeof(AbstractGameSession), int_28, new NetworkBehaviour.CmdDelegate(InvokeRpcRpcGameRestarted));
			int_29 = -758380962;
			NetworkBehaviour.RegisterRpcDelegate(typeof(AbstractGameSession), int_29, new NetworkBehaviour.CmdDelegate(InvokeRpcRpcGameStopping));
			int_30 = -1825579357;
			NetworkBehaviour.RegisterRpcDelegate(typeof(AbstractGameSession), int_30, new NetworkBehaviour.CmdDelegate(InvokeRpcRpcGameStopped));
			int_31 = 547040626;
			NetworkBehaviour.RegisterRpcDelegate(typeof(AbstractGameSession), int_31, new NetworkBehaviour.CmdDelegate(InvokeRpcRpcSyncGameTime));
			int_32 = 1152897188;
			NetworkBehaviour.RegisterRpcDelegate(typeof(AbstractGameSession), int_32, new NetworkBehaviour.CmdDelegate(InvokeRpcRpcDevelopSendBotData));
			int_33 = -1920895376;
			NetworkBehaviour.RegisterRpcDelegate(typeof(AbstractGameSession), int_33, new NetworkBehaviour.CmdDelegate(InvokeRpcRpcDevelopSendBotDataZone));
			int_34 = 314346392;
			NetworkBehaviour.RegisterRpcDelegate(typeof(AbstractGameSession), int_34, new NetworkBehaviour.CmdDelegate(InvokeRpcRpcDevelopSendBotDataGroups));
			int_35 = -1269941968;
			NetworkBehaviour.RegisterRpcDelegate(typeof(AbstractGameSession), int_35, new NetworkBehaviour.CmdDelegate(InvokeRpcRpcDevelopmentSpawnBotResponse));
			int_36 = -435294673;
			NetworkBehaviour.RegisterRpcDelegate(typeof(AbstractGameSession), int_36, new NetworkBehaviour.CmdDelegate(InvokeRpcRpcSoftStopNotification));
			int_37 = 1124901489;
			NetworkBehaviour.RegisterRpcDelegate(typeof(AbstractGameSession), int_37, new NetworkBehaviour.CmdDelegate(InvokeRpcRpcStartDisconnectionProcedure));
			int_38 = 1547608889;
			NetworkBehaviour.RegisterRpcDelegate(typeof(AbstractGameSession), int_38, new NetworkBehaviour.CmdDelegate(InvokeRpcRpcVoipAbuseNotification));
			int_39 = -2040405782;
			NetworkBehaviour.RegisterRpcDelegate(typeof(AbstractGameSession), int_39, new NetworkBehaviour.CmdDelegate(InvokeRpcRpcAirdropContainerData));
			int_40 = -689857055;
			NetworkBehaviour.RegisterRpcDelegate(typeof(AbstractGameSession), int_40, new NetworkBehaviour.CmdDelegate(InvokeRpcRpcMineDirectionExplosion));
			int_41 = -2141949542;
			NetworkBehaviour.RegisterRpcDelegate(typeof(AbstractGameSession), int_41, new NetworkBehaviour.CmdDelegate(InvokeRpcRpcSuccessAirdropFlareEvent));
			int_42 = 778150830;
			NetworkBehaviour.RegisterRpcDelegate(typeof(AbstractGameSession), int_42, new NetworkBehaviour.CmdDelegate(InvokeRpcRpcBufferZoneData));
			int_43 = -52162261;
			NetworkBehaviour.RegisterRpcDelegate(typeof(AbstractGameSession), int_43, new NetworkBehaviour.CmdDelegate(InvokeRpcRpcSendClientRadioTransmitterData));
			int_44 = 1358208182;
			NetworkBehaviour.RegisterRpcDelegate(typeof(AbstractGameSession), int_44, new NetworkBehaviour.CmdDelegate(InvokeRpcRpcSendObserverRadioTransmitterData));
			int_45 = 361141025;
			NetworkBehaviour.RegisterRpcDelegate(typeof(AbstractGameSession), int_45, new NetworkBehaviour.CmdDelegate(InvokeRpcRpcSyncLighthouseTraderZoneData));
                 */
                NetworkCRC.RegisterBehaviour("AbstractGameSession", 0);
                /*
				NetworkBehaviour.RegisterCommandDelegate(typeof(HlapiPlayer), HlapiPlayer.int_0, new NetworkBehaviour.CmdDelegate(HlapiPlayer.InvokeCmdCmdSetPlayerName));
				HlapiPlayer.int_1 = 1332331777;
				NetworkBehaviour.RegisterRpcDelegate(typeof(HlapiPlayer), HlapiPlayer.int_1, new NetworkBehaviour.CmdDelegate(HlapiPlayer.InvokeRpcRpcSetPlayerName));
				*/
				NetworkCRC.RegisterBehaviour("HlapiPlayer", 0);
                Debug.Log("RegisterCommandDelegate's done");
            }
            catch (Exception ex)
            {
                Debug.Log(ex);
            }

        }

        public static void Loaded(NetworkMessage msg)
        {
            Debug.Log(msg.channelId + " " + msg.msgType + " " + msg.conn.connectionId);
        }




        void LateUpdate()
        {
            if (server != null)
            {
                if (server.serverHostId != -1)
                {
                    //Debug.Log("Timer");
                    server.Update();
                }
            }
        }


        public void Stop()
        {
            server.Stop();
            Console.WriteLine("MLServer Stopped!");
        }
    }
}
