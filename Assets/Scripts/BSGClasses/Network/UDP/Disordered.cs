using System.Linq;
using TarkovServerU19.Helpers;

namespace TarkovServerU19.BSGClasses
{
    public class Disordered
    {
        public int totalValue;

        public int currentValue;

        public int lastValue;

        public float averageValue;

        private CustomBuffer<int> gclass1008_0 = new CustomBuffer<int>(8);

        public void Increment()
        {
            totalValue++;
            currentValue++;
        }

        public void Increment(int value)
        {
            totalValue += value;
            currentValue += value;
        }

        public void Commit()
        {
            lastValue = currentValue;
            gclass1008_0.PushBack(lastValue);
            averageValue = (float)gclass1008_0.Sum() / (float)gclass1008_0.Count();
            currentValue = 0;
        }
    }
}