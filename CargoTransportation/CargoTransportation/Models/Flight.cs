using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CargoTransportation.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CarId { get; set; }
        public int CargoId { get; set; }
        public double Price { get; set; }
        public bool IsPayment { get; set; }
        public bool IsReturn { get; set; }

        public Car Car { get; set; }
        public Cargo Cargo { get; set; }
    }
}
