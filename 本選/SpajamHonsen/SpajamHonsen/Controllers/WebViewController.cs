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

        // GET: WebView/WebView1
        public ActionResult WebView1()
        {
            return View();
        }

        // GET: WebView/PushTestView
        public ActionResult PushTestView()
        {
            return View();
        }

        // GET: WebView/TweetView
        public ActionResult TweetView()
        {
            return View();
        }

        // GET: WebView/DetailView
        public ActionResult DetailView()
        {
            return View();
        }   
    }
}