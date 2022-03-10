using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static FastFoodRestaurant.Data.DataConstants.Client;
namespace FastFoodRestaurant.Models.Client
{
    public class InformationModel
    {

        [Required]
        [StringLength(maxClientNameLength , MinimumLength = minClientNameLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(phoneNumberMaxLength)]
        [Display(Name = "Phone Number")]
        [RegularExpression(phoneNumberRegEx, ErrorMessage = "Phone is not valid")]
        public string PhoneNumber { get; set; }


        [Required]
        [StringLength(maxAddressLength, MinimumLength = minAddressLength)]
        public string Address { get; set; }

    }
}
