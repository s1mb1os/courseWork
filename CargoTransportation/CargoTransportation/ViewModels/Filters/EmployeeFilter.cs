using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CargoTransportation.ViewModels.Filters
{
    public class EmployeeFilter
    {
        public string Position { get; set; }

        public EmployeeFilter(string position)
        {
            Position = position;
        }
    }
}
