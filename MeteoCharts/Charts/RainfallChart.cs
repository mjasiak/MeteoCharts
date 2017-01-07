using MeteoCharts.Charts.ChartObjects;
using MeteoCharts.Data;
using MeteoCharts.Interfaces;
using MeteoCharts.Render;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoCharts.Charts
{
    public class RainfallChart : Chart, IChartable
    {
        private RainfallChartData _rainChartData;
        public RainfallChart(RainfallChartData rainChartData)
        {
            _rainChartData = rainChartData;
        }
      
        public void DrawChart(int canvasHeight, int spaceBetween, string pathFileSave)
        {
            this.canvasHeight = canvasHeight;
            MathChart(canvasHeight, spaceBetween);
            using (var surface = SKSurface.Create(canvasWidth, canvasHeight, SKColorType.Rgb565, SKAlphaType.Premul))
            {
                SKCanvas canvas = surface.Canvas;
                canvas.Clear(SKColors.White);

                canvas = DrawChartAxis(canvas);

                ImageRender.Render(surface, pathFileSave);
            }
        }
        public void MathChart(int canvasHeight, int spaceBetween)
        {
            canvasWidth = _rainChartData.RainfallChartDataItems.Count() * spaceBetween;
            _chartSetting = SetChartRange(_chartSetting, GetValues(), canvasHeight);
            MathChartAxis(_chartSetting, canvasHeight);
        }

        protected override SKCanvas DrawChartAxis(SKCanvas canvas)
        {
            return canvas;
        }

        private IEnumerable<int> GetValues()
        {
            List<int> values = new List<int>();
            foreach (var value in _rainChartData.RainfallChartDataItems)
            {
                values.Add((Int32)value.Value);
            }
            return values;
        }
        public ChartRangeSetting SetChartRange(ChartRangeSetting chartSetting, IEnumerable<int> values)
        {
            return chartSetting;
        }
    }
}
