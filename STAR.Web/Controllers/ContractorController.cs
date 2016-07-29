using STAR.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace STAR.Web.Controllers {
    public class ContractorController : Controller {
        private StarContext context;

        public ContractorController(DbContext context) {
            this.context = context as StarContext;
        }

        public ActionResult Index(string searchTerm) {
            if (!string.IsNullOrEmpty(searchTerm)) {
                ViewBag.SearchTerm = searchTerm;
                //given a skill return all contractors with that skill
                using (context) {
                    context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                    var contractors = context.Contractors
                                  .Include(c => c.Skills)
                                  .Where(c => c.Skills.Any(s => s.Name == searchTerm)).ToList();

                    return PartialView("ContractorListPartial", contractors);
                }
            }

            return View(context.Contractors.ToList());
        }

        public ActionResult Details() {
            return View();
        }
    }
}