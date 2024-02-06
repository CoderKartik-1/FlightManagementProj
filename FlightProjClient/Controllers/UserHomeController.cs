using Microsoft.AspNetCore.Mvc;
using FlightProjClient.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FlightProj.Controllers;

public class UserHomeController : Controller
{   
    private readonly Ace52024Context db;

        public UserHomeController(Ace52024Context _db){
            db=_db;
        }

    public ActionResult UserHomePage(){
        string usName = HttpContext.Session.GetString("UserName");
        if(usName!=null){
            return View();
        }
        return RedirectToAction("Login", "Login");
    }


    public ActionResult SearchFlights(){
        string usName = HttpContext.Session.GetString("UserName");
        if(usName!=null){
            var sourceC = db.Kgflights.Select(x => x.Fsource).Distinct().ToList();
            ViewBag.SourceCities = new SelectList(sourceC);
            var destC = db.Kgflights.Select(x => x.Fdest).Distinct().ToList();
            ViewBag.DestCities = new SelectList(destC);
            return View();
        }
        else{
            return RedirectToAction("Login", "Login");
        }
        
    }
    
    [HttpPost]
    public ActionResult SearchFlights(Kgflight fl){
        string srcCity = fl.Fsource;
        string destCity = fl.Fdest;
        return RedirectToAction("ShowFlights", new{Fsource=srcCity, Fdest=destCity});
    }
    public ActionResult ShowFlights(string Fsource, string Fdest){
        string usName = HttpContext.Session.GetString("UserName");
        if(usName!=null){
            List<Kgflight> records = db.Kgflights.Select(x=>x).Where(x=>x.Fsource==Fsource && x.Fdest==Fdest).ToList();
            return View(records);
        }
        return RedirectToAction("Login", "Login");
    }

    public ActionResult BookTicket(int id){
        string usName = HttpContext.Session.GetString("UserName");
        if(usName!=null){
            HttpContext.Session.SetString("FlightId", id.ToString());
            return View();
        }
        return RedirectToAction("Login", "Login");
    }

    [HttpPost]
    public ActionResult BookTicket(Kgbooking bk, int id){
        string usName = HttpContext.Session.GetString("UserName");
        if(usName!=null){
            bk.BookingDate = DateTime.Now;
            bk.Fid = Int32.Parse(HttpContext.Session.GetString("FlightId"));
            bk.Uid = Int32.Parse(HttpContext.Session.GetString("CustomerId"));
            foreach(Kgflight f in db.Kgflights){
                if(f.Fid == bk.Fid){
                    bk.TotalCost = bk.NumPax*f.Frate;
                }
            }
            db.Kgbookings.Add(bk);
            db.SaveChanges();
            return RedirectToAction("ShowCurrentBooking", new{BookingId = bk.BookingId});
        }
        return RedirectToAction("Login", "Login");
    }

    public ActionResult ShowCurrentBooking(int BookingId){
        string usName = HttpContext.Session.GetString("UserName");
        if(usName!=null){
            Kgbooking booking = db.Kgbookings.Include(y=>y.UidNavigation).Include(z=>z.FidNavigation).Select(x=>x).Where(x=>x.BookingId==BookingId).SingleOrDefault();
            return View(booking);
        }
        return RedirectToAction("Login", "Login");
    }

    public ActionResult ShowMyBookings(){
        string usName = HttpContext.Session.GetString("UserName");
        if(usName!=null){
            int custId = Int32.Parse(HttpContext.Session.GetString("CustomerId"));
            List<Kgbooking> bs = db.Kgbookings.Include(x=>x.FidNavigation).Select(y=>y).Where(y=>y.Uid==custId).ToList();
            return View(bs);
        }
        return RedirectToAction("Login", "Login");
    }

    public ActionResult DeleteBooking(int id){
        string usName = HttpContext.Session.GetString("UserName");
        if(usName!=null){
            Kgbooking bk = db.Kgbookings.Select(x=>x).Where(x=>x.BookingId==id).SingleOrDefault(); 
            return View(bk); 
        }  
        return RedirectToAction("Login", "Login");
    }

    [HttpPost]
    [ActionName("DeleteBooking")]
    public ActionResult DeleteBookingConfirmed(int id){
        string usName = HttpContext.Session.GetString("UserName");
        if(usName!=null){
            Kgbooking bk = db.Kgbookings.Select(x=>x).Where(x=>x.BookingId==id).SingleOrDefault(); ;
            db.Kgbookings.Remove(bk);
            db.SaveChanges();
            return RedirectToAction("ShowMyBookings");
        }
        return RedirectToAction("Login", "Login");
    }

}