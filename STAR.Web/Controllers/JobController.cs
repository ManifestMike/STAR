using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using STAR.Data;

namespace STAR.Web.Controllers
{
    public class JobController : Controller {
        private StarContext context;

        public JobController(DbContext context) {
            this.context = context as StarContext;
        }
        // GET: Job
        public ActionResult Index()
        {
            return View(context.Jobs);
        }
    }
}