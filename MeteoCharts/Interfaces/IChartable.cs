using MeteoCharts.Data;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoCharts.Interfaces
{
    public interface IChartable
    {
        void GenerateChart(int canvasWidth, int canvasHeight);
        SKCanvas DrawChartAxis(SKCanvas canvas, SKPaint paint, int canvasHeight, int canvasWidth);
    }
}
