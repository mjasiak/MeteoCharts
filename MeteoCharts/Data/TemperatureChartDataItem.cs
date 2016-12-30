using System;
using MeteoCharts.Enums;

namespace MeteoCharts.Data
{
	public class TemperatureChartDataItem
	{
		public TimeSpan Time { get; set; }
		public int Value { get; set; }
		public WeatherIconType IconType { get; set; }
	}
}