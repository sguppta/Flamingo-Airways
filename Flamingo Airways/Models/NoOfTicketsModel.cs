using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Flamingo_Airways.Models
{
    public class NoOfTicketsModel
    {
        [Display(Name ="Enter no. of tickets")]
        public int Nooftickets { get; set; }
    }
}