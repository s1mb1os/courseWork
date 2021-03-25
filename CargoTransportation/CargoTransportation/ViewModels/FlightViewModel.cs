using CargoTransportation.Models;
using CargoTransportation.ViewModels.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CargoTransportation.ViewModels
{
    public class FlightViewModel
    {
        public ICollection<Flight> Flights { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FlightFilter FlightFilter { get; set; }
    }
}
