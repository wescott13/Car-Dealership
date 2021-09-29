using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public int MakeId { get; set; }
        public int? ModelId { get; set; }
        public int TypeId { get; set; }
        public int? BodyStyleId { get; set; }
        public int? TransmissionId { get; set; }
        public int? ExteriorColorId { get; set; }
        public int? InteriorColorId { get; set; }
        public int Mileage { get; set; }
        public string VIN { get; set; }
        public decimal MSRP { get; set; }
        public decimal SalePrice { get; set; }
        public string VehicleDescription { get; set; }
        public int VehicleYear { get; set; }
        public bool HasFeatured { get; set; }
        public string ImageFileName { get; set; }

    }
}
