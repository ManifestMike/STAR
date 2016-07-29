using STAR.Data;
using STAR.Web.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace STAR.Web.Controllers {
    public class SkillController : Controller {
        private StarContext context;

        public SkillController(DbContext context) {
            this.context = context as StarContext;
        }

        [HttpPost]
        public ActionResult Details(SkillModel model) {
            if (ModelState.IsValid) {
                if (context.Skills.Any(x => x.Name == model.SkillName)) {
                    ModelState.AddModelError("Name", $"There is already a skill with the name {model.SkillName}.");
                    return View(model);
                }

                context.Skills.Add(new Domain.Skill { Name = model.SkillName, Description = model.Description });
                context.SaveChanges();

                return GetIndexView();
            }

            return View(model);
        }
        
        [HttpGet]
        public ActionResult Details(int? Id) {
            if (!Id.HasValue) {
                return View();
            }
            return View(); //todo pass in skill
        }

        public ActionResult Index(string term) {
            if (!string.IsNullOrEmpty(term)) {
                var skills = context.Skills.Where(x => x.Name.StartsWith(term))
                    .Select(y => y.Name).ToList();

                return Json(skills, JsonRequestBehavior.AllowGet);
            }

            return GetIndexView();
        }

        private ActionResult GetIndexView() {
            return View("Index", context.Skills.ToList());

        }
    }
}