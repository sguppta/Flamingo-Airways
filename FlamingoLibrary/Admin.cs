using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlamingoLibrary
{
    public class Admin
    {
        public string FlightNo { get; set; }
        public string ArrivalCity { get; set; }
        public string DepartureCity { get; set; }
        public TimeSpan Arrivaltime { get; set; }
        public TimeSpan Departtime { get; set; }
        public int Seatingcap { get; set; }
        public decimal Amount { get; set; }
    }
}
