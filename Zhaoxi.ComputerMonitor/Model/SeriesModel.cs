using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.ComputerMonitor.Model
{
    public class SeriesModel
    {
        public int SeriesId { get; set; }
        public string SeriesName { get; set; }
        public int CourseId { get; set; }
        public decimal CurrentValue { get; set; }
        public bool IsGrowing { get; set; }
        public int GrowingRate { get; set; }
    }
}
