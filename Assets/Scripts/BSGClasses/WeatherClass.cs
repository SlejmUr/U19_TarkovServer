using System;
using System.Collections.Generic;
using TarkovServerU19.BSGEnums;
using UnityEngine;

namespace TarkovServerU19.BSGClasses
{
    internal class WeatherClass
    {
        public static WeatherClass CreateDefault()
        {
            return new WeatherClass
            {
                Time = TimeHandler.UtcNow.Ticks,
                Cloudness = -0.3f,
                Wind = 4f,
                WindDirection = 7,
                Rain = 1f,
                RainRandomness = 0.5f,
                ScaterringFogDensity = 0.004f,
                Temperature = 20f,
                AtmospherePressure = 760f,
                LyingWater = 0f
            };
        }

        // Token: 0x0600A829 RID: 43049 RVA: 0x002E496C File Offset: 0x002E2B6C
        public EWeatherType GetWeatherTypeByNode()
        {
            if (this.Cloudness >= -0.7f && this.Wind <= 1f && this.Rain >= 1f && this.Rain <= 3f)
            {
                return EWeatherType.LightRain;
            }
            if (this.Wind <= 1f && this.Rain > 3f)
            {
                return EWeatherType.Rain;
            }
            if ((double)this.Cloudness < -0.4 && this.Wind <= 1f && this.Rain < 2f && this.ScaterringFogDensity <= 0.004f)
            {
                return EWeatherType.ClearDay;
            }
            if ((double)this.Cloudness < -0.4 && this.Wind > 1f && this.Rain < 2f && this.ScaterringFogDensity <= 0.004f)
            {
                return EWeatherType.ClearWind;
            }
            if (this.Cloudness >= -0.7f && this.Cloudness <= -0.4f && this.Wind <= 1f && this.Rain < 2f && this.ScaterringFogDensity <= 0.004f)
            {
                return EWeatherType.PartlyCloudDay;
            }
            if (this.Cloudness < -0.4f && this.ScaterringFogDensity > 0.004f && this.ScaterringFogDensity < 0.1f)
            {
                return EWeatherType.ClearFogDay;
            }
            if (this.ScaterringFogDensity >= 0.1f)
            {
                return EWeatherType.Fog;
            }
            if (this.Cloudness >= -0.4f && this.Cloudness <= 0.7f && this.ScaterringFogDensity > 0.004f)
            {
                return EWeatherType.CloudFog;
            }
            if (this.Cloudness >= -0.4f && this.Cloudness <= 0.7f && this.Rain <= 1f)
            {
                return EWeatherType.MostlyCloud;
            }
            if (this.Cloudness >= 0.7f && this.Cloudness <= 1f && this.Rain <= 1f)
            {
                return EWeatherType.FullCloud;
            }
            if (this.Cloudness >= 1f && this.Rain <= 1f)
            {
                return EWeatherType.ThunderCloud;
            }
            if (this.Cloudness >= 0f && this.Wind >= 2f && this.Rain <= 1f)
            {
                return EWeatherType.CloudWind;
            }
            if (this.Wind >= 2f && this.Rain >= 2f)
            {
                return EWeatherType.CloudWindRain;
            }
            Debug.LogWarning("Weather with node " + this + " has not been found!");
            return EWeatherType.None;
        }

        // Token: 0x0600A82A RID: 43050 RVA: 0x002E4BB8 File Offset: 0x002E2DB8
        public static WeatherClass[] GetFineTestWeatherNodes()
        {
            DateTime dateTime = TimeHandler.Now;
            TimeSpan t = TimeSpan.FromSeconds((double)600f);
            List<WeatherClass> list = new List<WeatherClass>();
            for (int i = 0; i < 2; i++)
            {
                list.Add(WeatherClass.smethod_1(dateTime));
                dateTime += t;
            }
            return list.ToArray();
        }

        // Token: 0x0600A82B RID: 43051 RVA: 0x002E4C04 File Offset: 0x002E2E04
        public static WeatherClass[] GetRandomTestWeatherNodes(int nodeDurationSeconds = 600, int nodeCount = 12)
        {
            DateTime dateTime = TimeHandler.Now;
            TimeSpan t = TimeSpan.FromSeconds((double)nodeDurationSeconds);
            List<WeatherClass> list = new List<WeatherClass>();
            for (int i = 0; i < nodeCount; i++)
            {
                list.Add(WeatherClass.smethod_0(dateTime));
                dateTime += t;
            }
            return list.ToArray();
        }

        // Token: 0x0600A82C RID: 43052 RVA: 0x002E4C4C File Offset: 0x002E2E4C
        private static WeatherClass smethod_0(DateTime dateTime)
        {
            float num = UnityEngine.Random.Range(-1f, 1f);
            int windDirection = UnityEngine.Random.Range(1, 8);
            float num2 = UnityEngine.Random.Range(0f, 4f);
            float rainRandomness = UnityEngine.Random.Range(0f, 1f);
            int num3;
            float scaterringFogDensity;
            if (num > 0.5f)
            {
                num3 = UnityEngine.Random.Range(0, 5);
                rainRandomness = 1f;
                scaterringFogDensity = 0.004f;
                switch (num3)
                {
                    case 0:
                        break;
                    case 1:
                        scaterringFogDensity = 0.008f;
                        goto IL_C5;
                    case 2:
                        scaterringFogDensity = 0.012f;
                        goto IL_C5;
                    case 3:
                        scaterringFogDensity = 0.02f;
                        goto IL_C5;
                    case 4:
                        scaterringFogDensity = 0.03f;
                        goto IL_C5;
                    default:
                        goto IL_C5;
                }
            }
            else
            {
                num3 = 0;
            }
            scaterringFogDensity = UnityEngine.Random.Range(0.003f, 0.006f);
        IL_C5:
            return new WeatherClass
            {
                Time = dateTime.Ticks,
                Cloudness = num,
                WindDirection = windDirection,
                TopWindDirection = UnityEngine.Random.insideUnitCircle * num2,
                Wind = num2,
                Rain = (float)num3,
                RainRandomness = rainRandomness,
                Temperature = 22f,
                AtmospherePressure = 780f,
                ScaterringFogDensity = scaterringFogDensity
            };
        }

        // Token: 0x0600A82D RID: 43053 RVA: 0x002E4D84 File Offset: 0x002E2F84
        private static WeatherClass smethod_1(DateTime dateTime)
        {
            return new WeatherClass
            {
                Time = dateTime.Ticks,
                Cloudness = -0.371f,
                WindDirection = 8,
                Wind = 1f,
                Rain = 1f,
                RainRandomness = 0f,
                Temperature = 10f,
                AtmospherePressure = 780f,
                ScaterringFogDensity = 0f,
                TopWindDirection = new Vector2(-1f, 0f)
            };
        }

        // Token: 0x0600A82E RID: 43054 RVA: 0x002E4E0C File Offset: 0x002E300C
        private static WeatherClass smethod_2(DateTime dateTime)
        {
            return new WeatherClass
            {
                Time = dateTime.Ticks,
                Cloudness = -0.371f,
                WindDirection = 8,
                Wind = 1.5f,
                Rain = 0f,
                RainRandomness = 1f,
                Temperature = 0f,
                AtmospherePressure = 780f,
                ScaterringFogDensity = 0.009f,
                TopWindDirection = new Vector2(-1f, 0f)
            };
        }

        // Token: 0x0600A82F RID: 43055 RVA: 0x002E4E94 File Offset: 0x002E3094
        public static WeatherClass GetHalloweenWeatherNode(int secDuration)
        {
            return new WeatherClass
            {
                Time = TimeHandler.MoscowNow.AddSeconds((double)secDuration).Ticks,
                Cloudness = 1.3f,
                WindDirection = 8,
                Wind = 0f,
                Rain = 0f,
                RainRandomness = 0f,
                Temperature = 20f,
                AtmospherePressure = 760f,
                ScaterringFogDensity = 0.018f,
                TopWindDirection = new Vector2(-1f, 0f)
            };
        }

        // Token: 0x0600A830 RID: 43056 RVA: 0x002E4F2C File Offset: 0x002E312C
        public override string ToString()
        {
            return string.Format(string.Concat(new object[]
            {
            "Time: ",
            this.Time,
            "\nCloudness: ",
            this.Cloudness,
            "\nWind: ",
            this.Wind,
            "\nWindDirection: ",
            this.WindDirection,
            "\nTurbulence: ",
            this.Turbulence,
            "\nRain: ",
            this.Rain,
            "\nRainRandomness: ",
            this.RainRandomness,
            "\nScaterringFogDensity: ",
            this.ScaterringFogDensity,
            "\nScaterringFogHeight: ",
            this.ScaterringFogHeight,
            "\nGlobalFogDensity: ",
            this.GlobalFogDensity,
            "\nGlobalFogHeight: ",
            this.GlobalFogHeight,
            "\nTemperature: ",
            this.Temperature,
            "\nAtmospherePressure: ",
            this.AtmospherePressure,
            "\nMainWindPosition: ",
            this.MainWindPosition,
            "\nMainWindDirection: ",
            this.MainWindDirection,
            "\nTopWindPosition: ",
            this.TopWindPosition,
            "\nTopWindDirection: ",
            this.TopWindDirection,
            "\nLyingWater: ",
            this.LyingWater,
            "\n"
            }), Array.Empty<object>());
        }

        // Token: 0x0400983B RID: 38971
        public static readonly Vector2[] WindDirections = new Vector2[]
        {
        new Vector2(0.1f, 0.1f),
        new Vector2(-1f, 0f),
        new Vector2(0f, -1f),
        new Vector2(1f, 0f),
        new Vector2(0f, 1f),
        new Vector2(-1f, 1f).normalized,
        new Vector2(1f, 1f).normalized,
        new Vector2(1f, -1f).normalized,
        new Vector2(-1f, -1f).normalized
        };

        // Token: 0x0400983C RID: 38972
        public long Time;

        // Token: 0x0400983D RID: 38973
        public float Cloudness;

        // Token: 0x0400983E RID: 38974
        public float Wind;

        // Token: 0x0400983F RID: 38975
        public int WindDirection;

        // Token: 0x04009840 RID: 38976
        public float Turbulence;

        // Token: 0x04009841 RID: 38977
        public float Rain;

        // Token: 0x04009842 RID: 38978
        public float RainRandomness;

        // Token: 0x04009843 RID: 38979
        public float ScaterringFogDensity;

        // Token: 0x04009844 RID: 38980
        public float ScaterringFogHeight;

        // Token: 0x04009845 RID: 38981
        public float GlobalFogDensity;

        // Token: 0x04009846 RID: 38982
        public float GlobalFogHeight;

        // Token: 0x04009847 RID: 38983
        public float Temperature;

        // Token: 0x04009848 RID: 38984
        public float AtmospherePressure;

        // Token: 0x04009849 RID: 38985
        public Vector2 MainWindPosition;

        // Token: 0x0400984A RID: 38986
        public Vector2 MainWindDirection;

        // Token: 0x0400984B RID: 38987
        public Vector2 TopWindPosition;

        // Token: 0x0400984C RID: 38988
        public Vector2 TopWindDirection;

        // Token: 0x0400984D RID: 38989
        public float LyingWater;
    }
}
