using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class PurchaseVehicle
    {
        public int PurchaseId { get; set; }
        public int VehicleId { get; set; }
        public string UserId { get; set; }
        public string PurchaseName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string StateId { get; set; }

        [Display(Name = "Zip Code")]
        [StringLength(10, MinimumLength = 5)]
        [RegularExpression("(^\\d{5}(-\\d{4})?$)|(^[ABCEGHJKLMNPRSTVXY]{1}\\d{1}[A-Z]{1} *\\d{1}[A-Z]{1}\\d{1}$)", ErrorMessage = "Zip code is invalid.")] // US or Canada
        [Required(ErrorMessage = "Zip Code is Required.")]
        public string ZipCode { set; get; }

        public decimal PurchasePrice { get; set; }
        public int PurchaseTypeId { get; set; }
        public DateTime PurchaseDate { get; set; }

    }
}
