using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FlamingoLibrary;
using Flamingo_Airways.Models;

namespace Flamingo_Airways.Controllers
{
    public class TicketController : Controller
    {
        // GET: Ticket
        public ActionResult TicketView()
        {
            ViewBag.username=TempData["username"];
            string mail = TempData["useremail"].ToString();
            TempData.Keep();
            TicketDAL dal = new TicketDAL();
            Ticket t = dal.GetTicket(mail);
            TicketModel tm = new TicketModel();
            tm.rid = t.rid;
            tm.flightno = t.flightno;
            tm.arrivalcity = t.arrivalcity;
            tm.departcity = t.departcity;
            tm.arrivaltime = TimeSpan.FromTicks(t.arrivaltime.Ticks);
            tm.departtime = TimeSpan.FromTicks(t.departtime.Ticks);
            tm.rdate = t.rdate;
            tm.amount = t.amount;
            tm.nooftickets = t.nooftickets;
            tm.userid = t.userid;
            return View(tm);
        }
        public ActionResult CancelTicket()
        {
            return View();
        }
        public ActionResult CancelConfirm()
        {
            return View();
        }


    }
}