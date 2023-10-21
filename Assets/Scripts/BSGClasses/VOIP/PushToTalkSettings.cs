using UnityEngine.Networking;

namespace TarkovServerU19.BSGClasses.VOIP
{
    public class PushToTalkSettings
    {
        public static PushToTalkSettings Default { get; } = new PushToTalkSettings
        {
            SpeakingSecondsLimit = 7f,
            SpeakingSecondsInterval = 10f,
            ActivationsLimit = 2,
            ActivationsInterval = 2f,
            BlockingTime = 10f,
            AlertDistanceMeters = 5,
            HearingDistance = 50,
            AbuseTraceSeconds = 2f
        };

        public override string ToString()
        {
            return string.Concat(new string[]
            {
            string.Format("{0}: {1},", "SpeakingSecondsLimit", this.SpeakingSecondsLimit),
            string.Format("{0}: {1},", "SpeakingSecondsInterval", this.SpeakingSecondsInterval),
            string.Format("{0}: {1},", "ActivationsInterval", this.ActivationsInterval),
            string.Format("{0}: {1},", "ActivationsLimit", this.ActivationsLimit),
            string.Format("{0}: {1},", "BlockingTime", this.BlockingTime),
            string.Format("{0}: {1},", "AlertDistanceMeters", this.AlertDistanceMeters),
            string.Format("{0}: {1}", "HearingDistance", this.HearingDistance),
            string.Format("{0}: {1}", "AbuseTraceSeconds", this.AbuseTraceSeconds)
            });
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.SpeakingSecondsLimit);
            writer.Write(this.SpeakingSecondsInterval);
            writer.Write(this.ActivationsLimit);
            writer.Write(this.ActivationsInterval);
            writer.Write(this.BlockingTime);
            writer.Write(this.AlertDistanceMeters);
            writer.Write(this.HearingDistance);
            writer.Write(this.AbuseTraceSeconds);
        }

        public void Deserialize(NetworkReader reader)
        {
            this.SpeakingSecondsLimit = reader.ReadSingle();
            this.SpeakingSecondsInterval = reader.ReadSingle();
            this.ActivationsLimit = reader.ReadByte();
            this.ActivationsInterval = reader.ReadSingle();
            this.BlockingTime = reader.ReadSingle();
            this.AlertDistanceMeters = reader.ReadByte();
            this.HearingDistance = reader.ReadByte();
            this.AbuseTraceSeconds = (float)reader.ReadByte();
        }

        public float SpeakingSecondsLimit;
        public float SpeakingSecondsInterval;
        public byte ActivationsLimit;
        public float ActivationsInterval;
        public float BlockingTime;
        public byte AlertDistanceMeters;
        public byte HearingDistance;
        public float AbuseTraceSeconds;
    }
}
