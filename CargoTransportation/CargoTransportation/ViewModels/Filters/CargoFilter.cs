using CargoTransportation.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CargoTransportation.ViewModels.Filters
{
    public class CargoFilter
    {
        public int? SelectedTypeOfCargoId { get; set; }
        public SelectList TypeOfCargoes { get; set; }

        public CargoFilter(int? selectedTypeOfCargoId, IList<TypeOfCargo> typeOfCargo)
        {
            typeOfCargo.Insert(0, new TypeOfCargo()
            {
                Id = 0,
                Name = "All"
            });

            SelectedTypeOfCargoId = selectedTypeOfCargoId;
            TypeOfCargoes = new SelectList(typeOfCargo, "Id", "Name", selectedTypeOfCargoId);
        }
    }
}
