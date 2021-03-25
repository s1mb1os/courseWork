using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CargoTransportation.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string PassportData { get; set; }
        public string Position { get; set; }

        public ICollection<Car> Cars { get; set; }

        public Employee()
        {
            Cars = new List<Car>();
        }
    }
}
