using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using STAR.Data;
using Extensions;

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
            
            ViewBag.SearchTerm = searchTerm;
            //given a skill return all contractors with that skill
            using (context) {
                context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                var contractors = context.Contractors
                              .Include(c => c.Skills)
                              .Where(c => c.Skills.Any(s => s.Name == searchTerm)).ToList();

                foreach (var person in contractors) {
                    var skillList = person.Skills.ToList();
                    int index = skillList.FindIndex(x => x.Name == searchTerm);
                    skillList.MoveToFront(index);
                    person.Skills = skillList; 
                }

                return View(contractors);
            }
        }
        //@Pre: term is retrieved from autocomplete source 
        //@Post: Returns json of names of the skills that start with that term
        public JsonResult GetSkills() {
           
            var skills = context.Skills.Select(y => y.Name).ToList();

            return Json(skills, JsonRequestBehavior.AllowGet);
        }
    }
}