using CargoTransportation.Models;
using CargoTransportation.ViewModels.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CargoTransportation.ViewModels
{
    public class CargoViewModel
    {
        public ICollection<Cargo> Cargos { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public CargoFilter CargoFilter { get; set; }
    }
}
