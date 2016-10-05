﻿using System;
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

    }
}
