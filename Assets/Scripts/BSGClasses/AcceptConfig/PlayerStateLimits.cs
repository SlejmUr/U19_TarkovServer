using System;
using UnityEngine.Networking;

namespace TarkovServerU19.BSGClasses
{
    [Serializable]
    public struct PlayerStateLimits
    {
        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.MinSpeed);
            writer.Write(this.MaxSpeed);
        }
        public void Deserialize(NetworkReader reader)
        {
            this.MinSpeed = reader.ReadSingle();
            this.MaxSpeed = reader.ReadSingle();
        }

        public float MinSpeed;
        public float MaxSpeed;
    }
}
