using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoCharts.Charts.ChartObjects
{
    public class ChartAxis
    {
        public float x0 { get; set; }
        public float y0 { get; set; }
        public float x1 { get; set; }
        public float y1 { get; set; }

        public int value { get; set; }

        public ChartAxis(float x0, float y0, float x1, float y1, int value)
        {
            this.x0 = x0;
            this.y0 = y0;
            this.x1 = x1;
            this.y1 = y1;
            this.value = value;
        }
    }
}
