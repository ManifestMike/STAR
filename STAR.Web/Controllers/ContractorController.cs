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

        [HttpPost]
        public ActionResult Details(string searchTerm)
        {
            //query
            ViewBag.SearchTerm = searchTerm;

            using (context)
            {
                context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                var contractors = context.Contractors
                              .Include(c => c.Skills)
                              .Where(c => c.Skills.Any(s => s.Name == searchTerm)).ToList();

                return View(contractors);
            }
        }


        public ActionResult Search() {

            return View(context.Contractors.ToList());
        }

        public JsonResult GetSkills(string term)
        {
            List<string> skills;

            skills = context.Skills.Where(x => x.Name.StartsWith(term))
                .Select(y => y.Name).ToList();

            return Json(skills, JsonRequestBehavior.AllowGet);
        }
    }
}