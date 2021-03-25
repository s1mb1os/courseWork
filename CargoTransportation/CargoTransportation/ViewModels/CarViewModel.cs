using CargoTransportation.Models;
using CargoTransportation.ViewModels.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CargoTransportation.ViewModels
{
    public class CarViewModel
    {
        public ICollection<Car> Cars { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public CarFilter CarFilter { get; set; }
    }
}
