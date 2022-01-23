using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flamingo_Airways.Models
{
    public class PaymentModel
    {

        [Required]
        [StringLength(10)]
        [Display(Name = "Mode(Credit Card/Debit Card)")]
        public string Mode { get; set; }

        private DateTime myVar;

        public DateTime Paymentdate
        {
            //get { return myVar; }
            set { myVar = DateTime.Now; }
        }


        public decimal Amount { get; set; }

        [Required]
        [StringLength(25)]
        [Display(Name = "Enter your registered email")]
        public string User_Email { get; set; }

        [Required,RegularExpression(@"^([0-9]{16})$")]
        [DataType(DataType.CreditCard)]
        [Display(Name = "enter your Card Number")]
        public long Cardno { get; set; }

        [Required]
        
        [MinLength(3)]
        [MaxLength(3)]
        [DataType(DataType.Password)]
        [Display(Name = "Enter CVV")]
        public string cvv { get; set; }
    }
}