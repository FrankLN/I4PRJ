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
            var result = new HomeController();
            var r = result.Index() as ViewResult;
            Assert.AreEqual("Index", r.ViewName);
        }

        [Test]
        public void HomeController_About_ReturnIndexView()
        {
            var result = new HomeController();
            var r = result.About() as ViewResult;
            Assert.AreEqual("About", r.ViewName);
        }

        [Test]
        public void HomeController_Contact_ReturnIndexView()
        {
            var result = new HomeController();
            var r = result.Contact() as ViewResult;
            Assert.AreEqual("Contact", r.ViewName);
        }

        [Test]
        public void HomeController_About_ViewbagContainsDescription()
        {
            var result = new HomeController();
            var r = result.About() as ViewResult;
            Assert.AreEqual("Description", r.ViewData["Message"]);
        }

        [Test]
        public void HomeController_Contact_ViewbagMessageP1ContainsALongText()
        {
            string actual = "Visit our help pages for support and answers to commonly asked questions.";
            var result = new HomeController();
            var r = result.Contact() as ViewResult;
            Assert.AreEqual(actual, r.ViewData["MessageP1"]);
        }

        [Test]
        public void HomeController_Contact_ViewbagMessageP2ContainsALongText()
        {
            string actual = "Use the email link below to contact us for related enquiries.";
            var result = new HomeController();
            var r = result.Contact() as ViewResult;
            Assert.AreEqual(actual, r.ViewData["MessageP2"]);
        }
       
    }
}
