using MeteoCharts.Charts.ChartObjects;
using System;

namespace MeteoCharts.Data
{
	public class RainfallChartDataItem : ChartItem
	{
		public TimeSpan Time { get; set; }
		public uint Value { get; set; }
	}
}