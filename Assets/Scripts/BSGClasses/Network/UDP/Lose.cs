using System.Linq;
using TarkovServerU19.Helpers;

namespace TarkovServerU19.BSGClasses
{
    public class Lose
    {
        // Token: 0x0600BE11 RID: 48657 RVA: 0x003360B8 File Offset: 0x003342B8
        public void Increment(int total, int lose)
        {
            this.totalCount += total;
            this.loseCount += lose;
        }

        // Token: 0x0600BE12 RID: 48658 RVA: 0x003360D8 File Offset: 0x003342D8
        public void Commit()
        {
            if (this.totalCount == 0)
            {
                return;
            }
            this.lastLoseCountValue = this.loseCount;
            this.gclass1008_0.PushBack(this.lastLoseCountValue);
            this.averageLoseCountValue = this.gclass1008_0.Sum() / this.gclass1008_0.Count<int>();
            this.lastLosePercentValue = (float)this.loseCount / (float)this.totalCount;
            this.gclass1008_1.PushBack(this.lastLosePercentValue);
            this.averageLosePercentValue = this.gclass1008_1.Sum() / (float)this.gclass1008_1.Count<float>();
            this.loseCount = 0;
            this.totalCount = 0;
        }

        // Token: 0x0400A9D7 RID: 43479
        public int totalCount;

        // Token: 0x0400A9D8 RID: 43480
        public int loseCount;

        // Token: 0x0400A9D9 RID: 43481
        public int lastLoseCountValue;

        // Token: 0x0400A9DA RID: 43482
        public int averageLoseCountValue;

        // Token: 0x0400A9DB RID: 43483
        public float lastLosePercentValue;

        // Token: 0x0400A9DC RID: 43484
        public float averageLosePercentValue;

        // Token: 0x0400A9DD RID: 43485
        private CustomBuffer<int> gclass1008_0 = new CustomBuffer<int>(16);

        // Token: 0x0400A9DE RID: 43486
        private CustomBuffer<float> gclass1008_1 = new CustomBuffer<float>(16);
    }
}