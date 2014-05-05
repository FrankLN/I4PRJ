using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AjaxControlToolkit;
using Microsoft.AspNet.Identity;

namespace WebApplication.Controllers
{
    [Authorize]
    public class UploadJobController : Controller
    {
        
        //
        // GET: /UploadJob/
        
        public ActionResult UploadJob()
        {
            
                return View();

            }
         
        }
    }
