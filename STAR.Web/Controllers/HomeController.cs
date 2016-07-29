using System.Web.Mvc;

namespace STAR.Web.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }
    }
}