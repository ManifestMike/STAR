using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using STAR.Web.Models;
using STAR.Data;
using STAR.Domain;

namespace STAR.Web.Controllers {
    public class AccountController : Controller {
        // GET: Login
        public ActionResult Index() {
            return View();
        }

        public ActionResult Register() {
            return View();
        }

        [HttpPost]
        public ActionResult Register(LoginModel account) {
            if (ModelState.IsValid) {
                using (StarContext db = new StarContext()) {
                    Login newAccount = new Login {FirstName = account.FirstName, LastName = account.LastName, Email = account.Email, Username = account.Username, Password = account.Password, PasswordConfirm = account.ConfirmPassword };
                    db.Logins.Add(newAccount);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.message = account.FirstName + " " + account.LastName + " registered successfully!";
            }
            return View();
        }
        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel user) {
            using (StarContext db = new StarContext()) {
                var usr = db.Logins.Where(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefault();
                if (usr != null) {
                    Session["UserId"] = usr.UserId.ToString();
                    Session["Username"] = usr.Username.ToString();
                    return RedirectToAction("LoggedIn");
                }
                else {
                    ModelState.AddModelError("", "Username or password is wrong.");
                }
            }
            return View();
        }
        public ActionResult LoggedIn() {
            if(Session["UserId"] != null) {
                return View();
            }
            else {
                return RedirectToAction("Login");
            }
        }
    }
}