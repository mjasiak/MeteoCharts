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
        void GenerateChart();
        void MathChart();
        void DrawChart(int canvasWidth, int canvasHeight);
    }
}
