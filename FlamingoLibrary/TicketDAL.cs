using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace FlamingoLibrary
{
    public class TicketDAL
    {
        SqlConnection cn;
        public TicketDAL()
        {
            cn = new SqlConnection(ConfigurationManager.ConnectionStrings["FlightBooking"].ConnectionString);
        }
        public Ticket GetTicket(string mail)
        {
            SqlCommand cmd = new SqlCommand("select * from GetTicket(@userid)", cn);
            cmd.Parameters.AddWithValue("@userid", mail);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Ticket t = new Ticket();
            t.rid = Convert.ToInt32(dr["R_ID"]);
            t.flightno = dr["Flight_No"].ToString();
            t.rdate = Convert.ToDateTime(dr["R_date"]);
            t.amount= Convert.ToDecimal(dr["Amount"]);
            t.arrivalcity= dr["Arrival_Airport"].ToString();
            t.departcity= dr["Depart_Airport"].ToString();
            t.arrivaltime= ((TimeSpan)dr["arrival_time"]);
            t.departtime= ((TimeSpan)dr["depart_time"]);
            t.nooftickets= Convert.ToInt32(dr["No_of_Tickets"]);
            t.userid= dr["UserId"].ToString();

            cn.Close();
            return t;

        }
    }
}
