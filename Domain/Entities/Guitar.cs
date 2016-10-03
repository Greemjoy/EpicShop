using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Guitar
    {
        public      int      GuitarId           { get; set; }
        public      string   Name               { get; set; }
        public      string   Author             { get; set; }
        public      string   Description        { get; set; }
        public      string   Type               { get; set; }
        public      decimal  Price              { get; set; }

    }
}
