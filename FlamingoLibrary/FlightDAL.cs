using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace FlamingoLibrary
{
    public class FlightDAL
    {
        SqlConnection cn;
        public FlightDAL()
        {
            cn = new SqlConnection(ConfigurationManager.ConnectionStrings["FlightBooking"].ConnectionString);
        }
        public int FindSeat(string fid)
        {
            SqlCommand cmd = new SqlCommand("SeatAvailaiblity", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flightno", fid);
            cmd.Parameters.Add("@avlbl", SqlDbType.Int);
            cmd.Parameters["@avlbl"].Direction = ParameterDirection.Output;
            cn.Open();
            cmd.ExecuteNonQuery();
            int loginStatus = Convert.ToInt32(cmd.Parameters["@avlbl"].Value);
            cn.Close();
            return loginStatus;
        }
        public List<FlightDetails> FindFlights(FlightDetails fd)
        {
            List<FlightDetails> myl = new List<FlightDetails>();
            SqlCommand cmd = new SqlCommand("FindFlights", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ar", fd.Arrival_Airport);
            cmd.Parameters.AddWithValue("@dp", fd.Depart_Airport);
           // cmd.Parameters.AddWithValue("@date", fd.Depart_Time);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                FlightDetails f = new FlightDetails();
                f.Flightno = dr[0].ToString();
                f.Arrival_Airport = dr[1].ToString();
                f.Depart_Airport = dr[2].ToString();
                f.Arrival_Time = (TimeSpan)(dr[3]);
                f.Depart_Time = (TimeSpan)dr[4];
                f.Seating_Capacity = Convert.ToInt32(dr[5]);
                f.Amount = Convert.ToDouble(dr[6]);
                myl.Add(f);
            }
            cn.Close();
            return myl;

        }
        public List<String> GetArrivalCities()
        {
            List<String> acity = new List<string>();
            SqlCommand cmd = new SqlCommand("select Distinct(Arrival_Airport)from  Flight_Details", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                acity.Add(dr[0].ToString());
            }
            cn.Close();
            return acity;
        }
        public List<String> GetDepartCities()
        {
            List<String> acity = new List<string>();
            SqlCommand cmd = new SqlCommand("select Distinct(Depart_Airport) from  Flight_Details", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                acity.Add(dr[0].ToString());
            }
            cn.Close();
            return acity;
        }
        public decimal TotalPayment(string fid)
        {
           
            SqlCommand cmd = new SqlCommand("getAmount", cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fno", fid);
            cmd.Parameters.Add("@result", SqlDbType.Decimal);
            cmd.Parameters["@result"].Direction = ParameterDirection.Output;
            cn.Open();
            cmd.ExecuteNonQuery();
            decimal amount = Convert.ToDecimal(cmd.Parameters["@result"].Value);
            cn.Close();
            return amount;
        }
        public void ConfirmPayment(Reservation r,Payment p)
        {
            using (SqlCommand cmd = new SqlCommand("PayamentInsert", cn))
            {

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", p.Mode);
                cmd.Parameters.AddWithValue("@paymentdate",DateTime.Now.Date);
                cmd.Parameters.AddWithValue("@amt", p.Amount);
                cmd.Parameters.AddWithValue("@email", p.email);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            using (SqlCommand cmd=new SqlCommand("ReservationInsert1",cn) )
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@userid", r.Userid);
                cmd.Parameters.AddWithValue("@fno", r.Flightno);
                cmd.Parameters.AddWithValue("@noofticket", r.Nooftickets);
                cmd.Parameters.AddWithValue("@rdate", r.Rdate.Date);
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }

    }
}
