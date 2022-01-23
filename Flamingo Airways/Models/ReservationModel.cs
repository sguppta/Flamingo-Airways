using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flamingo_Airways.Models
{
    public class ReservationModel
    {
        //public int Rid { get; set; }
        public string Userid { get; set; }
        public string Flightno { get; set; }
        public int Nooftickets { get; set; }
        public DateTime Rdate { get; set; }
    }
}