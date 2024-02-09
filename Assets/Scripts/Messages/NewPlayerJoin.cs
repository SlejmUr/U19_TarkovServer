using UnityEngine.Networking;

namespace TarkovServerU19.Messages
{
    /// <summary>
    /// Packet 188
    /// </summary>
    public class NewPlayerJoin : MessageBase
    {
        public override void Deserialize(NetworkReader reader)
        {
            this.Id = reader.ReadInt32();
            this.prefabsData = reader.ReadBytesAndSize();
            this.customizationsData = reader.ReadBytesAndSize();
            base.Deserialize(reader);
        }
        public override void Serialize(NetworkWriter writer)
        {
            writer.Write(this.Id);
            writer.WriteBytesAndSize(this.prefabsData, this.prefabsData.Length);
            writer.WriteBytesAndSize(this.customizationsData, this.customizationsData.Length);
            base.Serialize(writer);
        }
        internal int Id;

        internal byte[] prefabsData; //SimpleZlib.Compress(ResourceKey[])

        internal byte[] customizationsData; //SimpleZlib.Compress(string[])
    }
}
