using UnityEngine.Networking;

namespace TarkovServerU19.Messages
{
    /// <summary>
    /// Packet 147 Request
    /// </summary>
    public class ConnectSentPacket : MessageBase
    {
        public override void Deserialize(NetworkReader reader)
        {
            this.ProfileId = reader.ReadString();
            this.Token = reader.ReadString();
            this.ObserveOnly = reader.ReadBoolean();
            this.OpenEncryptionKey = reader.ReadBytesAndSize();
            this.OpenEncryptionKeyLength = reader.ReadInt32();
            this.LocationId = reader.ReadString();
            base.Deserialize(reader);
        }

        // Token: 0x060063D1 RID: 25553 RVA: 0x001DB90C File Offset: 0x001D9B0C
        public override void Serialize(NetworkWriter writer)
        {
            writer.Write(this.ProfileId);
            writer.Write(this.Token);
            writer.Write(this.ObserveOnly);
            writer.WriteBytesFull(this.OpenEncryptionKey);
            writer.Write(this.OpenEncryptionKeyLength);
            writer.Write(this.LocationId);
            base.Serialize(writer);
        }

        // Token: 0x04005ED1 RID: 24273
        internal string ProfileId;

        // Token: 0x04005ED2 RID: 24274
        internal string Token;

        // Token: 0x04005ED3 RID: 24275
        internal bool ObserveOnly;

        // Token: 0x04005ED4 RID: 24276
        internal byte[] OpenEncryptionKey;

        // Token: 0x04005ED5 RID: 24277
        internal int OpenEncryptionKeyLength;

        // Token: 0x04005ED6 RID: 24278
        internal string LocationId;
    }
}
