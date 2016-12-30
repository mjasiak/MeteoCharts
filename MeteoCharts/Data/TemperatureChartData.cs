using System.Collections.Generic;

namespace MeteoCharts.Data
{
	public class TemperatureChartData
	{
		public string Id { get; set; }
		public IEnumerable<TemperatureChartDataItem> TemperatureChartDataItems { get; set; }
	}
}