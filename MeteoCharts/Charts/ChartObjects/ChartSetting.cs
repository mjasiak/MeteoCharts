using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoCharts.Charts.ChartObjects
{
    public class ChartSetting
    {
        public int min { get; set; }
        public int max { get; set; }
        public int minInScale { get; set; }
        public int maxInScale { get; set; }

        public int valuesRange { get; set; }
        public int valuesRangeInScale { get; set; }

        public float oneInScale { get; set; }

        public float heightOfAxis { get; set; }

        public void setInScale(int min, int max)
        {
            if (min < 0) min -= 10;
            max += 30;

            max -= max % 10;
            min -= min % 10;

            minInScale = min;
            maxInScale = max;

            if (max < 0 && min < 0) valuesRangeInScale = (max * -1) - (min * -1);
            else valuesRangeInScale = max - min;

            if (max < 0 && min < 0) valuesRange = (max * -1) - (min * -1);
            else valuesRange = max - min; 
        }
    }
}
