using MeteoCharts.Data;
using MeteoCharts.Interfaces;
using MeteoCharts.NinjectConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoCharts.NinjectConsole
{
    public class ChartNinject
    {
        private IChartable _chart;

        public ChartNinject(IChartable chart)
        {
            _chart = chart;
        }

        public void GenerateChart(MinMaxValues minmax)
        {
            _chart.GenerateChart(minmax);
        }
    }
}
