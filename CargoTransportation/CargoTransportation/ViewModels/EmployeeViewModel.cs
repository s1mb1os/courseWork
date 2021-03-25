using CargoTransportation.Models;
using CargoTransportation.ViewModels.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CargoTransportation.ViewModels
{
    public class EmployeeViewModel
    {
        public ICollection<Employee> Employees { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public EmployeeFilter EmployeeFilter { get; set; }
    }
}
