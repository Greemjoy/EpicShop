using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Domain.Abstract;
using Domain.Entities;
using WebUI.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebUI.Models;
using WebUI.HtmlHelpers;

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

            GuitarsListViewModel result = (GuitarsListViewModel)controller.List(null,2).Model;
            List<Guitar> guitars = result.Guitars.ToList();
            Assert.IsTrue(guitars.Count == 2);
            Assert.AreEqual(guitars[0].Name, "Guitar4");
            Assert.AreEqual(guitars[1].Name, "Guitar5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            HtmlHelper myHelper = null;
            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            Func<int, string> pageUrlDelegate = i => "Page" + i;

            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                          + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                          + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                          result.ToString());

        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
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

            GuitarsListViewModel result = (GuitarsListViewModel)controller.List(null, 2).Model;

            PagingInfo pagingInfo = result.PagingInfo;
            Assert.AreEqual(pagingInfo.CurrentPage, 2);
            Assert.AreEqual(pagingInfo.ItemsPerPage, 3);
            Assert.AreEqual(pagingInfo.TotalItems, 5);
            Assert.AreEqual(pagingInfo.TotalPages, 2);
        }

        [TestMethod]
        public void Can_Filter_Guitars()
        {
            Mock<IGuitarRepository> mock = new Mock<IGuitarRepository>();
            mock.Setup(m => m.Guitars).Returns(new List<Guitar>
            {
                new Guitar {GuitarId = 1,Name = "Guitar1", Type ="Type1" },
                new Guitar {GuitarId = 2,Name = "Guitar2", Type ="Type2" },
                new Guitar {GuitarId = 3,Name = "Guitar3", Type ="Type1" },
                new Guitar {GuitarId = 4,Name = "Guitar4", Type ="Type3" },
                new Guitar {GuitarId = 5,Name = "Guitar5", Type ="Type2" },
            });

            GuitarsController controller = new GuitarsController(mock.Object);
            controller.pageSize = 3;

            List<Guitar> result = ((GuitarsListViewModel)controller.List("Type2", 1).Model).Guitars.ToList();

            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].Name == "Guitar2" && result[0].Type == "Type2");
            Assert.IsTrue(result[1].Name == "Guitar5" && result[1].Type == "Type2");
        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            Mock<IGuitarRepository> mock = new Mock<IGuitarRepository>();
            mock.Setup(m => m.Guitars).Returns(new List<Guitar>
            {
                new Guitar {GuitarId = 1,Name = "Guitar1", Type ="Type1" },
                new Guitar {GuitarId = 2,Name = "Guitar2", Type ="Type2" },
                new Guitar {GuitarId = 3,Name = "Guitar3", Type ="Type1" },
                new Guitar {GuitarId = 4,Name = "Guitar4", Type ="Type3" },
                new Guitar {GuitarId = 5,Name = "Guitar5", Type ="Type2" },
            });

            NavController target = new NavController(mock.Object);

            List<string> result = ((IEnumerable<string>)target.Menu().Model).ToList();

            Assert.AreEqual(result.Count(), 3);
            Assert.AreEqual(result[0], "Type1");
            Assert.AreEqual(result[1], "Type2");
            Assert.AreEqual(result[2], "Type3");
        }

        public void Indicates_Selected_Type()
        {
            Mock<IGuitarRepository> mock = new Mock<IGuitarRepository>();
            mock.Setup(m => m.Guitars).Returns(new List<Guitar>
            {
                new Guitar {GuitarId = 1,Name = "Guitar1", Type ="Type1" },
                new Guitar {GuitarId = 2,Name = "Guitar2", Type ="Type2" },
                new Guitar {GuitarId = 3,Name = "Guitar3", Type ="Type1" },
                new Guitar {GuitarId = 4,Name = "Guitar4", Type ="Type3" },
                new Guitar {GuitarId = 5,Name = "Guitar5", Type ="Type2" },
            });

            NavController target = new NavController(mock.Object);

            string typeToSelect = "Type2";

            string result = target.Menu(typeToSelect).ViewBag.SelectedGenre;

            Assert.AreEqual(typeToSelect, result);
        }
   
        public void Generate_Type_Specific_Guitar_Count()
        {
            Mock<IGuitarRepository> mock = new Mock<IGuitarRepository>();
            mock.Setup(m => m.Guitars).Returns(new List<Guitar>
            {
                new Guitar {GuitarId = 1,Name = "Guitar1", Type ="Type1" },
                new Guitar {GuitarId = 2,Name = "Guitar2", Type ="Type2" },
                new Guitar {GuitarId = 3,Name = "Guitar3", Type ="Type1" },
                new Guitar {GuitarId = 4,Name = "Guitar4", Type ="Type3" },
                new Guitar {GuitarId = 5,Name = "Guitar5", Type ="Type2" },
            });

            GuitarsController controller = new GuitarsController(mock.Object);
            controller.pageSize = 3;

            int res1 = ((GuitarsListViewModel)controller.List("Type1").Model).PagingInfo.TotalItems;
            int res2 = ((GuitarsListViewModel)controller.List("Type2").Model).PagingInfo.TotalItems;
            int res3 = ((GuitarsListViewModel)controller.List("Type3").Model).PagingInfo.TotalItems;
            int resAll = ((GuitarsListViewModel)controller.List(null).Model).PagingInfo.TotalItems;

            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }

    }
}
