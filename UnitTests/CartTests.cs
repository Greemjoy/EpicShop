using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Entities;
using System.Linq;
using System.Collections.Generic;
using Domain.Abstract;
using Moq;
using WebUI.Controllers;

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
        [TestMethod]
        public void Can_Remove_Lines()
        {
            Guitar guitar1 = new Guitar { GuitarId = 1, Name = "Guitar1" };
            Guitar guitar2 = new Guitar { GuitarId = 2, Name = "Guitar2" };
            Guitar guitar3 = new Guitar { GuitarId = 3, Name = "Guitar3" };

            Cart cart = new Cart();

            cart.AddItem(guitar1, 1);
            cart.AddItem(guitar2, 1);
            cart.AddItem(guitar1, 5);
            cart.AddItem(guitar3, 2);
            cart.RemoveLine(guitar2);

            Assert.AreEqual(cart.Lines.Where(c => c.Guitar == guitar2).Count(), 0);
            Assert.AreEqual(cart.Lines.Count(), 2);
        }
        [TestMethod]
        public void Calculate_Cart_Total()
        {
            Guitar guitar1 = new Guitar { GuitarId = 1, Name = "Guitar1", Price = 100   };
            Guitar guitar2 = new Guitar { GuitarId = 2, Name = "Guitar2" ,Price = 55    };

            Cart cart = new Cart();

            cart.AddItem(guitar1, 1);
            cart.AddItem(guitar2, 1);
            cart.AddItem(guitar1, 5);
            decimal result = cart.ComputeTotalValue();

            Assert.AreEqual(result, 655);
        }
        [TestMethod]
        public void Can_Clear_Contents()
        {
            Guitar guitar1 = new Guitar { GuitarId = 1, Name = "Guitar1", Price = 100 };
            Guitar guitar2 = new Guitar { GuitarId = 2, Name = "Guitar2", Price = 55 };

            Cart cart = new Cart();

            cart.AddItem(guitar1, 1);
            cart.AddItem(guitar2, 1);
            cart.AddItem(guitar1, 5);
            cart.Clear();

            Assert.AreEqual(cart.Lines.Count(), 0);
        }
        [TestMethod]
        public void Can_Add_To_Cart()
        {
            Mock<IGuitarRepository> mock = new Mock<IGuitarRepository>();
            mock.Setup(m => m.Guitars).Returns(new List<Guitar> {
                new Guitar {GuitarId = 1, Name = "Guitar1", Type = "Type1"}
            }.AsQueryable());

            Cart cart = new Cart();
            CartController controller = new CartController(mock.Object);

            controller.AddToCart(cart, 1, null);

            Assert.AreEqual(cart.Lines.Count(), 1);
            Assert.AreEqual(cart.Lines.ToList()[0].Guitar.GuitarId, 1);
        }
    }
}
