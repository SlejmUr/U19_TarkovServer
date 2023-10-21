using UnityEngine.Networking;

namespace TarkovServerU19.BSGClasses.VOIP
{
    public class VoipQualitySettings
    {
        public static VoipQualitySettings Default { get; } = new VoipQualitySettings
        {
            FrameSize = FrameSize.Medium,
            AudioQuality = AudioQuality.Medium,
            ForwardErrorCorrection = true,
            NoiseSuppression = NoiseSuppressionLevels.High,
            SensitivityLevels = VadSensitivityLevels.MediumSensitivity
        };

        public override string ToString()
        {
            return string.Concat(new string[]
            {
            string.Format("{0}: {1},", "FrameSize", this.FrameSize),
            string.Format("{0}: {1},", "AudioQuality", this.AudioQuality),
            string.Format("{0}: {1},", "ForwardErrorCorrection", this.ForwardErrorCorrection),
            string.Format("{0}: {1},", "NoiseSuppression", this.NoiseSuppression),
            string.Format("{0}: {1}", "SensitivityLevels", this.SensitivityLevels)
            });
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write((byte)this.FrameSize);
            writer.Write((byte)this.AudioQuality);
            writer.Write(this.ForwardErrorCorrection);
            writer.Write((byte)this.NoiseSuppression);
            writer.Write((byte)this.SensitivityLevels);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.FrameSize = (FrameSize)reader.ReadByte();
            this.AudioQuality = (AudioQuality)reader.ReadByte();
            this.ForwardErrorCorrection = reader.ReadBoolean();
            this.NoiseSuppression = (NoiseSuppressionLevels)reader.ReadByte();
            this.SensitivityLevels = (VadSensitivityLevels)reader.ReadByte();
        }

        public FrameSize FrameSize;
        public AudioQuality AudioQuality;
        public bool ForwardErrorCorrection;
        public NoiseSuppressionLevels NoiseSuppression;
        public VadSensitivityLevels SensitivityLevels;
    }
}
