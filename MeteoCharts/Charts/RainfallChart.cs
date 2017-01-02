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
        public void GenerateChart(int nowy, int stary)
        {

        }
        public SKCanvas DrawChartAxis(SKCanvas canvas, SKPaint paint, int canvasHeight, int canvasWidth)
        {
            return canvas;
        }
    }
}
