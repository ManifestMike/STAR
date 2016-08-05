using STAR.Data;
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
                if (context.Skills.Any(x => x.Name == model.SkillName))
                {
                    ModelState.AddModelError("Name", $"There is already a skill with the name {model.SkillName}.");
                    return View(model);
                }

                if (model.ID == 0)
                {
                    context.Skills.Add(new Skill { Name = model.SkillName, Description = model.Description });
                }

                else
                {
                    Skill updatedSkill = context.Skills.Where(c => c.SkillId == model.ID).FirstOrDefault();
                    updatedSkill.Name = model.SkillName;
                    updatedSkill.Description = model.Description;
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
            Skill skill = context.Skills.Where(c => c.SkillId == Id).FirstOrDefault();
            SkillModel model = new SkillModel();
            model.SkillName = skill.Name;
            model.Description = skill.Description;
            return View(model);
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
            RouteData.Values.Remove("id");
            RouteData.Values.Remove("action");
            return View("Index", context.Skills.ToList());
        }
    }
}