using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CargoTransportation.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string RegNumber { get; set; }
        public string VinNumber { get; set; }
        public string EngineNumber { get; set; }
        public int YearOfIssue { get; set; }
        public DateTime TechInspection  { get; set; }
        public int EmployeeId { get; set; }  // Driver
        public int CarBrandId { get; set; }

        public Employee Employee { get; set; }
        public CarBrand CarBrand { get; set; }

        public ICollection<Flight> Flights { get; set; }

        public Car()
        {
            Flights = new List<Flight>();
        }
    }
}
