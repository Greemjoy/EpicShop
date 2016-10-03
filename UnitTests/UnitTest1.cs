using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Domain.Abstract;
using Domain.Entities;
using WebUI.Controllers;
using System.Collections.Generic;
using System.Linq;
namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            Mock<IGuitarRepository> mock = new Mock<IGuitarRepository>();
            mock.Setup(m => m.Guitars).Returns(new List<Guitar>
            {
                new Guitar {GuitarId = 1,Name = "Guitar1" },
                new Guitar {GuitarId = 2,Name = "Guitar2" },
                new Guitar {GuitarId = 3,Name = "Guitar3" },
                new Guitar {GuitarId = 4,Name = "Guitar4" },
                new Guitar {GuitarId = 5,Name = "Guitar5" },
            });

            GuitarsController controller = new GuitarsController(mock.Object);
            controller.pageSize = 3;

            IEnumerable<Guitar> result = (IEnumerable<Guitar>)controller.List(2).Model;
            List<Guitar> guitars = result.ToList();
            Assert.IsTrue(guitars.Count == 2);
            Assert.AreEqual(guitars[0].Name, "Guitar4");
            Assert.AreEqual(guitars[1].Name, "Guitar5");
        }
    }
}
