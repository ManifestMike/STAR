using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using STAR.Data;

namespace STAR.Web.Controllers {
    public class HomeController : Controller {
        private StarContext context;

        public HomeController(DbContext context) {
            this.context = context as StarContext;
        }


        // GET: Home
        public ActionResult Index() {
            return View();
        }
    }
}