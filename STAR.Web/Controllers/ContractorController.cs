using STAR.Data;
using STAR.Domain;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace STAR.Web.Controllers {
    public class ContractorController : Controller {
        private StarContext context;

        public ContractorController(DbContext context) {
            this.context = context as StarContext;
        }

        public ActionResult Index() {
            return View(context.Contractors.ToList());
        }

        public ActionResult Details() {
            return View();
        }

        public ActionResult Search() {

            return View(context.Contractors.ToList());
        }
    }
}