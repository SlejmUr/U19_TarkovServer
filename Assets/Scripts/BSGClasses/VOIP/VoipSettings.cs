using UnityEngine.Networking;

namespace TarkovServerU19.BSGClasses.VOIP
{
    public class VoipSettings
    {
        public bool MicrophoneChecked { get; set; } = true;

        public static VoipSettings Default { get; } = new VoipSettings
        {
            VoipEnabled = false,
            VoipQualitySettings = VoipQualitySettings.Default,
            PushToTalkSettings = PushToTalkSettings.Default
        };

        public override string ToString()
        {
            return string.Format("{0}: {1},", "VoipEnabled", this.VoipEnabled) + string.Format("{0}: {1}", "VoipQualitySettings", this.VoipQualitySettings) + string.Format("{0}: {1}", "PushToTalkSettings", this.PushToTalkSettings);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.VoipEnabled);
            this.VoipQualitySettings.Serialize(writer);
            this.PushToTalkSettings.Serialize(writer);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.VoipEnabled = reader.ReadBoolean();
            this.VoipQualitySettings = VoipQualitySettings.Default;
            this.VoipQualitySettings.Deserialize(reader);
            this.PushToTalkSettings = PushToTalkSettings.Default;
            this.PushToTalkSettings.Deserialize(reader);
        }

        private bool bool_0;
        public bool VoipEnabled;
        public VoipQualitySettings VoipQualitySettings;
        public PushToTalkSettings PushToTalkSettings;
        private static readonly VoipSettings gclass1661_0;
    }
}
