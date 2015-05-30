using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpajamHonsen.Controllers
{
    public class WebViewController : Controller
    {
        // GET: WebView
        public ActionResult Index()
        {
            return View();
        }   
    }
}