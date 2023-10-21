using UnityEngine.Networking;

namespace TarkovServerU19.Messages
{
    /// <summary>
    /// Packet 148
    /// </summary>
    public class RejectConnection : MessageBase
    {
        public override void Deserialize(NetworkReader reader)
        {
            this.RealErrorCode = reader.ReadInt32();
            base.Deserialize(reader);
        }

        public override void Serialize(NetworkWriter writer)
        {
            writer.Write(this.RealErrorCode);
            base.Serialize(writer);
        }

        internal const int int_0 = 100500; //Internal Error: client and server location mismatch
        internal const int int_1 = 100501; //Game aborted. Invalid user profile
        internal const int int_2 = 100502; //Game aborted. Invalid session
        internal const int int_3 = 100503; //Internal Error: client messaging queue fail
        internal int RealErrorCode; //Game aborted. Internal error
    }
}
