using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class VehicleModels
    {
        public int ModelId { get; set; }
        public int MakeId { get; set; }
        public string ModelTypeName { get; set; }

        public string MakeTypeName { get; set; }

        public DateTime CreatedDate { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string UserEmail { get; set; }
    }
}
