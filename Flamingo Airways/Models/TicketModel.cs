using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Flamingo_Airways.Models
{
    public class TicketModel
    {
        [DisplayName("Ticket ID")]
        public int rid { get; set; }


        [DisplayName("Flight No.")]
        public string flightno { get; set; }


        [DisplayName("Boarding date")]
        public DateTime rdate { get; set; }


        [DisplayName("Amount")]
        public decimal amount { get; set; }


        [DisplayName("Arrival at")]
        public string arrivalcity { get; set; }


        [DisplayName("Depart from")]
        public string departcity { get; set; }


        [DisplayName("Arrival time")]
        public TimeSpan arrivaltime { get; set; }


        [DisplayName("Departure time")]
        public TimeSpan departtime{ get; set; }


        [DisplayName("No. of Tickets")]
        public int nooftickets { get; set; }


        [DisplayName("User Email")]
        public string userid { get; set; }

    }
}