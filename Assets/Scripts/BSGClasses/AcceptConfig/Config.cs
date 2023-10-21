using System.Collections.Generic;
using TarkovServerU19.Assets.Scripts.Helpers;
using UnityEngine.Networking;

namespace TarkovServerU19.BSGClasses
{
    public class Config
    {
        public void Serialize(NetworkWriter writer)
        {
            this.DefaultPlayerStateLimits.Serialize(writer);
            this.PlayerStateLimits.Serialize(writer);
        }
        public void Deserialize(NetworkReader reader)
        {
            this.DefaultPlayerStateLimits.Deserialize(reader);
            this.PlayerStateLimits.Deserialize(reader);
        }
        public PlayerStateLimits DefaultPlayerStateLimits;
        public Dictionary<EPlayerState, PlayerStateLimits> PlayerStateLimits = EnumHelper<EPlayerState>.GetDictWith<PlayerStateLimits>();
    }
}
