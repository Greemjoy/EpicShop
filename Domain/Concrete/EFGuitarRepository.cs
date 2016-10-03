using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFGuitarRepository : IGuitarRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<Guitar> Guitars
        {
            get { return context.Guitars; }
        }
    }
}
