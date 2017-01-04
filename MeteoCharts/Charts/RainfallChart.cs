using MeteoCharts.Charts.ChartObjects;
using MeteoCharts.Interfaces;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoCharts.Charts
{
    public class RainfallChart : IChartable
    {
        public void GenerateChart()
        {

        }
        public void MathChart()
        {

        }
        public void DrawChart(int nowy, int spaceBetween)
        {

        }
        public IEnumerable<int> GetValues()
        {           
            return new List<int>();
        }
        public ChartRangeSetting SetChartRange(ChartRangeSetting chartSetting, IEnumerable<int> values)
        {
            return chartSetting;
        }
    }
}
