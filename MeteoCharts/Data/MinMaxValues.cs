using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoCharts.Data
{
    public class MinMaxValues
    {
        public float min { get; set; }
        public float max { get; set; }
        public int valuesCollectionCount { get; set; }

        public void SetMinMax(int value)
        {
            if (min > value) min = value;
            if (max < value) max = value;
        }
        public void SetToDrawLeftColumn()
        {
            if (min < 0) min = min - 10;
            float mod = min % 10;
            min = min - mod;            
            max = max + 30;
            mod = max % 10;
            max = max - mod;
            SetValuesCollCount();        
        }
        private void SetValuesCollCount()
        {
            if (max > 0 && min <= 0) valuesCollectionCount = (Int32)(max - min);
            else if (max > 0 && min < 0) valuesCollectionCount = (Int32)(max - min);
            else if (max < 0 && min < 0)
            {
                max = max * -1;
                min = min * -1;
                valuesCollectionCount = (Int32)(max - min);
            }
        }
    }
}
