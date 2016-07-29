using Extensions;
using STAR.Data;
using STAR.Domain;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace STAR.Web.Controllers
{
    public class ContractorController : Controller
    {
        private StarContext context;

        public ContractorController(DbContext context)
        {
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
            return View(context.Contractors.ToList());
        }

        public ActionResult Details(int? id)
        {
            /*Contractor contractor;
            contractor.ID = 0;*/
            if (!id.HasValue)
            {
                return View();
            }
            context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            var contractor = context.Contractors.Where(c => c.ID == id).FirstOrDefault();

            return View(contractor);
            //go to the db and find the contractor with id = id.Value
            //return View(theFoundContractor);
        }

        [HttpPost]
        public ActionResult Details(Contractor contractor)
        {
            context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            var contractors = context.Contractors.Include(c => c.Skills).Where(c => c.FirstName == contractor.FirstName).Where(c => c.LastName == contractor.LastName).FirstOrDefault();


            if (context.Contractors.Any(x => x.FirstName == contractor.FirstName && x.LastName==contractor.LastName))
            {
                return View();
            }
            context.Contractors.Add(new Domain.Contractor

            { FirstName = contractor.FirstName, LastName = contractor.LastName });

            context.SaveChanges();


            return View(contractors);
        }
    }
}