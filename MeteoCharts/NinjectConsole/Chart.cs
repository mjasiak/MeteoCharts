using MeteoCharts.Interfaces;
using MeteoCharts.NinjectConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoCharts.NinjectConsole
{
    public class Chart
    {
        private IChartable _chart;

        public Chart(IChartable chart)
        {
            _chart = chart;
        }

        public void GenerateChart()
        {
            _chart.GenerateChart();
        }
    }
}
