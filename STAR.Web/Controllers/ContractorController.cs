using STAR.Data;
using STAR.Domain;
using STAR.Web.Models;
using System;
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
            return View(context.Contractors.ToList());
        }

        //autopopulate Details
        public ActionResult Details(int? id) {
            if (!id.HasValue) {
                return View();
            }

            var contractor = context.Contractors.Include(c => c.Skills).Where(c => c.ID == id).FirstOrDefault();
            return View(contractor);
        }

        //Update/Save Details
        [HttpPost]
        public ActionResult Details(PostContractorViewModel contractor) {
            var selectedSkills = contractor.SkillIds?.Split(',').Select(s => Convert.ToInt32(s)) ?? new int[] { };

            var skills = (from s in context.Skills
                         where selectedSkills.Contains(s.SkillId)
                         select s).ToList();

            if (contractor.Id == 0) {
                context.Contractors.Add(new Contractor {
                    FirstName = contractor.FirstName,
                    LastName = contractor.LastName,
                    Skills = skills
                });

                context.SaveChanges();

                return View();
            }
            else {
                var updatedContractor = context.Contractors
                    .Include(c => c.Skills).FirstOrDefault(c => c.ID == contractor.Id);
                updatedContractor.FirstName = contractor.FirstName;
                updatedContractor.LastName = contractor.LastName;
                updatedContractor.Skills = skills;

                context.SaveChanges();

                return View(updatedContractor);
            }
        }

        [HttpGet]
        public ActionResult Delete(int? id) {
            if (id == null) {
                return View();
            }
            Contractor contractor = context.Contractors.Find(id);
            if (contractor == null) {
                return HttpNotFound();
            }
            return View(contractor);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id) {
            Contractor contractor = context.Contractors.Find(id);
            context.Contractors.Remove(contractor);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}