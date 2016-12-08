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

        public void SaveGuitar(Guitar gutiar)
        {
            if (gutiar.GuitarId == 0)
            {
                context.Guitars.Add(gutiar);
            }
            else
            {
                Guitar dbEntry = context.Guitars.Find(gutiar.GuitarId);
                if (dbEntry != null)
                {
                    dbEntry.Name = gutiar.Name;
                    dbEntry.Author = gutiar.Author;
                    dbEntry.Description = gutiar.Description;
                    dbEntry.Type = gutiar.Type;
                    dbEntry.Price = gutiar.Price;
                }
            }
            context.SaveChanges();
        }
    }
}
