using UnityEngine.Networking;

namespace TarkovServerU19.Messages
{
    /// <summary>
    /// Packet 189
    /// </summary>
    public class SyncToClient : MessageBase
    {		
		public override void Deserialize(NetworkReader reader)
        {
            this.float_0 = reader.ReadSingle();
            base.Deserialize(reader);
        }

        public override void Serialize(NetworkWriter writer)
        {
            writer.Write(this.float_0);
            base.Serialize(writer);
        }

        internal float float_0;
    }
}
