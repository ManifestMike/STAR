using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STAR.Web.Models;
using STAR.Data;
using System.Data.Entity;

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
                if (context.Skills.Any(x => x.Name == model.SkillName))
                {
                    ModelState.AddModelError("Name", $"There is already a skill with the name {model.SkillName}.");
                    return View(model);
                }

                context.Skills.Add(new Domain.Skill
                { Name = model.SkillName, Description = model.Description });

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
            return View(); //todo pass in skill
        }

        public ActionResult Index()
        {
            return GetIndexView();
        }

        private ActionResult GetIndexView()
        {
            return View("Index", context.Skills.ToList());

        }

     
    }
}