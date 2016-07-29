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

        //autopopulate Details
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return View();
            }
            context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            var contractor = context.Contractors.Include(c => c.Skills).Where(c => c.ID == id).FirstOrDefault();
            
            return View(contractor);
        }

        //Update/Save Details
        [HttpPost]
        public ActionResult Details(Contractor contractor)
        {
            if (contractor.ID==0)
            {
                context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                var contractors = context.Contractors.Include(c => c.Skills).Where(c => c.FirstName == contractor.FirstName).Where(c => c.LastName == contractor.LastName).FirstOrDefault();

                if (context.Contractors.Any(x => x.FirstName == contractor.FirstName && x.LastName == contractor.LastName))
                {
                    return View();
                }
                context.Contractors.Add(new Domain.Contractor
                { FirstName = contractor.FirstName, LastName = contractor.LastName });

                context.SaveChanges();

                return View();
            }
            else
            {
                Contractor updatedContractor = context.Contractors.Where(c => c.ID == contractor.ID).FirstOrDefault();
                updatedContractor.FirstName = contractor.FirstName;
                updatedContractor.LastName = contractor.LastName;
                //updatedContractor.Skills =


                context.SaveChanges();

                return View(updatedContractor);
            }

        }


        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View();
            }
            Contractor contractor = context.Contractors.Find(id);
            if (contractor == null)
            {
                return HttpNotFound();
            }
            return View(contractor);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Contractor contractor = context.Contractors.Find(id);
            context.Contractors.Remove(contractor);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}