using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarkovServerU19.BSGEnums;
using UnityEngine;
using UnityEngine.Networking;

namespace TarkovServerU19.BSGClasses
{
    internal class AbstractGameSession : NetworkBehaviour
    {
        internal static NetworkHash128 NetworkHash128_0
        {
            get
            {
                return NetworkHash128.Parse("d");
            }
        }

        [Command]
        protected virtual void CmdSpawn()
        {
        }

        [Command]
        protected virtual void CmdRespawn()
        {
        }

        [Command]
        protected virtual void CmdStartGame()
        {
        }

        [Command]
        protected virtual void CmdStartGameAfterTeleport()
        {
        }

        [Command]
        protected virtual void CmdRestartGameInitiate()
        {
        }

        [Command]
        protected virtual void CmdRestartGame()
        {
        }

        [Command]
        protected virtual void CmdGameStarted()
        {
        }

        [Command]
        protected virtual void CmdStopGame()
        {
        }

        [Command]
        protected virtual void CmdSyncGameTime()
        {
        }

        [Command]
        protected virtual void CmdDevelopRequestBot(string profileId)
        {
        }

        [Command]
        protected virtual void CmdDevelopRequestBotZones()
        {
        }

        [Command]
        protected virtual void CmdDevelopRequestBotGroups()
        {
        }

        [Command]
        protected virtual void CmdDevelopmentSpawnBotRequest(EPlayerSide side)
        {
        }

        [Command]
        protected virtual void CmdDevelopmentSpawnBotOnServer(EPlayerSide side)
        {
        }

        [Command]
        protected virtual void CmdDevelopmentSpawnBotOnClient(EPlayerSide side, int instanceId)
        {
        }

        [Command]
        protected virtual void CmdDisconnectAcceptedOnClient()
        {
        }

        [Command]
        protected virtual void CmdWorldSpawnConfirm()
        {
        }

        [Command]
        protected virtual void CmdSpawnConfirm(int playerId)
        {
        }

        [Command]
        protected virtual void CmdReportVoipAbuse()
        {
        }

        [Command]
        protected virtual void CmdPlayerEffectsPause(string playerProfileID, bool isPaused)
        {
        }

        [Command]
        protected virtual void CmdOnPlayerKeeperStatisticsChanged(string playerProfileID, CounterTag statisticsType, int valueToSet)
        {
        }

        [Command]
        protected virtual void CmdGetRadiotransmitterData(string playerProfileID)
        {
        }

        [ClientRpc]
        protected virtual void RpcGameSpawned()
        {
        }

        [ClientRpc]
        protected virtual void RpcGameMatching(ushort activitiesCounter, ushort minCounter, int seconds)
        {
        }

        [ClientRpc]
        protected virtual void RpcGameStarting(int seconds)
        {
        }

        [ClientRpc]
        protected virtual void RpcGameStartingWithTeleport(Vector3 position, int exfiltrationId, string entryPoint)
        {
        }

        [ClientRpc]
        protected virtual void RpcGameStarted(float pastTime, int sessionSeconds)
        {
        }

        [ClientRpc]
        protected virtual void RpcGameRestarting()
        {
        }

        [ClientRpc]
        protected virtual void RpcGameRestarted()
        {
        }

        [ClientRpc]
        protected virtual void RpcGameStopping()
        {
        }

        [ClientRpc]
        protected virtual void RpcGameStopped(ExitStatus exitStatus, int playTime)
        {
        }

        [ClientRpc]
        protected virtual void RpcSyncGameTime(long time)
        {
        }

        [ClientRpc]
        protected virtual void RpcDevelopSendBotData(byte[] data)
        {
        }

        [ClientRpc]
        protected virtual void RpcDevelopSendBotDataZone(byte[] data)
        {
        }

        [ClientRpc]
        protected virtual void RpcDevelopSendBotDataGroups(byte[] data)
        {
        }

        [ClientRpc]
        protected virtual void RpcDevelopmentSpawnBotResponse(EPlayerSide side, int instanceId)
        {
        }

        [ClientRpc]
        protected virtual void RpcSoftStopNotification(int sessionSeconds)
        {
        }

        [ClientRpc]
        protected virtual void RpcStartDisconnectionProcedure(int disconnectionCode, string additionalInfo, string technicalMessage)
        {
        }

        [ClientRpc]
        protected virtual void RpcVoipAbuseNotification(string reporter)
        {
        }

        [ClientRpc]
        protected virtual void RpcAirdropContainerData(byte[] data)
        {
        }

        [ClientRpc]
        protected virtual void RpcMineDirectionExplosion(byte[] data)
        {
        }

        [ClientRpc]
        protected virtual void RpcSuccessAirdropFlareEvent(bool canSendAirdrop)
        {
        }

        [ClientRpc]
        protected virtual void RpcBufferZoneData(byte[] data)
        {
        }

        [ClientRpc]
        protected virtual void RpcSendClientRadioTransmitterData(RadioTransmitterData data)
        {
        }

        [ClientRpc]
        protected virtual void RpcSendObserverRadioTransmitterData(RadioTransmitterData data)
        {
        }

        [ClientRpc]
        protected virtual void RpcSyncLighthouseTraderZoneData(LighthouseTraderZoneDataPacket data)
        {
        }

        private void method_1()
        {
        }

        protected static void InvokeCmdCmdSpawn(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("Command CmdSpawn called on client.");
                return;
            }
            ((AbstractGameSession)obj).CmdSpawn();
        }

        protected static void InvokeCmdCmdRespawn(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("Command CmdRespawn called on client.");
                return;
            }
            ((AbstractGameSession)obj).CmdRespawn();
        }

        protected static void InvokeCmdCmdStartGame(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("Command CmdStartGame called on client.");
                return;
            }
            ((AbstractGameSession)obj).CmdStartGame();
        }

        protected static void InvokeCmdCmdStartGameAfterTeleport(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("Command CmdStartGameAfterTeleport called on client.");
                return;
            }
            ((AbstractGameSession)obj).CmdStartGameAfterTeleport();
        }

        protected static void InvokeCmdCmdRestartGameInitiate(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("Command CmdRestartGameInitiate called on client.");
                return;
            }
            ((AbstractGameSession)obj).CmdRestartGameInitiate();
        }

        protected static void InvokeCmdCmdRestartGame(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("Command CmdRestartGame called on client.");
                return;
            }
            ((AbstractGameSession)obj).CmdRestartGame();
        }

        protected static void InvokeCmdCmdGameStarted(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("Command CmdGameStarted called on client.");
                return;
            }
            ((AbstractGameSession)obj).CmdGameStarted();
        }

        protected static void InvokeCmdCmdStopGame(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("Command CmdStopGame called on client.");
                return;
            }
            ((AbstractGameSession)obj).CmdStopGame();
        }

        protected static void InvokeCmdCmdSyncGameTime(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("Command CmdSyncGameTime called on client.");
                return;
            }
            ((AbstractGameSession)obj).CmdSyncGameTime();
        }

        protected static void InvokeCmdCmdDevelopRequestBot(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("Command CmdDevelopRequestBot called on client.");
                return;
            }
            ((AbstractGameSession)obj).CmdDevelopRequestBot(reader.ReadString());
        }

        protected static void InvokeCmdCmdDevelopRequestBotZones(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("Command CmdDevelopRequestBotZones called on client.");
                return;
            }
            ((AbstractGameSession)obj).CmdDevelopRequestBotZones();
        }

        protected static void InvokeCmdCmdDevelopRequestBotGroups(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("Command CmdDevelopRequestBotGroups called on client.");
                return;
            }
            ((AbstractGameSession)obj).CmdDevelopRequestBotGroups();
        }

        protected static void InvokeCmdCmdDevelopmentSpawnBotRequest(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("Command CmdDevelopmentSpawnBotRequest called on client.");
                return;
            }
            ((AbstractGameSession)obj).CmdDevelopmentSpawnBotRequest((EPlayerSide)reader.ReadInt32());
        }

        protected static void InvokeCmdCmdDevelopmentSpawnBotOnServer(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("Command CmdDevelopmentSpawnBotOnServer called on client.");
                return;
            }
            ((AbstractGameSession)obj).CmdDevelopmentSpawnBotOnServer((EPlayerSide)reader.ReadInt32());
        }

        protected static void InvokeCmdCmdDevelopmentSpawnBotOnClient(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("Command CmdDevelopmentSpawnBotOnClient called on client.");
                return;
            }
            ((AbstractGameSession)obj).CmdDevelopmentSpawnBotOnClient((EPlayerSide)reader.ReadInt32(), (int)reader.ReadPackedUInt32());
        }

        protected static void InvokeCmdCmdDisconnectAcceptedOnClient(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("Command CmdDisconnectAcceptedOnClient called on client.");
                return;
            }
            ((AbstractGameSession)obj).CmdDisconnectAcceptedOnClient();
        }

        protected static void InvokeCmdCmdWorldSpawnConfirm(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("Command CmdWorldSpawnConfirm called on client.");
                return;
            }
            ((AbstractGameSession)obj).CmdWorldSpawnConfirm();
        }

        protected static void InvokeCmdCmdSpawnConfirm(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("Command CmdSpawnConfirm called on client.");
                return;
            }
            ((AbstractGameSession)obj).CmdSpawnConfirm((int)reader.ReadPackedUInt32());
        }

        protected static void InvokeCmdCmdReportVoipAbuse(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("Command CmdReportVoipAbuse called on client.");
                return;
            }
            ((AbstractGameSession)obj).CmdReportVoipAbuse();
        }

        protected static void InvokeCmdCmdPlayerEffectsPause(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("Command CmdPlayerEffectsPause called on client.");
                return;
            }
            ((AbstractGameSession)obj).CmdPlayerEffectsPause(reader.ReadString(), reader.ReadBoolean());
        }

        protected static void InvokeCmdCmdOnPlayerKeeperStatisticsChanged(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("Command CmdOnPlayerKeeperStatisticsChanged called on client.");
                return;
            }
            ((AbstractGameSession)obj).CmdOnPlayerKeeperStatisticsChanged(reader.ReadString(), (CounterTag)reader.ReadInt32(), (int)reader.ReadPackedUInt32());
        }

        protected static void InvokeCmdCmdGetRadiotransmitterData(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("Command CmdGetRadiotransmitterData called on client.");
                return;
            }
            ((AbstractGameSession)obj).CmdGetRadiotransmitterData(reader.ReadString());
        }

        public void CallCmdSpawn()
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("Command function CmdSpawn called on server.");
                return;
            }
            if (base.isServer)
            {
                this.CmdSpawn();
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(5);
            networkWriter.WritePackedUInt32(1723132743);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            base.SendCommandInternal(networkWriter, 0, "CmdSpawn");
        }

        public void CallCmdRespawn()
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("Command function CmdRespawn called on server.");
                return;
            }
            if (base.isServer)
            {
                this.CmdRespawn();
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(5);
            networkWriter.WritePackedUInt32(740792038);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            base.SendCommandInternal(networkWriter, 0, "CmdRespawn");
        }

        public void CallCmdStartGame()
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("Command function CmdStartGame called on server.");
                return;
            }
            if (base.isServer)
            {
                this.CmdStartGame();
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(5);
            networkWriter.WritePackedUInt32(1220356686);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            base.SendCommandInternal(networkWriter, 0, "CmdStartGame");
        }

        public void CallCmdStartGameAfterTeleport()
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("Command function CmdStartGameAfterTeleport called on server.");
                return;
            }
            if (base.isServer)
            {
                this.CmdStartGameAfterTeleport();
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(5);
            networkWriter.WritePackedUInt32(1792897173);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            base.SendCommandInternal(networkWriter, 0, "CmdStartGameAfterTeleport");
        }

        public void CallCmdRestartGameInitiate()
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("Command function CmdRestartGameInitiate called on server.");
                return;
            }
            if (base.isServer)
            {
                this.CmdRestartGameInitiate();
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(5);
            networkWriter.WritePackedUInt32(273195288);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            base.SendCommandInternal(networkWriter, 0, "CmdRestartGameInitiate");
        }

        public void CallCmdRestartGame()
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("Command function CmdRestartGame called on server.");
                return;
            }
            if (base.isServer)
            {
                this.CmdRestartGame();
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(5);
            networkWriter.WritePackedUInt32(1501005473);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            base.SendCommandInternal(networkWriter, 0, "CmdRestartGame");
        }

        public void CallCmdGameStarted()
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("Command function CmdGameStarted called on server.");
                return;
            }
            if (base.isServer)
            {
                this.CmdGameStarted();
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(5);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_6);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            base.SendCommandInternal(networkWriter, 0, "CmdGameStarted");
        }

        public void CallCmdStopGame()
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("Command function CmdStopGame called on server.");
                return;
            }
            if (base.isServer)
            {
                this.CmdStopGame();
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(5);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_7);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            base.SendCommandInternal(networkWriter, 0, "CmdStopGame");
        }

        public void CallCmdSyncGameTime()
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("Command function CmdSyncGameTime called on server.");
                return;
            }
            if (base.isServer)
            {
                this.CmdSyncGameTime();
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(5);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_8);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            base.SendCommandInternal(networkWriter, 0, "CmdSyncGameTime");
        }

        public void CallCmdDevelopRequestBot(string profileId)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("Command function CmdDevelopRequestBot called on server.");
                return;
            }
            if (base.isServer)
            {
                this.CmdDevelopRequestBot(profileId);
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(5);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_9);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            networkWriter.Write(profileId);
            base.SendCommandInternal(networkWriter, 0, "CmdDevelopRequestBot");
        }

        public void CallCmdDevelopRequestBotZones()
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("Command function CmdDevelopRequestBotZones called on server.");
                return;
            }
            if (base.isServer)
            {
                this.CmdDevelopRequestBotZones();
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(5);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_10);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            base.SendCommandInternal(networkWriter, 0, "CmdDevelopRequestBotZones");
        }

        public void CallCmdDevelopRequestBotGroups()
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("Command function CmdDevelopRequestBotGroups called on server.");
                return;
            }
            if (base.isServer)
            {
                this.CmdDevelopRequestBotGroups();
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(5);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_11);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            base.SendCommandInternal(networkWriter, 0, "CmdDevelopRequestBotGroups");
        }

        public void CallCmdDevelopmentSpawnBotRequest(EPlayerSide side)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("Command function CmdDevelopmentSpawnBotRequest called on server.");
                return;
            }
            if (base.isServer)
            {
                this.CmdDevelopmentSpawnBotRequest(side);
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(5);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_12);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            networkWriter.Write((int)side);
            base.SendCommandInternal(networkWriter, 0, "CmdDevelopmentSpawnBotRequest");
        }

        public void CallCmdDevelopmentSpawnBotOnServer(EPlayerSide side)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("Command function CmdDevelopmentSpawnBotOnServer called on server.");
                return;
            }
            if (base.isServer)
            {
                this.CmdDevelopmentSpawnBotOnServer(side);
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(5);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_13);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            networkWriter.Write((int)side);
            base.SendCommandInternal(networkWriter, 0, "CmdDevelopmentSpawnBotOnServer");
        }

        public void CallCmdDevelopmentSpawnBotOnClient(EPlayerSide side, int instanceId)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("Command function CmdDevelopmentSpawnBotOnClient called on server.");
                return;
            }
            if (base.isServer)
            {
                this.CmdDevelopmentSpawnBotOnClient(side, instanceId);
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(5);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_14);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            networkWriter.Write((int)side);
            networkWriter.WritePackedUInt32((uint)instanceId);
            base.SendCommandInternal(networkWriter, 0, "CmdDevelopmentSpawnBotOnClient");
        }

        public void CallCmdDisconnectAcceptedOnClient()
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("Command function CmdDisconnectAcceptedOnClient called on server.");
                return;
            }
            if (base.isServer)
            {
                this.CmdDisconnectAcceptedOnClient();
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(5);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_15);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            base.SendCommandInternal(networkWriter, 0, "CmdDisconnectAcceptedOnClient");
        }

        public void CallCmdWorldSpawnConfirm()
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("Command function CmdWorldSpawnConfirm called on server.");
                return;
            }
            if (base.isServer)
            {
                this.CmdWorldSpawnConfirm();
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(5);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_16);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            base.SendCommandInternal(networkWriter, 0, "CmdWorldSpawnConfirm");
        }

        public void CallCmdSpawnConfirm(int playerId)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("Command function CmdSpawnConfirm called on server.");
                return;
            }
            if (base.isServer)
            {
                this.CmdSpawnConfirm(playerId);
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(5);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_17);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            networkWriter.WritePackedUInt32((uint)playerId);
            base.SendCommandInternal(networkWriter, 0, "CmdSpawnConfirm");
        }

        public void CallCmdReportVoipAbuse()
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("Command function CmdReportVoipAbuse called on server.");
                return;
            }
            if (base.isServer)
            {
                this.CmdReportVoipAbuse();
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(5);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_18);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            base.SendCommandInternal(networkWriter, 0, "CmdReportVoipAbuse");
        }

        public void CallCmdPlayerEffectsPause(string playerProfileID, bool isPaused)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("Command function CmdPlayerEffectsPause called on server.");
                return;
            }
            if (base.isServer)
            {
                this.CmdPlayerEffectsPause(playerProfileID, isPaused);
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(5);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_19);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            networkWriter.Write(playerProfileID);
            networkWriter.Write(isPaused);
            base.SendCommandInternal(networkWriter, 0, "CmdPlayerEffectsPause");
        }

        public void CallCmdOnPlayerKeeperStatisticsChanged(string playerProfileID, CounterTag statisticsType, int valueToSet)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("Command function CmdOnPlayerKeeperStatisticsChanged called on server.");
                return;
            }
            if (base.isServer)
            {
                this.CmdOnPlayerKeeperStatisticsChanged(playerProfileID, statisticsType, valueToSet);
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(5);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_20);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            networkWriter.Write(playerProfileID);
            networkWriter.Write((int)statisticsType);
            networkWriter.WritePackedUInt32((uint)valueToSet);
            base.SendCommandInternal(networkWriter, 0, "CmdOnPlayerKeeperStatisticsChanged");
        }

        public void CallCmdGetRadiotransmitterData(string playerProfileID)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("Command function CmdGetRadiotransmitterData called on server.");
                return;
            }
            if (base.isServer)
            {
                this.CmdGetRadiotransmitterData(playerProfileID);
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(5);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_21);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            networkWriter.Write(playerProfileID);
            base.SendCommandInternal(networkWriter, 0, "CmdGetRadiotransmitterData");
        }

        protected static void InvokeRpcRpcGameSpawned(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("RPC RpcGameSpawned called on server.");
                return;
            }
            ((AbstractGameSession)obj).RpcGameSpawned();
        }

        protected static void InvokeRpcRpcGameMatching(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("RPC RpcGameMatching called on server.");
                return;
            }
            ((AbstractGameSession)obj).RpcGameMatching((ushort)reader.ReadPackedUInt32(), (ushort)reader.ReadPackedUInt32(), (int)reader.ReadPackedUInt32());
        }

        protected static void InvokeRpcRpcGameStarting(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("RPC RpcGameStarting called on server.");
                return;
            }
            ((AbstractGameSession)obj).RpcGameStarting((int)reader.ReadPackedUInt32());
        }

        protected static void InvokeRpcRpcGameStartingWithTeleport(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("RPC RpcGameStartingWithTeleport called on server.");
                return;
            }
            ((AbstractGameSession)obj).RpcGameStartingWithTeleport(reader.ReadVector3(), (int)reader.ReadPackedUInt32(), reader.ReadString());
        }

        protected static void InvokeRpcRpcGameStarted(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("RPC RpcGameStarted called on server.");
                return;
            }
            ((AbstractGameSession)obj).RpcGameStarted(reader.ReadSingle(), (int)reader.ReadPackedUInt32());
        }

        protected static void InvokeRpcRpcGameRestarting(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("RPC RpcGameRestarting called on server.");
                return;
            }
            ((AbstractGameSession)obj).RpcGameRestarting();
        }

        protected static void InvokeRpcRpcGameRestarted(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("RPC RpcGameRestarted called on server.");
                return;
            }
            ((AbstractGameSession)obj).RpcGameRestarted();
        }

        protected static void InvokeRpcRpcGameStopping(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("RPC RpcGameStopping called on server.");
                return;
            }
            ((AbstractGameSession)obj).RpcGameStopping();
        }

        protected static void InvokeRpcRpcGameStopped(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("RPC RpcGameStopped called on server.");
                return;
            }
            ((AbstractGameSession)obj).RpcGameStopped((ExitStatus)reader.ReadInt32(), (int)reader.ReadPackedUInt32());
        }

        protected static void InvokeRpcRpcSyncGameTime(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("RPC RpcSyncGameTime called on server.");
                return;
            }
            ((AbstractGameSession)obj).RpcSyncGameTime((long)reader.ReadPackedUInt64());
        }

        protected static void InvokeRpcRpcDevelopSendBotData(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("RPC RpcDevelopSendBotData called on server.");
                return;
            }
            ((AbstractGameSession)obj).RpcDevelopSendBotData(reader.ReadBytesAndSize());
        }

        protected static void InvokeRpcRpcDevelopSendBotDataZone(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("RPC RpcDevelopSendBotDataZone called on server.");
                return;
            }
            ((AbstractGameSession)obj).RpcDevelopSendBotDataZone(reader.ReadBytesAndSize());
        }

        protected static void InvokeRpcRpcDevelopSendBotDataGroups(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("RPC RpcDevelopSendBotDataGroups called on server.");
                return;
            }
            ((AbstractGameSession)obj).RpcDevelopSendBotDataGroups(reader.ReadBytesAndSize());
        }

        protected static void InvokeRpcRpcDevelopmentSpawnBotResponse(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("RPC RpcDevelopmentSpawnBotResponse called on server.");
                return;
            }
            ((AbstractGameSession)obj).RpcDevelopmentSpawnBotResponse((EPlayerSide)reader.ReadInt32(), (int)reader.ReadPackedUInt32());
        }

        protected static void InvokeRpcRpcSoftStopNotification(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("RPC RpcSoftStopNotification called on server.");
                return;
            }
            ((AbstractGameSession)obj).RpcSoftStopNotification((int)reader.ReadPackedUInt32());
        }

        protected static void InvokeRpcRpcStartDisconnectionProcedure(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("RPC RpcStartDisconnectionProcedure called on server.");
                return;
            }
            ((AbstractGameSession)obj).RpcStartDisconnectionProcedure((int)reader.ReadPackedUInt32(), reader.ReadString(), reader.ReadString());
        }

        protected static void InvokeRpcRpcVoipAbuseNotification(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("RPC RpcVoipAbuseNotification called on server.");
                return;
            }
            ((AbstractGameSession)obj).RpcVoipAbuseNotification(reader.ReadString());
        }

        protected static void InvokeRpcRpcAirdropContainerData(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("RPC RpcAirdropContainerData called on server.");
                return;
            }
            ((AbstractGameSession)obj).RpcAirdropContainerData(reader.ReadBytesAndSize());
        }

        protected static void InvokeRpcRpcMineDirectionExplosion(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("RPC RpcMineDirectionExplosion called on server.");
                return;
            }
            ((AbstractGameSession)obj).RpcMineDirectionExplosion(reader.ReadBytesAndSize());
        }

        protected static void InvokeRpcRpcSuccessAirdropFlareEvent(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("RPC RpcSuccessAirdropFlareEvent called on server.");
                return;
            }
            ((AbstractGameSession)obj).RpcSuccessAirdropFlareEvent(reader.ReadBoolean());
        }

        protected static void InvokeRpcRpcBufferZoneData(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("RPC RpcBufferZoneData called on server.");
                return;
            }
            ((AbstractGameSession)obj).RpcBufferZoneData(reader.ReadBytesAndSize());
        }

        protected static void InvokeRpcRpcSendClientRadioTransmitterData(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("RPC RpcSendClientRadioTransmitterData called on server.");
                return;
            }
            //((AbstractGameSession)obj).RpcSendClientRadioTransmitterData(GClass3133._ReadRadioTransmitterData_None(reader));
        }

        protected static void InvokeRpcRpcSendObserverRadioTransmitterData(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("RPC RpcSendObserverRadioTransmitterData called on server.");
                return;
            }
            //((AbstractGameSession)obj).RpcSendObserverRadioTransmitterData(GClass3133._ReadRadioTransmitterData_None(reader));
        }

        protected static void InvokeRpcRpcSyncLighthouseTraderZoneData(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!NetworkClient.active)
            {
                Debug.LogError("RPC RpcSyncLighthouseTraderZoneData called on server.");
                return;
            }
            //((AbstractGameSession)obj).RpcSyncLighthouseTraderZoneData(GClass3133._ReadLighthouseTraderZoneData_None(reader));
        }

        public void CallRpcGameSpawned()
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("RPC Function RpcGameSpawned called on client.");
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(2);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_22);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            this.SendRPCInternal(networkWriter, 0, "RpcGameSpawned");
        }

        public void CallRpcGameMatching(ushort activitiesCounter, ushort minCounter, int seconds)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("RPC Function RpcGameMatching called on client.");
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(2);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_23);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            networkWriter.WritePackedUInt32((uint)activitiesCounter);
            networkWriter.WritePackedUInt32((uint)minCounter);
            networkWriter.WritePackedUInt32((uint)seconds);
            this.SendRPCInternal(networkWriter, 0, "RpcGameMatching");
        }

        public void CallRpcGameStarting(int seconds)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("RPC Function RpcGameStarting called on client.");
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(2);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_24);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            networkWriter.WritePackedUInt32((uint)seconds);
            this.SendRPCInternal(networkWriter, 0, "RpcGameStarting");
        }

        public void CallRpcGameStartingWithTeleport(Vector3 position, int exfiltrationId, string entryPoint)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("RPC Function RpcGameStartingWithTeleport called on client.");
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(2);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_25);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            networkWriter.Write(position);
            networkWriter.WritePackedUInt32((uint)exfiltrationId);
            networkWriter.Write(entryPoint);
            this.SendRPCInternal(networkWriter, 0, "RpcGameStartingWithTeleport");
        }

        public void CallRpcGameStarted(float pastTime, int sessionSeconds)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("RPC Function RpcGameStarted called on client.");
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(2);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_26);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            networkWriter.Write(pastTime);
            networkWriter.WritePackedUInt32((uint)sessionSeconds);
            this.SendRPCInternal(networkWriter, 0, "RpcGameStarted");
        }

        public void CallRpcGameRestarting()
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("RPC Function RpcGameRestarting called on client.");
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(2);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_27);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            this.SendRPCInternal(networkWriter, 0, "RpcGameRestarting");
        }

        public void CallRpcGameRestarted()
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("RPC Function RpcGameRestarted called on client.");
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(2);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_28);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            this.SendRPCInternal(networkWriter, 0, "RpcGameRestarted");
        }

        public void CallRpcGameStopping()
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("RPC Function RpcGameStopping called on client.");
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(2);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_29);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            this.SendRPCInternal(networkWriter, 0, "RpcGameStopping");
        }

        public void CallRpcGameStopped(ExitStatus exitStatus, int playTime)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("RPC Function RpcGameStopped called on client.");
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(2);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_30);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            networkWriter.Write((int)exitStatus);
            networkWriter.WritePackedUInt32((uint)playTime);
            this.SendRPCInternal(networkWriter, 0, "RpcGameStopped");
        }

        public void CallRpcSyncGameTime(long time)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("RPC Function RpcSyncGameTime called on client.");
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(2);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_31);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            networkWriter.WritePackedUInt64((ulong)time);
            this.SendRPCInternal(networkWriter, 0, "RpcSyncGameTime");
        }

        public void CallRpcDevelopSendBotData(byte[] data)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("RPC Function RpcDevelopSendBotData called on client.");
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(2);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_32);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            networkWriter.WriteBytesFull(data);
            this.SendRPCInternal(networkWriter, 0, "RpcDevelopSendBotData");
        }

        public void CallRpcDevelopSendBotDataZone(byte[] data)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("RPC Function RpcDevelopSendBotDataZone called on client.");
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(2);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_33);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            networkWriter.WriteBytesFull(data);
            this.SendRPCInternal(networkWriter, 0, "RpcDevelopSendBotDataZone");
        }

        public void CallRpcDevelopSendBotDataGroups(byte[] data)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("RPC Function RpcDevelopSendBotDataGroups called on client.");
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(2);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_34);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            networkWriter.WriteBytesFull(data);
            this.SendRPCInternal(networkWriter, 0, "RpcDevelopSendBotDataGroups");
        }

        public void CallRpcDevelopmentSpawnBotResponse(EPlayerSide side, int instanceId)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("RPC Function RpcDevelopmentSpawnBotResponse called on client.");
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(2);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_35);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            networkWriter.Write((int)side);
            networkWriter.WritePackedUInt32((uint)instanceId);
            this.SendRPCInternal(networkWriter, 0, "RpcDevelopmentSpawnBotResponse");
        }

        public void CallRpcSoftStopNotification(int sessionSeconds)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("RPC Function RpcSoftStopNotification called on client.");
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(2);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_36);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            networkWriter.WritePackedUInt32((uint)sessionSeconds);
            this.SendRPCInternal(networkWriter, 0, "RpcSoftStopNotification");
        }

        public void CallRpcStartDisconnectionProcedure(int disconnectionCode, string additionalInfo, string technicalMessage)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("RPC Function RpcStartDisconnectionProcedure called on client.");
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(2);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_37);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            networkWriter.WritePackedUInt32((uint)disconnectionCode);
            networkWriter.Write(additionalInfo);
            networkWriter.Write(technicalMessage);
            this.SendRPCInternal(networkWriter, 0, "RpcStartDisconnectionProcedure");
        }

        public void CallRpcVoipAbuseNotification(string reporter)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("RPC Function RpcVoipAbuseNotification called on client.");
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(2);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_38);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            networkWriter.Write(reporter);
            this.SendRPCInternal(networkWriter, 0, "RpcVoipAbuseNotification");
        }

        public void CallRpcAirdropContainerData(byte[] data)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("RPC Function RpcAirdropContainerData called on client.");
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(2);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_39);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            networkWriter.WriteBytesFull(data);
            this.SendRPCInternal(networkWriter, 0, "RpcAirdropContainerData");
        }

        public void CallRpcMineDirectionExplosion(byte[] data)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("RPC Function RpcMineDirectionExplosion called on client.");
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(2);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_40);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            networkWriter.WriteBytesFull(data);
            this.SendRPCInternal(networkWriter, 0, "RpcMineDirectionExplosion");
        }

        public void CallRpcSuccessAirdropFlareEvent(bool canSendAirdrop)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("RPC Function RpcSuccessAirdropFlareEvent called on client.");
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(2);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_41);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            networkWriter.Write(canSendAirdrop);
            this.SendRPCInternal(networkWriter, 0, "RpcSuccessAirdropFlareEvent");
        }

        public void CallRpcBufferZoneData(byte[] data)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("RPC Function RpcBufferZoneData called on client.");
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(2);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_42);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            networkWriter.WriteBytesFull(data);
            this.SendRPCInternal(networkWriter, 0, "RpcBufferZoneData");
        }

        public void CallRpcSendClientRadioTransmitterData(RadioTransmitterData data)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("RPC Function RpcSendClientRadioTransmitterData called on client.");
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(2);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_43);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            //GClass3133._WriteRadioTransmitterData_None(networkWriter, data);
            this.SendRPCInternal(networkWriter, 0, "RpcSendClientRadioTransmitterData");
        }

        public void CallRpcSendObserverRadioTransmitterData(RadioTransmitterData data)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("RPC Function RpcSendObserverRadioTransmitterData called on client.");
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(2);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_44);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            //GClass3133._WriteRadioTransmitterData_None(networkWriter, data);
            this.SendRPCInternal(networkWriter, 0, "RpcSendObserverRadioTransmitterData");
        }

        public void CallRpcSyncLighthouseTraderZoneData(LighthouseTraderZoneDataPacket data)
        {
            if (!NetworkServer.active)
            {
                Debug.LogError("RPC Function RpcSyncLighthouseTraderZoneData called on client.");
                return;
            }
            NetworkWriter networkWriter = new NetworkWriter();
            networkWriter.Write(0);
            networkWriter.Write(2);
            networkWriter.WritePackedUInt32((uint)AbstractGameSession.int_45);
            networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
            //GClass3133._WriteLighthouseTraderZoneData_None(networkWriter, data);
            this.SendRPCInternal(networkWriter, 0, "RpcSyncLighthouseTraderZoneData");
        }

        private static int int_0 = -1723132743;

        // Token: 0x04005EA4 RID: 24228
        private static int int_1;

        // Token: 0x04005EA5 RID: 24229
        private static int int_2;

        // Token: 0x04005EA6 RID: 24230
        private static int int_3;

        // Token: 0x04005EA7 RID: 24231
        private static int int_4;

        // Token: 0x04005EA8 RID: 24232
        private static int int_5;

        // Token: 0x04005EA9 RID: 24233
        private static int int_6;

        // Token: 0x04005EAA RID: 24234
        private static int int_7;

        // Token: 0x04005EAB RID: 24235
        private static int int_8;

        // Token: 0x04005EAC RID: 24236
        private static int int_9;

        // Token: 0x04005EAD RID: 24237
        private static int int_10;

        // Token: 0x04005EAE RID: 24238
        private static int int_11;

        // Token: 0x04005EAF RID: 24239
        private static int int_12;

        // Token: 0x04005EB0 RID: 24240
        private static int int_13;

        // Token: 0x04005EB1 RID: 24241
        private static int int_14;

        // Token: 0x04005EB2 RID: 24242
        private static int int_15;

        // Token: 0x04005EB3 RID: 24243
        private static int int_16;

        // Token: 0x04005EB4 RID: 24244
        private static int int_17;

        // Token: 0x04005EB5 RID: 24245
        private static int int_18;

        // Token: 0x04005EB6 RID: 24246
        private static int int_19;

        // Token: 0x04005EB7 RID: 24247
        private static int int_20;

        // Token: 0x04005EB8 RID: 24248
        private static int int_21;

        // Token: 0x04005EB9 RID: 24249
        private static int int_22;

        // Token: 0x04005EBA RID: 24250
        private static int int_23;

        // Token: 0x04005EBB RID: 24251
        private static int int_24;

        // Token: 0x04005EBC RID: 24252
        private static int int_25;

        // Token: 0x04005EBD RID: 24253
        private static int int_26;

        // Token: 0x04005EBE RID: 24254
        private static int int_27;

        // Token: 0x04005EBF RID: 24255
        private static int int_28;

        // Token: 0x04005EC0 RID: 24256
        private static int int_29;

        // Token: 0x04005EC1 RID: 24257
        private static int int_30;

        // Token: 0x04005EC2 RID: 24258
        private static int int_31;

        // Token: 0x04005EC3 RID: 24259
        private static int int_32;

        // Token: 0x04005EC4 RID: 24260
        private static int int_33;

        // Token: 0x04005EC5 RID: 24261
        private static int int_34;

        // Token: 0x04005EC6 RID: 24262
        private static int int_35;

        // Token: 0x04005EC7 RID: 24263
        private static int int_36;

        // Token: 0x04005EC8 RID: 24264
        private static int int_37;

        // Token: 0x04005EC9 RID: 24265
        private static int int_38;

        // Token: 0x04005ECA RID: 24266
        private static int int_39;

        // Token: 0x04005ECB RID: 24267
        private static int int_40;

        // Token: 0x04005ECC RID: 24268
        private static int int_41;

        // Token: 0x04005ECD RID: 24269
        private static int int_42;

        // Token: 0x04005ECE RID: 24270
        private static int int_43;

        // Token: 0x04005ECF RID: 24271
        private static int int_44;

        // Token: 0x04005ED0 RID: 24272
        private static int int_45;
    }
}