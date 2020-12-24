using System;
using System.Collections.Generic;
using System.Text;

namespace XinerjiSocketApp.Model
{
    public class CovidChart
    {
        public CovidChart()
        {
            Counts = new List<int>();
        }

        public string CovidDate { get; set; }

        public List<int> Counts { get; set; }
    }
}
