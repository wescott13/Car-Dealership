using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Models.Queries;

namespace GuildCars.Models.UIViews
{
    public class InventoryReport
    {
        public List<VehicleInventory> NewVehicles { get; set; }
        public List<VehicleInventory> UsedVehicles { get; set; }
    }
}
