
using Microsoft.AspNetCore.Mvc;
using FlightProjClient.Models;
// using Microsoft.EntityFrameworkCore;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace FlightProjClient.Controllers{

    public class AdminHomeController : Controller
    {   
        // private readonly Ace52024Context db;

        // private readonly ISession session;

        // public AdminHomeController(IHttpContextAccessor httpContextAccessor){
        //     session = httpContextAccessor.HttpContext.Session;
        // }

        string Baseurl = "http://localhost:5021/";

        public ActionResult AdminHomePage(){
            string usName = HttpContext.Session.GetString("UserName");
            if(usName!=null){
                return View();
            }
            else{
                return RedirectToAction("Login", "Login");
            }
        }

        public static List<Kgflight> acc = new List<Kgflight>();
        public static List<Kguser> users = new List<Kguser>();

        public async Task<ActionResult> GetAllUsers(){
            string usName = HttpContext.Session.GetString("UserName");
            if(usName!=null){
                using (var client = new HttpClient()){

                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("http://localhost:5021/api/User");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api  
                        var FlightResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        users = JsonConvert.DeserializeObject<List<Kguser>>(FlightResponse);
                        users = users.Select(x=>x).Where(x=>x.UserType==null).ToList();
                    }
                    return RedirectToAction("ViewCustomers", "AdminHome");
                }
            }
            else{
                return RedirectToAction("Login", "Login");
            }
        }
        
        public async Task<ActionResult> RetrieveFlights(){
            string usName = HttpContext.Session.GetString("UserName");
            if(usName!=null){
                using (var client = new HttpClient()){

                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("http://localhost:5021/api/Flight");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api  
                        var FlightResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        acc = JsonConvert.DeserializeObject<List<Kgflight>>(FlightResponse);

                    }
                    return RedirectToAction("ViewFlights", "AdminHome");
                }
            }
            return RedirectToAction("Login", "Login");
        }
        public async Task<ActionResult> ViewFlights()
        {
            string usName = HttpContext.Session.GetString("UserName");
            if(usName!=null){
                return View(acc);
            }
            return RedirectToAction("Login", "Login");
        }

        public IActionResult AddFlight()
        {
            string usName = HttpContext.Session.GetString("UserName");
            if(usName!=null){
                var sourceC = acc.Select(x=>x.Fsource).Distinct().ToList();
                ViewBag.SourceCitys =new SelectList(sourceC);
                var destC = acc.Select(x=>x.Fdest).Distinct().ToList();
                ViewBag.dest =new SelectList(destC);
                return View();
            }
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public async Task<IActionResult> AddFlight(Kgflight e)
        {
            string usName = HttpContext.Session.GetString("UserName");
            if(usName!=null){
                using (var client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(e),
                Encoding.UTF8, "application/json");

                    using (var response = await client.PostAsync("http://localhost:5021/api/Flight", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        e = JsonConvert.DeserializeObject<Kgflight>(apiResponse);
                    }
                }
                return RedirectToAction("RetrieveFlights");
            }
            return RedirectToAction("Login", "Login");
        }

        //Edit flight
        [HttpGet]
        public async Task<ActionResult> EditFlight(int id){
            string usName = HttpContext.Session.GetString("UserName");
            if(usName!=null){
                Kgflight fl = new Kgflight();
                TempData["FlightId"] = id;
                using (var client = new HttpClient()){
                    using (var response = await client.GetAsync("http://localhost:5021/api/Flight/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        fl = JsonConvert.DeserializeObject<Kgflight>(apiResponse);
                    }
                }
                return View(fl);
            }
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public async Task<ActionResult> EditFlight(Kgflight e){
            string usName = HttpContext.Session.GetString("UserName");
            if(usName!=null){
                Kgflight recvdfl = new Kgflight();
                // Console.WriteLine($"{e.Frate}");
                using (var client = new HttpClient()){
                    int id = Convert.ToInt32(TempData["FlightId"]);
                    e.Fid = id;
                    StringContent content1 = new StringContent(JsonConvert.SerializeObject(e)
                    , Encoding.UTF8, "application/json");
                    Console.WriteLine(id);
                    using (var response = await client.PutAsync("http://localhost:5021/api/Flight/" + id, content1))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ViewBag.Result = "Success";
                        recvdfl = JsonConvert.DeserializeObject<Kgflight>(apiResponse);
                    }
                }
                return RedirectToAction("RetrieveFlights");
            }
            return RedirectToAction("Login", "Login");
        }
    
        public static List<Kgbooking> bookings = new List<Kgbooking>();

        public async Task<ActionResult> RetrieveBookings(Kgbooking e){
            string usName = HttpContext.Session.GetString("UserName");
            if(usName!=null){
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("http://localhost:5021/api/Booking");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api  
                        var BookingResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        bookings = JsonConvert.DeserializeObject<List<Kgbooking>>(BookingResponse);

                    }
                    return RedirectToAction("ShowBookings", "AdminHome");
                }
            }
            return RedirectToAction("Login", "Login");
        }

        public async Task<ActionResult> ShowBookings(Kgbooking e){
            string usName = HttpContext.Session.GetString("UserName");
            if(usName!=null){
                return View(bookings);
            }
            return RedirectToAction("Login", "Login");
        }
        
        public async Task<ActionResult> BookingDetails(int id){
            string usName = HttpContext.Session.GetString("UserName");
            if(usName!=null){
                Kgflight fl = new Kgflight();
                using (var client = new HttpClient()){
                    using (var response = await client.GetAsync("http://localhost:5021/api/Flight/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        fl = JsonConvert.DeserializeObject<Kgflight>(apiResponse);
                    }
                }
                return View(fl);
            }
            return RedirectToAction("Login", "Login");
        }

        //Delete booking
        [HttpGet]
        public async Task<ActionResult> AdminDeleteBooking(int id)
        {
            string usName = HttpContext.Session.GetString("UserName");
            if(usName!=null){
                TempData["Bid"] = id;
                Kgbooking e = new Kgbooking();
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync("http://localhost:5021/api/Booking/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        e = JsonConvert.DeserializeObject<Kgbooking>(apiResponse);
                    }
                }
                return View(e);
            }
            return RedirectToAction("Login", "Login");
        }


        [HttpPost]
        public async Task<ActionResult> AdminDeleteBooking(Kgbooking e)
        {
            string usName = HttpContext.Session.GetString("UserName");
            if(usName!=null){
                int Bookingid = Convert.ToInt32(TempData["Bid"]);
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.DeleteAsync("http://localhost:5021/api/Booking/" + Bookingid))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                    }
                }

                return RedirectToAction("RetrieveBookings");
            }
            return RedirectToAction("Login", "Login");
        }

        //View Customers

        public async Task<ActionResult> ViewCustomers(Kguser e){
            string usName = HttpContext.Session.GetString("UserName");
            if(usName!=null){
                return View(users);
            }
            return RedirectToAction("Login", "Login");
        }

        //Edit user
        [HttpGet]
        public async Task<ActionResult> EditUser(int id){
            string usName = HttpContext.Session.GetString("UserName");
            if(usName!=null){
                Kguser us = new Kguser();
                TempData["userId"] = id;
                using (var client = new HttpClient()){
                    using (var response = await client.GetAsync("http://localhost:5021/api/User/" + id))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        us = JsonConvert.DeserializeObject<Kguser>(apiResponse);
                    }
                }
                return View(us);
            }
            return RedirectToAction("Login", "Login");
        }

        [HttpPost]
        public async Task<ActionResult> EditUser(Kguser e){
            string usName = HttpContext.Session.GetString("UserName");
            if(usName!=null){
                Kguser recvdus = new Kguser();
                using (var client = new HttpClient()){
                    int id = Convert.ToInt32(TempData["userId"]);
                    e.Uid = id;
                    Console.WriteLine(id);
                    StringContent content1 = new StringContent(JsonConvert.SerializeObject(e)
                    , Encoding.UTF8, "application/json");
                    Console.WriteLine(id);
                    using (var response = await client.PutAsync("http://localhost:5021/api/User/" + id, content1))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        ViewBag.Result = "Success";
                        recvdus = JsonConvert.DeserializeObject<Kguser>(apiResponse);
                    }
                }
                return RedirectToAction("GetAllUsers");
            }
            return RedirectToAction("Login", "Login");
        }
    }
    
}



