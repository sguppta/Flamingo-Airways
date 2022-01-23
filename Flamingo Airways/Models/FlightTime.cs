using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flamingo_Airways.Models
{
    public class FlightTime
    {
        [Required(ErrorMessage = "Enter The Date")]
        public DateTime FlightDate { get; set; }

        [Required(ErrorMessage = "Enter The Arrival City")]
        public String ArrivalAirport { get; set; }

        [Required(ErrorMessage = "Enter The Destination City")]
        public String DepartureAirport { get; set; }
    }
}