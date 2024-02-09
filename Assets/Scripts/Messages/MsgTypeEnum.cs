namespace TarkovServerU19.Messages
{
    public enum MsgTypeEnum : short
    {
        ClientReadyToBegin = 43,
        ClientAddPlayerFailedMessage = 45,

        ConnectionRequest = 147,
        RejectResponse = 148,
        BEPacket = 168,
        PartialCommand = 185,
        NewPlayerJoin = 188,
        SyncToPlayers = 189,

        WorldSpwan = 151,
        WorldUnspawn = 152,
        SubWorldSpawnLoot = 191,
        SubWorldSpawnSearchLoot = 192,
        SubWorldUnspawn = 154,
        PlayerUnspawn = 156,
        ObserverUnspaw = 158,
        DeathInventorySync = 160,

        PlayerSpawn = 155,
        ObserverSpawn = 157,

        messageFromServer = 170,
        SpawnObservedPlayer = 171,
        SpawnObservedPlayers = 172,
        ChangeFramerate = 175,
        SnapshotObservedPlayers = 173,
        CommandsObservedPlayers = 174,
        SnapshotBTRVehicles = 184,

        HLAPI = 18385,

        ProgressReport = 190
    }
}
