using Extensions;
using STAR.Data;
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

        
        public ActionResult Index(int[] selectedSkillIds) {
            if (selectedSkillIds != null) {
                var convertedIds = selectedSkillIds.ToList();
                ViewBag.ConvertedIds = convertedIds;

                //given a list of skills return all contractors with those skills
                using (context) {
                    context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                    //Query 
                    var contractors = context.Contractors
                                  .Include(c => c.Skills)
                                  .Where(c => c.Skills.Select(s => s.SkillId)
                                        .Intersect(selectedSkillIds).Count() == selectedSkillIds.Count()).ToList();
                    //Put the selected skills in the front of the list
                    foreach (var person in contractors) {
                        person.MoveToFront(convertedIds);
                    }
                    return PartialView("ContractorListPartial", contractors);
                }
            }
            return View();
        }

        public ActionResult Details() {
            return View();
        }
    }
}