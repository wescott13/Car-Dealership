using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;

namespace GuildCars.Models.UIViews
{
    public class SalesPurchase
    {
        public VehicleDetails vehicleDetails {get; set;}
        public PurchaseVehicle purchaseDetails { get; set; }
        public IEnumerable<SelectListItem> States { get; set; }
        public IEnumerable<SelectListItem> PurchaseTypes { get; set; }
    }
}
