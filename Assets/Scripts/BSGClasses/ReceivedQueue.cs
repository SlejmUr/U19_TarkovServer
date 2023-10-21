using TarkovServerU19.Helpers;
using System.Linq;

namespace TarkovServerU19.BSGClasses
{
    public class ReceivedQueue
    {
        public float value;

        public float averageValue;

        private GClass1008<float> gclass1008_0 = new GClass1008<float>(8);

        public void Set(float value)
        {
            this.value = value;
            gclass1008_0.PushBack(value);
            averageValue = gclass1008_0.Sum() / (float)gclass1008_0.Count();
        }
    }
}