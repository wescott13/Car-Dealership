using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class FeaturedVehicleShortItem
    {
        public int vehicleId { get; set; }
        public string MakeName { get; set; }
        public string ModelTypeName { get; set; }
        public decimal SalePrice { get; set; }
        public int VehicleYear { get; set; }
        public string ImageFileName { get; set; }
    }
}
