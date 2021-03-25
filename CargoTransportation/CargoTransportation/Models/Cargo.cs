using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CargoTransportation.Models
{
    public class Cargo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeOfCargoId { get; set; }
        public DateTime ShelfLife  { get; set; } // Срок годности
        public string Features { get; set; }

        public TypeOfCargo TypeOfCargo { get; set; }

        public ICollection<Flight> Flights { get; set; }

        public Cargo()
        {
            Flights = new List<Flight>();
        }
    }
}
