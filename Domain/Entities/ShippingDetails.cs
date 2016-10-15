using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Set your name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Set your adress")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        [Required(ErrorMessage = "Set your city")]
        public string City { get; set; }
        [Required(ErrorMessage = "Set your country")]
        public string Country { get; set; }
        public bool GiftWrap { get; set; }
    }
}
