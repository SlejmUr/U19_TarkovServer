using TarkovServerU19.BSGEnums;

namespace TarkovServerU19.BSGClasses
{
    public struct LighthouseTraderZoneDataPacket
    {

        public AllowedPlayers[] AllowedPlayers;

        public AllowedPlayers[] UnallowedPlayers;
    }
    public struct AllowedPlayers
    {

        public string Nickname;

        public RadioTransmitterStatus Status;
    }
}