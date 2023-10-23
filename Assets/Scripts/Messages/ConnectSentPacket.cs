using System;
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

        internal string ProfileId;
        internal string Token;
        internal bool ObserveOnly;
        internal byte[] OpenEncryptionKey;
        internal int OpenEncryptionKeyLength;
        internal string LocationId;

        public override string ToString()
        {
            return $"ProfileID: {ProfileId}, Token: {Token}, Observe: {ObserveOnly}, EncKey: {BitConverter.ToString(OpenEncryptionKey)}, EncL: {OpenEncryptionKeyLength}, LocationID: {LocationId}";
        }
    }
}
