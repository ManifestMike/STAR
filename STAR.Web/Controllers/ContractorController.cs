﻿using STAR.Data;
using STAR.Domain;
using System;
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

        public ActionResult Index()
        {
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
        }

        [HttpPost]
        public ActionResult Details(Contractor contractor)
        {
            /*
            if (Nocontractor)
            {
                context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                var contractors = context.Contractors.Include(c => c.Skills).Where(c => c.FirstName == contractor.FirstName).Where(c => c.LastName == contractor.LastName).FirstOrDefault();

                if (context.Contractors.Any(x => x.FirstName == contractor.FirstName && x.LastName == contractor.LastName))
                {
                    return View();
                }
                context.Contractors.Add(new Domain.Contractor

                { FirstName = contractor.FirstName, LastName = contractor.LastName });
            }*/
            Contractor updatedContractor = context.Contractors.Where(c => c.ID == contractor.ID).FirstOrDefault();
            updatedContractor.FirstName = contractor.FirstName;
            updatedContractor.LastName = contractor.LastName;
            //updatedContractor.Skills.....


            context.SaveChanges();


            return View(updatedContractor);
        }

        /*
        public ActionResult ContractorExists()
        {
            return View();
        }*/


        public ActionResult Search()
        {

            return View(context.Contractors.ToList());
        }

        public JsonResult GetSkills(string term)
        {
            List<string> skills;

            skills = context.Skills.Where(x => x.Name.StartsWith(term))
                .Select(y => y.Name).ToList();

            return Json(skills, JsonRequestBehavior.AllowGet);
        }
    }
}