using STAR.Data;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace STAR.Web.Controllers {
    public class HomeController : Controller {
        private StarContext context;

        public HomeController(DbContext context) {
            this.context = context as StarContext;
        }

        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string searchTerm) {
            ViewBag.SearchTerm = searchTerm;
            //given a skill return all contractors with that skill
            using (context) {
                context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                var contractors = context.Contractors
                              .Include(c => c.Skills)
                              .Where(c => c.Skills.Any(s => s.Name == searchTerm)).ToList();

                return View(contractors);
            }
        }
    }
}