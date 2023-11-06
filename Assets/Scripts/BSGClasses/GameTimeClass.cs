using System;
using UnityEngine;
using UnityEngine.Networking;

namespace TarkovServerU19.BSGClasses
{
    internal class GameTimeClass
    {

        internal bool IsDebug { get; private set; }
        internal DateTime RealTime { get; private set; }
        internal DateTime GameTime { get; private set; }
        public float TimeFactor { get; private set; }

        private float _realtimeSinceStartup;

        public float TimeFactorMod = 1f;

        internal GameTimeClass(float startup, DateTime realDateTime, DateTime gameDateTime, float timeFactor, bool debug = false)
        {
            this._realtimeSinceStartup = startup;
            this.RealTime = realDateTime;
            this.GameTime = gameDateTime;
            this.TimeFactor = timeFactor;
            this.IsDebug = debug;
            Debug.Log(string.Concat(new object[]
            {
                "RealDateTime:",
                this.RealTime,
                "  GameDateTime:",
                this.GameTime,
                "  factor:",
                this.TimeFactor,
                " debug:",
                debug.ToString()
            }));
        }

        internal GameTimeClass(DateTime realDateTime, DateTime gameDateTime, float timeFactor, bool debug = false)
        {
            this._realtimeSinceStartup = Time.realtimeSinceStartup;
            this.RealTime = realDateTime;
            this.GameTime = gameDateTime;
            this.TimeFactor = timeFactor;
            this.IsDebug = debug;
            Debug.Log(string.Concat(new object[]
            {
                "RealDateTime:",
                this.RealTime,
                "  GameDateTime:",
                this.GameTime,
                "  factor:",
                this.TimeFactor,
                " debug:",
                debug.ToString()
            }));
        }

        public static GameTimeClass Deserialize(NetworkReader reader)
        {
            DateTime realDateTime = reader.ReadBoolean() ? DateTime.UtcNow : DateTime.FromBinary(reader.ReadInt64());
            DateTime gameDateTime = DateTime.FromBinary(reader.ReadInt64());
            float timeFactor = reader.ReadSingle();
            return new GameTimeClass(realDateTime, gameDateTime, timeFactor, false);
        }

        public void Serialize(NetworkWriter writer, bool gameOnly)
        {
            writer.Write(gameOnly);
            if (gameOnly)
            {
                writer.Write(this.Calculate().ToBinary());
            }
            else
            {
                writer.Write(this.RealTime.ToBinary());
                writer.Write(this.GameTime.ToBinary());
            }
            writer.Write(this.TimeFactor);
        }

        private DateTime method_0()
        {
            float num = Time.realtimeSinceStartup - this._realtimeSinceStartup;
            return this.RealTime + TimeSpan.FromSeconds((double)num);
        }

        public DateTime Calculate()
        {
            TimeSpan timeSpan = this.method_0() - this.RealTime;
            return this.GameTime + TimeSpan.FromTicks((long)((float)timeSpan.Ticks * this.TimeFactor * this.TimeFactorMod));
        }
    }
}
