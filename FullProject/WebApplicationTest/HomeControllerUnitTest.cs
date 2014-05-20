using System;
using System.Web.Mvc;
using NUnit.Framework;
using WebApplication.Controllers;

namespace WebApplicationTest
{
    [TestFixture]
    class HomeControllerUnitTest
    {
        [Test]
        public void HomeController_Index_ReturnIndexView()
        {
            string expected = "Index";

            HomeController t = new HomeController();

            ActionResult a = t.Index();

            string actual = ((ViewResult) a).ViewName;



            Assert.AreEqual(expected, actual);
        }
    }
}
