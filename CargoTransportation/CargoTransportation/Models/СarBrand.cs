using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CargoTransportation.Models
{
    public class CarBrand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MaxSpeed { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public ICollection<Car> Cars { get; set; }

        public CarBrand()
        {
            Cars = new List<Car>();
        }
    }
}
