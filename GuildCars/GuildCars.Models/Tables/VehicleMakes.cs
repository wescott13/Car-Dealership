using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class VehicleMakes
    {
        public int MakeId { get; set; }
        public string MakeName { get; set; }
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string UserEmail { get; set; }
        public string Id { get; set; }
    }
}
