using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlamingoLibrary
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public string Mode { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string email { get; set; }
    }
}
