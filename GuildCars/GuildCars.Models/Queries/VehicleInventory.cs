using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class VehicleInventory
    {
        public int VehicleCount { get; set; }
        public int VehicleYear { get; set; }
        public string MakeName { get; set; }
        public string ModelTypeName { get; set; }
        public decimal StockValue { get; set; }
        public int VehicleInvId { get; set; }
        public int VehicleId { get; set; }
    }
}
