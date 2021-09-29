using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;

namespace GuildCars.Models.UIViews
{
    public class AddEditVehicle 
    {
        public Vehicle Vehicle { get; set; }
        public IEnumerable<SelectListItem> VehicleMakes { get; set; }
        public IEnumerable<SelectListItem> VehicleModels { get; set; }
        public IEnumerable<SelectListItem> VehicleType { get; set; }
        public IEnumerable<SelectListItem> BodyStyles { get; set; }
        public IEnumerable<SelectListItem> Transmission { get; set; }
        public IEnumerable<SelectListItem> ExteriorColor { get; set; }
        public IEnumerable<SelectListItem> InteriorColor { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }
        
    }
}

    