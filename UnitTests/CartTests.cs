using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Entities;
using System.Linq;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_Lines()
        {
            Guitar guitar1 = new Guitar { GuitarId = 1, Name = "Guitar1" };
            Guitar guitar2 = new Guitar { GuitarId = 2, Name = "Guitar2" };

            Cart cart = new Cart();

            cart.AddItem(guitar1, 1);
            cart.AddItem(guitar2, 1);
            List<CartLine> results = cart.Lines.ToList();

            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Guitar, guitar1);
            Assert.AreEqual(results[1].Guitar, guitar2);    
        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            Guitar guitar1 = new Guitar { GuitarId = 1, Name = "Guitar1" };
            Guitar guitar2 = new Guitar { GuitarId = 2, Name = "Guitar2" };

            Cart cart = new Cart();

            cart.AddItem(guitar1, 1);
            cart.AddItem(guitar2, 1);
            cart.AddItem(guitar1, 5);
            List<CartLine> results = cart.Lines.OrderBy(c => c.Guitar.GuitarId).ToList();

            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Quantity, 6);
            Assert.AreEqual(results[1].Quantity, 1);
        }
    }
}
