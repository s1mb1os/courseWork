using CargoTransportation.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CargoTransportation.Data
{
    public class CargoTransportationContext : DbContext
    {
        public CargoTransportationContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<CarBrand> CarBrands { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<TypeOfCargo> TypeOfCargos { get; set; }
    }
}
