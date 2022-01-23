using Flamingo_Airways.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlamingoLibrary;
using System.Globalization;

namespace Flamingo_Airways.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult AdminHome()
        {
            return View();
        }
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(LoginModel lm)
        {
            if (lm.Email == "admin@gmail.com" && lm.Pwd == "admin@123")
            {
                return View("AdminHome");
            }
            else
            {
                Response.Write("Enter valid credentials");
                return View(lm);
            }
        }
        public ActionResult Index()
        {
            AdminDAL adminDAL = new AdminDAL();
            List<Admin> admin = adminDAL.GetFlightDetails();
            List<AdminModel> am = new List<AdminModel>();

            foreach (var item in admin)
            {
                AdminModel model = new AdminModel();
                model.FlightNo = item.FlightNo;
                model.ArrivalCity = item.ArrivalCity;
                model.DepartureCity = item.DepartureCity;
                model.Arrivaltime = item.Arrivaltime;
                model.Departtime = item.Departtime;
                model.Seatingcap = item.Seatingcap;
                model.Amount=item.Amount;
                am.Add(model);

            }
            return View(am);
        }


        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(AdminModel am)
        {
            AdminDAL adminDAL = new AdminDAL();
            Admin admin = new Admin();
            admin.FlightNo = am.FlightNo;
            admin.ArrivalCity = am.ArrivalCity;
            admin.DepartureCity = am.DepartureCity;
           // DateTime dt = DateTime.ParseExact(am.Arrivaltime.ToString(),"HH:mm",CultureInfo.InvariantCulture);

            admin.Arrivaltime = TimeSpan.FromTicks(am.Arrivaltime.Ticks);
            admin.Departtime = TimeSpan.FromTicks(am.Departtime.Ticks);
            admin.Seatingcap = am.Seatingcap;
            admin.Amount = am.Amount;
            adminDAL.Add_FlightDetails(admin);

            return RedirectToAction("Index");
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(string id)
        {
            AdminDAL adminDAL = new AdminDAL();
            Admin admin = adminDAL.getDetailsbyid(id);
            AdminModel am = new AdminModel();
            am.FlightNo = admin.FlightNo;
            am.ArrivalCity = admin.ArrivalCity;
            am.DepartureCity = admin.DepartureCity;
            am.Arrivaltime = TimeSpan.FromTicks(admin.Arrivaltime.Ticks);
            am.Departtime = TimeSpan.FromTicks(admin.Departtime.Ticks);
            am.Seatingcap = admin.Seatingcap;
            am.Amount = admin.Amount;
            return View(am);
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(string id,AdminModel am)
        {
            AdminDAL adminDAL = new AdminDAL();
            Admin admin = new Admin(); 
            admin.FlightNo = id;
            admin.ArrivalCity = am.ArrivalCity;
            admin.DepartureCity = am.DepartureCity;
            admin.Arrivaltime = TimeSpan.FromTicks(am.Arrivaltime.Ticks);
            admin.Departtime = TimeSpan.FromTicks(am.Departtime.Ticks);
            admin.Seatingcap = am.Seatingcap;
            admin.Amount = am.Amount;
            adminDAL.Update_FlightDetails(admin);
            return RedirectToAction("Index");

        }

        // GET: Admin/Delete/5
        public ActionResult Delete(string id)
        {
            AdminDAL adminDAL = new AdminDAL();
            Admin admin = adminDAL.getDetailsbyid(id);
            AdminModel am = new AdminModel();
            am.FlightNo = admin.FlightNo;
            am.ArrivalCity = admin.ArrivalCity;
            am.DepartureCity = admin.DepartureCity;
            am.Arrivaltime = TimeSpan.FromTicks(admin.Arrivaltime.Ticks);
            am.Departtime = TimeSpan.FromTicks(admin.Departtime.Ticks);
            am.Seatingcap = admin.Seatingcap;
            am.Amount = admin.Amount;
            return View(am);

            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, AdminModel am)
        {
            
            AdminDAL adminDAL = new AdminDAL();
            Admin admin = new Admin();
            admin.FlightNo = id;
            admin.ArrivalCity = am.ArrivalCity;
            admin.DepartureCity = am.DepartureCity;
            admin.Arrivaltime = TimeSpan.FromTicks(am.Arrivaltime.Ticks);
            admin.Departtime = TimeSpan.FromTicks(am.Departtime.Ticks);
            admin.Seatingcap = am.Seatingcap;
            admin.Amount = am.Amount;
            adminDAL.Delete_FlightDetails(admin);
            return RedirectToAction("Index");
        }
        public ActionResult ShowBookings()
        {
            AdminDAL dal = new AdminDAL();
            List<Reservation> rlist = dal.GetReservations();
            List<ReservationModel> rmodellist = new List<ReservationModel>();
            foreach (var item in rlist)
            {
                ReservationModel rm = new ReservationModel();
                rm.Userid = item.Userid;
                rm.Flightno = item.Flightno;
                rm.Nooftickets = item.Nooftickets;
                rm.Rdate = item.Rdate;
                rmodellist.Add(rm);

            }
            return View(rmodellist);
        }
    }
}
