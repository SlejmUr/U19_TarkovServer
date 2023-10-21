using UnityEngine.Networking;

namespace TarkovServerU19.Messages
{
    /// <summary>
    /// Packet 190
    /// </summary>
    public class SyncFromServer : MessageBase
    {
        public override void Deserialize(NetworkReader reader)
        {
            this.ProfileId = reader.ReadString();
            this.Id = reader.ReadInt32();
            this.ProgressValue = reader.ReadSingle();
            base.Deserialize(reader);
        }

        public override void Serialize(NetworkWriter writer)
        {
            writer.Write(this.ProfileId);
            writer.Write(this.Id);
            writer.Write(this.ProgressValue);
            base.Serialize(writer);
        }

        internal string ProfileId;

        internal int Id;

        internal float ProgressValue;
    }
}
