using STAR.Data;
using STAR.Domain;
using System;
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

        public ActionResult Details(int? id) {
            if (!id.HasValue)
            {
                return View();
            }

            return View();
            //go to the db and find the contractor with id = id.Value
            //return View(theFoundContractor);
        }

        [HttpPost]
        public ActionResult Details(Contractor contractor)
        {
            return View();
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