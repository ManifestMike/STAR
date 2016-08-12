using STAR.Data;
using STAR.Domain;
using STAR.Web.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using Extensions;

namespace STAR.Web.Controllers {
    public class ContractorController : Controller {
        private StarContext context;

        public ContractorController(DbContext context) {
            this.context = context as StarContext;
        }

        private List<Contractor> SearchBySkills(int[] selectedSkillIds) {
            var convertedIds = selectedSkillIds.ToList();
            ViewBag.ConvertedIds = convertedIds;
            //given a list of skills return all contractors with those skills
            using (context) {
                var contractors = context.Contractors
                              .Include(c => c.Skills)
                              .Where(c => c.Skills.Select(s => s.SkillId)
                                    .Intersect(selectedSkillIds).Count() == selectedSkillIds.Count()).ToList();

                //Put the selected skills in the front of the list
                MoveSkillsToFront(contractors, convertedIds);
                return contractors;
            }
        }

        public void MoveSkillsToFront(List<Contractor> contractors, List<int> skillIds) {
            MoveSkillsToFrontHelper(contractors, skillIds);
        }
        private void MoveSkillsToFrontHelper(List<Contractor> contractors, List<int> skillIds) {
            foreach (var person in contractors) {
                person.MoveToFront(skillIds);
            }
        }

        private List<Contractor> getContractorList() {
            return context.Contractors.ToList();
        }

        public ActionResult Index(int[] selectedSkillIds) {
            if (selectedSkillIds != null) {
                var contractors = SearchBySkills(selectedSkillIds);
                return PartialView("ContractorListPartial", contractors);
            }
            //return View(getContractorList());
            return GetIndexView();
        }

        //autopopulate Details
        public ActionResult Details(int? id) {
            if (!id.HasValue) {
                return View(new PostContractorViewModel());
            }
            
            var contractor = context.Contractors.Include(c => c.Skills).Where(c => c.ID == id).FirstOrDefault();
            ViewBag.skillnames = contractor.Skills.Select(s => s.Name).Delimit();

            return View(new PostContractorViewModel
            {
                FirstName = contractor.FirstName,
                LastName = contractor.LastName,
                Id = contractor.ID,
                SkillIds = contractor.Skills.Select(s => s.SkillId).Delimit()
            });
        }
        
        //Update/Save Details
        [HttpPost]
        public ActionResult Details(PostContractorViewModel contractor) {
            if (ModelState.IsValid) {
                var skills = matchSelectedSkillsToSkills(contractor.SkillIds);
                
                if (contractor.Id == 0) {
                    addNewContractor(contractor, skills); 
                }
                else {
                    updateContractor(contractor, skills);
                }
                context.SaveChanges();
                //return View("Index", getContractorList());
                return GetIndexView();
            }

            return View(contractor);
        }

        private IEnumerable<int> splitSelectedSkills(string SkillIds) {
            return SkillIds?.Split(',').Select(s => Convert.ToInt32(s)) ?? new int[] { };
        }

        public List<Skill> matchSelectedSkillsToSkills(string SkillIds) {
            var selectedSkills = splitSelectedSkills(SkillIds);

            return (from s in context.Skills
                    where selectedSkills.Contains(s.SkillId)
                    select s).ToList();
        }

        public void addNewContractor(PostContractorViewModel contractor, List<Skill> skills) {
            context.Contractors.Add(new Contractor {
                FirstName = contractor.FirstName,
                LastName = contractor.LastName,
                Skills = skills
            });
        }

        public void updateContractor(PostContractorViewModel contractor, List<Skill> skills) {
            var updatedContractor = context.Contractors
                         .Include(c => c.Skills).FirstOrDefault(c => c.ID == contractor.Id);
            updatedContractor.FirstName = contractor.FirstName;
            updatedContractor.LastName = contractor.LastName;
            updatedContractor.Skills = skills;
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

        
        public ActionResult AvailableContractors(int positionId) {
            var availableContractors = getAvailableContractors();
            ViewBag.positionId = positionId;
        
            return View(availableContractors);
        }
        private IEnumerable<Contractor> getAvailableContractors() {
            var positions = context.Positions;
            return context.Contractors
                .Where(c => !positions
                    .Select(p => p.contractorId)
                    .Contains(c.ID));
        }

        public ActionResult AssignContractorToPosition(int positionId, int contractorId) {
            var position = context.Positions.Where(x => x.PositionId == positionId).FirstOrDefault();
            position.contractorId = contractorId;
            return View("Index", getAvailableContractors());
        }



        private ActionResult GetIndexView()
        {
            RouteData.Values.Remove("id");
            RouteData.Values.Remove("action");
            return View("Index", getContractorList());
        }
    }
}