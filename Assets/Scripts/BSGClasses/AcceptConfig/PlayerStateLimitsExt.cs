using System.Collections.Generic;
using UnityEngine.Networking;

namespace TarkovServerU19.BSGClasses
{
    public static class PlayerStateLimitsExt
    {
        public static void Serialize(this Dictionary<EPlayerState, PlayerStateLimits> playerStateLimits, NetworkWriter writer)
        {
            writer.Write(playerStateLimits.Count);
            foreach (KeyValuePair<EPlayerState, PlayerStateLimits> keyValuePair in playerStateLimits)
            {
                writer.Write((byte)keyValuePair.Key);
                keyValuePair.Value.Serialize(writer);
            }
        }

        public static void Deserialize(this Dictionary<EPlayerState, PlayerStateLimits> playerStateLimits, NetworkReader reader)
        {
            playerStateLimits.Clear();
            int num = reader.ReadInt32();
            for (int i = 0; i < num; i++)
            {
                EPlayerState key = (EPlayerState)reader.ReadByte();
                PlayerStateLimits value = default(PlayerStateLimits);
                value.Deserialize(reader);
                playerStateLimits[key] = value;
            }
        }
    }
}
