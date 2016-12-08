using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class Guitar
    {
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "ID")]
        public      int      GuitarId           { get; set; }
        [Display(Name = "Name")]
        public      string   Name               { get; set; }
        [Display(Name = "Author")]
        public      string   Author             { get; set; }
        [Display(Name = "Description")]
        public      string   Description        { get; set; }
        [Display(Name = "Type")]
        public      string   Type               { get; set; }
        [Display(Name = "Price")]
        public      decimal  Price              { get; set; }

    }
}
