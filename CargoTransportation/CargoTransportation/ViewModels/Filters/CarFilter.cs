using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CargoTransportation.ViewModels.Filters
{
    public class CarFilter
    {
        public DateTime? TechStartDate { get; set; }
        public DateTime? TechEndDate { get; set; }

        public CarFilter(DateTime? techStartDate, DateTime? techEndDate)
        {
            TechStartDate = techStartDate;
            TechEndDate = techEndDate;
        }
    }
}
