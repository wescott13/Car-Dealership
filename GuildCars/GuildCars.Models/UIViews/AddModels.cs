using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using GuildCars.Models.Tables;

namespace GuildCars.Models.UIViews
{
    public class AddModels
    {
        public IEnumerable<SelectListItem> VehicleMakes { get; set; }
        public VehicleModels vehicleModel { get; set; }
        public List<VehicleModels> ModelsList { get; set; }
    }
}
