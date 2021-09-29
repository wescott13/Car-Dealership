using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class VehicleDetails
    {
        public int VehicleId { get; set; }
        public string MakeName { get; set; }
        public string ModelTypeName { get; set; }
        public string BodyStyleName { get; set; }
        public string TransmissionTypeName { get; set; }
        public string ExteriorColorName { get; set; }
        public string InteriorColorName { get; set; }
        public int Mileage { get; set; }
        public string VIN { get; set; }
        public decimal MSRP { get; set; }
        public decimal SalePrice { get; set; }
        public int VehicleYear { get; set; }
        public string ImageFileName { get; set; }
        public string VehicleDescription { get; set; }
    }
}
