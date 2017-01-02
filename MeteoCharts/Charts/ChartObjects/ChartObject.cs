using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoCharts.Charts.ChartObjects
{
    public class ChartObject
    {
        public int x { get; set; }
        public int y { get; set; }

        public int value { get; set; }
        public TimeSpan hour { get; set; }
    }
}
