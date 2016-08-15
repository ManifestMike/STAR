using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using STAR.Data;
using STAR.Web.Models;
using STAR.Domain;
using Extensions;

namespace STAR.Web.Controllers
{
    public class PositionController : Controller {
        private StarContext context;

        public PositionController(DbContext context) {
            this.context = context as StarContext;
        }

        [HttpPost]
        public ActionResult Details(PositionModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ID == 0)
                {
                    context.Positions.Add(new Position { Name = model.PositionName, Description = model.Description });
                }

                else
                {
                    Position updatedPosition = context.Positions.Where(c => c.PositionId == model.ID).FirstOrDefault();
                    updatedPosition.Name = model.PositionName;
                    updatedPosition.Description = model.Description;
                }
                context.SaveChanges();
                return GetIndexView();
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int? Id)
        {
            if (!Id.HasValue)
            {
                return View();
            }
            Position position = context.Positions.Where(c => c.PositionId == Id).FirstOrDefault();
            PositionModel model = new PositionModel();
            model.PositionName = position.Name;
            model.Description = position.Description;
            if(position.contractorId != null)
                model.contractor = getContractorInPosition(position.contractorId, Id);

            return View(model);
        }
        //This is an odd implementation please review
        private PostContractorViewModel getContractorInPosition(int? contractorID, int? positionID) {
            var contractor = context.Contractors
                                        .Where(c => c.ID == context.Positions
                                        .Where(p => p.contractorId == contractorID).FirstOrDefault().contractorId).FirstOrDefault();
            return new PostContractorViewModel {
                FirstName = contractor.FirstName,
                Id = contractor.ID,
                LastName = contractor.LastName,
                SkillIds = contractor.Skills.Select(s => s.SkillId).Delimit()
            };
        }
        public ActionResult Index()
        {
            return View(context.Positions);
        }

        public ActionResult Details()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View();
            }
            Position position = context.Positions.Find(id);
            if (position == null)
            {
                return HttpNotFound();
            }
            return View(position);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Position position = context.Positions.Find(id);
            context.Positions.Remove(position);
            context.SaveChanges();
            return RedirectToAction("Index");
        }


        private ActionResult GetIndexView()
        {
            RouteData.Values.Remove("id");
            RouteData.Values.Remove("action");
            return View("Index", context.Positions.ToList());
        }
    }
}