using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlamingoLibrary;
using Flamingo_Airways.Models;

namespace Flamingo_Airways.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            FlightDAL fd = new FlightDAL();
            ViewBag.arrival = fd.GetArrivalCities();
            ViewBag.depart = fd.GetDepartCities();
            return View();
        }
        [HttpPost]
        public ActionResult Index(FlightTime fp)
        {
            TempData["flightdate"]=fp.FlightDate.Date;
            FlightDAL fd = new FlightDAL();
            FlightDetails f = new FlightDetails();
            f.Arrival_Airport = fp.ArrivalAirport;
            f.Depart_Airport = fp.DepartureAirport;
            //f.Depart_Time = fp.FlightDate;
            List<FlightDetails> mylist = new List<FlightDetails>();
            List<FlightModel> flist = new List<FlightModel>();

            mylist = fd.FindFlights(f);

            foreach (var item in mylist)
            {
                FlightModel fm = new FlightModel();
                fm.Amount = item.Amount;
                fm.Arrival_Airport = item.Arrival_Airport;
                fm.Depart_Airport = item.Depart_Airport;
                fm.Arrival_Time = item.Arrival_Time;
                fm.Depart_Time = item.Depart_Time;
                fm.Flightno = item.Flightno;
                fm.Seating_Capacity = item.Seating_Capacity;
                fm.Availaible_Seat = fd.FindSeat(item.Flightno);
                flist.Add(fm);

            }

            TempData["FP"] = flist;
            return RedirectToAction("ShowFlights");
        }
        public ActionResult BookFlights(String id)
        {
            TempData.Keep();
            return View();
        }
        [HttpPost]
        public ActionResult BookFlights(String id,NoOfTicketsModel m)
        {
            TempData["nooftickets"]=m.Nooftickets;
            TempData["FlightID"] = id;
            return RedirectToAction("Payment");
        }
        public ActionResult ShowFlights()
        {

            List<FlightModel> flist = TempData["FP"] as List<FlightModel>;
            if (flist.Count == 0)
            {
                ViewBag.error = "Sorry No Flights Availaible!! Change your Choice..";

            }

            return View(flist);
        }
        public ActionResult Payment()
        {
            PaymentModel pm = new PaymentModel();
            FlightDAL dal = new FlightDAL();
            string fid=TempData["FlightID"].ToString();
            decimal amount=dal.TotalPayment(fid);
           int tickets = Convert.ToInt32(TempData["nooftickets"]);
            pm.Amount = (amount) * (tickets);
            ViewBag.totalamt = pm.Amount;
            TempData["Amount"] = pm.Amount;
            TempData.Keep();
            return View(pm);
        }
        [HttpPost]
        public ActionResult Payment(PaymentModel pm)
        {
            decimal amt = Convert.ToDecimal(TempData["Amount"]);
            string mail=TempData["useremail"].ToString();
            FlightDAL dal = new FlightDAL();
            Payment p = new Payment();
            Reservation r = new Reservation();
            p.Mode=pm.Mode;
            p.Amount = amt;
            p.email = mail;

            r.Userid = mail;
            r.Flightno= TempData["FlightID"].ToString();
            r.Nooftickets= Convert.ToInt32(TempData["nooftickets"]);
            r.Rdate = Convert.ToDateTime(TempData["flightdate"]).Date;

            dal.ConfirmPayment(r,p);
            return RedirectToAction("PaymentSuccess");
        }
        public ActionResult PaymentSuccess()
        {
            return View();
        }
        


    }
}