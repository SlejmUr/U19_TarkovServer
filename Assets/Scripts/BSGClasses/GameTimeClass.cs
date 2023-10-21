using System;
using UnityEngine;
using UnityEngine.Networking;

namespace TarkovServerU19.BSGClasses
{
    internal class GameTimeClass
    {

        internal bool Boolean_0 { get; private set; }
        internal DateTime DateTime_0 { get; private set; }
        internal DateTime DateTime_1 { get; private set; }
        public float TimeFactor { get; private set; }

        private float _realtimeSinceStartup;

        public float TimeFactorMod = 1f;
        internal GameTimeClass(DateTime realDateTime, DateTime gameDateTime, float timeFactor, bool debug = false)
        {
            this._realtimeSinceStartup = Time.realtimeSinceStartup;
            this.DateTime_0 = realDateTime;
            this.DateTime_1 = gameDateTime;
            this.TimeFactor = timeFactor;
            this.Boolean_0 = debug;
            Debug.Log(string.Concat(new object[]
            {
                "RealDateTime:",
                this.DateTime_0,
                "  GameDateTime:",
                this.DateTime_1,
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
                writer.Write(this.DateTime_0.ToBinary());
                writer.Write(this.DateTime_1.ToBinary());
            }
            writer.Write(this.TimeFactor);
        }

        private DateTime method_0()
        {
            float num = Time.realtimeSinceStartup - this._realtimeSinceStartup;
            return this.DateTime_0 + TimeSpan.FromSeconds((double)num);
        }

        public DateTime Calculate()
        {
            TimeSpan timeSpan = this.method_0() - this.DateTime_0;
            return this.DateTime_1 + TimeSpan.FromTicks((long)((float)timeSpan.Ticks * this.TimeFactor * this.TimeFactorMod));
        }
    }
}
