using FlightProjClient.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using Newtonsoft.Json;
using System.Text;

namespace FlightProjClient.Controllers{

    public class LoginController: Controller{
        private readonly Ace52024Context db;

        private readonly ISession session;
        

        public LoginController(Ace52024Context _db, IHttpContextAccessor httpContextAccessor){
            db=_db;
            session = httpContextAccessor.HttpContext.Session;
        }
        public IActionResult Login(){
            return View();
        }

        [HttpPost]
        public IActionResult Login(Kguser u){
            var result = (from i in db.Kgusers
                          where i.Email==u.Email && i.Password==u.Password && i.UserType=="Admin"
                          select i).SingleOrDefault();
            if(result!=null){
                HttpContext.Session.SetString("UserName", result.Uname);
                int customerId = result.Uid;
                HttpContext.Session.SetString("Uid", customerId.ToString());
                return RedirectToAction("AdminHomePage", "AdminHome");
            }
            var result1 = (from i in db.Kgusers
                           where i.Email==u.Email && i.Password==u.Password && i.UserType==null
                           select i).SingleOrDefault();
            if(result1!=null){
                HttpContext.Session.SetString("UserName", result1.Uname);
                int customerId = result1.Uid;
                HttpContext.Session.SetString("CustomerId", customerId.ToString());
                return RedirectToAction("UserHomePage", "UserHome");
            }
            
            return View();
        }

        public ActionResult ViewProfile(){
            Kguser? us = (from i in db.Kgusers
                         where i.Uid==Convert.ToInt32(HttpContext.Session.GetString("CustomerId"))
                         select i).SingleOrDefault();
            return View(us);
        }

        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Login");
        }

        public IActionResult Register(){
            return View();
        }

        [HttpPost]
        public IActionResult Register(Kguser u){
            if(ModelState.IsValid){
                db.Kgusers.Add(u);
                db.SaveChanges();
                return RedirectToAction("Login", "Login");
            }
            return RedirectToAction("Register","Login");
        }
    }
}