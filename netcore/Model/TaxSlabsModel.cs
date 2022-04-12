using System;
using System.Collections.Generic;
using System.Text;

namespace Congestion.Calculator
{
    internal class TaxSlabsModel
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public int Toll { get; set; }
    }
}
