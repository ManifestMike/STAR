using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using STAR.Data;
using STAR.Web.Models;
using STAR.Domain;

namespace STAR.Web.Controllers
{
    public class JobController : Controller {
        private StarContext context;

        public JobController(DbContext context) {
            this.context = context as StarContext;
        }

        [HttpPost]
        public ActionResult Details(JobModel model)
        {
            if (ModelState.IsValid)
            {
                if (context.Jobs.Any(x => x.Name == model.JobName))
                {
                    ModelState.AddModelError("Name", $"There is already a skill with the name {model.JobName}.");
                    return View(model);
                }

                if (model.ID == 0)
                {
                    context.Jobs.Add(new Job { Name = model.JobName, Description = model.Description });
                }

                else
                {
                    Job updatedJob = context.Jobs.Where(c => c.JobId == model.ID).FirstOrDefault();
                    updatedJob.Name = model.JobName;
                    updatedJob.Description = model.Description;
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
            Job job = context.Jobs.Where(c => c.JobId == Id).FirstOrDefault();
            JobModel model = new JobModel();
            model.JobName = job.Name;
            model.Description = job.Description;
            return View(model);
        }
        public ActionResult Index()
        {
            return View(context.Jobs);
        }

        public ActionResult Details()
        {
            return View();
        }

        private ActionResult GetIndexView()
        {
            RouteData.Values.Remove("id");
            RouteData.Values.Remove("action");
            return View("Index", context.Jobs.ToList());
        }
    }
}