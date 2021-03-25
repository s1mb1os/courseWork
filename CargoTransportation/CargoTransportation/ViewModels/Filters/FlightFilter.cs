using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CargoTransportation.ViewModels.Filters
{
    public class FlightFilter
    {
        public bool FlightsFromLastWeek { get; set; }

        public FlightFilter(bool flightsFromLastWeek)
        {
            FlightsFromLastWeek = flightsFromLastWeek;
        }
    }
}
