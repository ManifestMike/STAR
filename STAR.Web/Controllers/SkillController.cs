﻿using STAR.Data;
using STAR.Domain;
using STAR.Web.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace STAR.Web.Controllers
{
    public class SkillController : Controller
    {
        private StarContext context;

        public SkillController(DbContext context)
        {
            this.context = context as StarContext;
        }

        [HttpPost]
        public ActionResult Details(SkillModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ID == 0)
                {
                    if (context.Skills.Any(x => x.Name == model.SkillName))
                    {
                        ModelState.AddModelError("Name", $"There is already a skill with the name {model.SkillName}.");
                        return View(model);
                    }
                    else
                    {
                        context.Skills.Add(new Domain.Skill { Name = model.SkillName, Description = model.Description });
                        context.SaveChanges();
                        return GetIndexView();
                    }
                }

                Skill updatedSkill = context.Skills.Where(c => c.Name == model.SkillName).FirstOrDefault();
                if (context.Skills.Any(x => x.Name != model.SkillName))
                {
                    ModelState.AddModelError("Name", $"There is not already a skill with the name {model.SkillName}.");
                    return View(model);
                }
                else
                {
                    updatedSkill.Name = model.SkillName;
                    updatedSkill.Description = model.Description;

                    context.SaveChanges();
                    return GetIndexView();
                }
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
            return View();
        }

        public ActionResult Index()
        {
            return GetIndexView();
        }

        public ActionResult Search(string term)
        {
            var skills = context.Skills.Select(y => new { name = y.Name, id = y.SkillId });

            if (!string.IsNullOrEmpty(term))
            {
                skills = skills.Where(x => x.name.StartsWith(term));
            }

            return Json(skills, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return View();
            }
            Skill skill = context.Skills.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Skill skill = context.Skills.Find(id);
            context.Skills.Remove(skill);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        private ActionResult GetIndexView()
        {
            return View("Index", context.Skills.ToList());
        }
    }
}