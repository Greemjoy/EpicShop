using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public IEnumerable<CartLine>Lines { get { return lineCollection; } }
        public void AddItem(Guitar guitar, int quantity)
        {
            CartLine line = lineCollection
                .Where(b => b.Guitar.GuitarId == guitar.GuitarId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine { Guitar = guitar, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void RemoveLine(Guitar guitar)
        {
            lineCollection.RemoveAll(l => l.Guitar.GuitarId == guitar.GuitarId);
        }
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Guitar.Price * e.Quantity);
        }
        public void Clear()
        {
            lineCollection.Clear();
        }
    }

    public class CartLine
    {
        public Guitar Guitar { get; set; }
        public int Quantity { get; set; }
    }
}
