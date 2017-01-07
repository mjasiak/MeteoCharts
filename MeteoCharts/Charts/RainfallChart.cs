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
            MathChart(spaceBetween);
            using (var surface = SKSurface.Create(canvasWidth, canvasHeight, SKColorType.Rgb565, SKAlphaType.Premul))
            {
                SKCanvas canvas = surface.Canvas;
                canvas.Clear(SKColors.White);

                canvas = DrawChartAxis(canvas);
                canvas = DrawHours<IEnumerable<RainfallChartDataItem>>(_rainChartData.RainfallChartDataItems,canvas);

                ImageRender.Render(surface, pathFileSave);
            }
        }
        protected override void MathChart(int spaceBetween)
        {
            canvasWidth = _rainChartData.RainfallChartDataItems.Count() * spaceBetween;
            _chartSetting = SetChartRange(_chartSetting, GetValues(), canvasHeight);
            MathChartAxis(_chartSetting, canvasHeight);
            MathChartValues<IEnumerable<RainfallChartDataItem>>(_rainChartData.RainfallChartDataItems, spaceBetween);
        }
        
        protected override SKCanvas DrawChartAxis(SKCanvas canvas)
        {
            SKPaint paint = new SKPaint() { Color = new SKColor(208, 208, 208), IsAntialias = true, StrokeCap = SKStrokeCap.Round };
            foreach (var axis in _chartAxis)
            {                
                canvas.DrawLine(axis.x0 + (0.08f * canvasHeight), axis.y0, axis.x1 + (0.09f * canvasHeight), axis.y1, paint);
            }
            return canvas;
        }
        protected override SKCanvas DrawChartValues(SKCanvas canvas)
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
        public ChartSetting SetChartRange(ChartSetting chartSetting, IEnumerable<int> values)
        {
            return chartSetting;
        }
    }
}
