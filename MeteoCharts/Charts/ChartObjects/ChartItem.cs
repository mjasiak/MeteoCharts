using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoCharts.Charts.ChartObjects
{
    public class ChartItem
    {
        public float x { get; set; }
        public float y { get; set; }

        public TimeSpan Time { get; set; }
        public int chartValue { get; set; }
    }
}
