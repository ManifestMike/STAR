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
        [HttpPost]
        public ActionResult Index(string searchTerm) {
            //query
            ViewBag.SearchTerm = searchTerm;

            using (context) {
                context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                var contractors = context.Contractors
                              .Include(c => c.Skills)
                              .Where(c => c.Skills.Any(s => s.Name == searchTerm)).ToList();

                return View(contractors);
            }
        }
        //@Pre: term 
        //@Post:
        public JsonResult GetSkills(string term) {
            List<string> skills;
             
            skills = context.Skills.Where(x => x.Name.StartsWith(term))
                .Select(y => y.Name).ToList();

            return Json(skills, JsonRequestBehavior.AllowGet);
        }
    }
}