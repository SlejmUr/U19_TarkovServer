using UnityEngine.Networking;
using UnityEngine;
using TarkovServerU19.BSGClasses;
using TarkovServerU19.BSGClasses.VOIP;

namespace TarkovServerU19.Messages
{
    /// <summary>
    /// Packet 147 Response
    /// </summary>
    public class AcceptConnection : MessageBase
    {
        public override void Deserialize(NetworkReader reader)
        {
            this.encryptionEnabled = reader.ReadBoolean();
            this.decryptionEnabled = reader.ReadBoolean();
            this.gameTimeClass = GameTimeClass.Deserialize(reader);
            this.CompressedResources = reader.ReadBytesAndSize();
            this.CompressedCustomizationIds = reader.ReadBytesAndSize();
            this.CompressedWeathers = reader.ReadBytesAndSize();
            this.canRestart = reader.ReadBoolean();
            this.MemberCategory = (EMemberCategory)reader.ReadInt32();
            this.fixedDeltaTime = reader.ReadSingle();
            this.CompressedInteractables = reader.ReadBytesAndSize();
            this.sessionId = reader.ReadBytesAndSize();
            Vector3 min = reader.ReadVector3();
            Vector3 max = reader.ReadVector3();
            this.bounds = new Bounds
            {
                min = min,
                max = max
            };
            this.antiCheatPort = reader.ReadUInt16();
            this.NetLogsLevel = (ENetLogsLevel)reader.ReadByte();
            this.GitVersion = GitVersion.Deserialize(reader);
            this.speedLimitsEnabled = reader.ReadBoolean();
            if (this.speedLimitsEnabled)
            {
                this.speedLimits.Deserialize(reader);
            }
            if (reader.ReadBoolean())
            {
                this.voipSettings = new VoipSettings();
                this.voipSettings.Deserialize(reader);
            }
            base.Deserialize(reader);
        }

        public override void Serialize(NetworkWriter writer)
        {
            writer.Write(this.encryptionEnabled);
            writer.Write(this.decryptionEnabled);
            this.gameTimeClass.Serialize(writer, false);
            writer.WriteBytesFull(this.CompressedResources);
            writer.WriteBytesFull(this.CompressedCustomizationIds);
            writer.WriteBytesFull(this.CompressedWeathers);
            writer.Write(this.canRestart);
            writer.Write((int)this.MemberCategory);
            writer.Write(this.fixedDeltaTime);
            writer.WriteBytesFull(this.CompressedInteractables);
            writer.WriteBytesFull(this.sessionId);
            writer.Write(this.bounds.min);
            writer.Write(this.bounds.max);
            writer.Write(this.antiCheatPort);
            writer.Write((byte)this.NetLogsLevel);
            this.GitVersion.Serialize(writer);
            writer.Write(this.speedLimitsEnabled);
            if (this.speedLimitsEnabled)
            {
                this.speedLimits.Serialize(writer);
            }
            writer.Write(this.voipSettings != null);
            VoipSettings voipSettings = this.voipSettings;
            if (voipSettings != null)
            {
                voipSettings.Serialize(writer);
            }
            base.Serialize(writer);
        }
        internal bool encryptionEnabled;
        internal bool decryptionEnabled;
        internal GameTimeClass gameTimeClass;
        internal byte[] CompressedResources; //ResourceKey[]
        internal byte[] CompressedCustomizationIds; //string[]
        internal byte[] CompressedWeathers; //WeatherClass[]
        internal bool canRestart;
        internal EMemberCategory MemberCategory;
        internal float fixedDeltaTime;
        internal byte[] CompressedInteractables; //Dictionary<string, int>
        internal byte[] sessionId;
        internal Bounds bounds;
        internal ushort antiCheatPort;
        internal ENetLogsLevel NetLogsLevel;
        internal GitVersion GitVersion;
        internal bool speedLimitsEnabled;
        internal Config speedLimits = new Config();
        internal VoipSettings voipSettings;
    }
}
