using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlamingoLibrary
{
    public class AdminDAL
    {
        public Admin getDetailsbyid(string fno)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["FlightBooking"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Select * from fn_GetDetailsByID(@fno)", cn);
            //cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fno",fno);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Admin admin = new Admin();
            admin.FlightNo = dr["Flight_no"].ToString();
            admin.ArrivalCity = dr["Arrival_Airport"].ToString();
            admin.DepartureCity = dr["Depart_Airport"].ToString();
            admin.Arrivaltime = ((TimeSpan)dr["arrival_time"]);
            admin.Departtime = ((TimeSpan)dr["depart_time"]);
            admin.Seatingcap = Convert.ToInt32(dr["seating_capacity"]);
            admin.Amount = Convert.ToDecimal(dr["Amount"]);
            cn.Close();
            return admin;
        }
        public List<Admin> GetFlightDetails()
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["FlightBooking"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select * from Flight_Details", cn);
            cn.Open();
            SqlDataReader dr= cmd.ExecuteReader();
            List<Admin> admins = new List<Admin>();
            while(dr.Read())
            {
                Admin admin = new Admin();
               admin.FlightNo= dr["Flight_no"].ToString();
                admin.ArrivalCity=dr["Arrival_Airport"].ToString();
               admin.DepartureCity =dr["Depart_Airport"].ToString();
                admin.Arrivaltime=((TimeSpan)dr["arrival_time"]);
                admin.Departtime= ((TimeSpan)dr["depart_time"]);
                admin.Seatingcap=Convert.ToInt32(dr["seating_capacity"]);
                admin.Amount= Convert.ToDecimal(dr["Amount"]);
                admins.Add(admin);
            }
            cn.Close();
            return admins;

        }
        public void Add_FlightDetails(Admin admin)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["FlightBooking"].ConnectionString);
            SqlCommand cmd = new SqlCommand("InsertFlightDetails", cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flightno", admin.FlightNo);
            cmd.Parameters.AddWithValue("@arrivalcity",admin.ArrivalCity);
            cmd.Parameters.AddWithValue("@departcity", admin.DepartureCity);
            cmd.Parameters.AddWithValue("@arrivaltime",admin.Arrivaltime);
            cmd.Parameters.AddWithValue("@departtime",admin.Departtime);
            cmd.Parameters.AddWithValue("@seatingcap", admin.Seatingcap);
            cmd.Parameters.AddWithValue("@amount", admin.Amount);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();

        }
        public void Update_FlightDetails(Admin admin)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["FlightBooking"].ConnectionString);
            SqlCommand cmd = new SqlCommand("UpdateFlightDetails", cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flightno", admin.FlightNo);
            cmd.Parameters.AddWithValue("@arrivalcity", admin.ArrivalCity);
            cmd.Parameters.AddWithValue("@departcity", admin.DepartureCity);
            cmd.Parameters.AddWithValue("@arrivaltime",admin.Arrivaltime);
            cmd.Parameters.AddWithValue("@departtime",admin.Departtime);
            cmd.Parameters.AddWithValue("@seatingcap", admin.Seatingcap);
            cmd.Parameters.AddWithValue("@amount", admin.Amount);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();

        }
        public void Delete_FlightDetails(Admin admin)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["FlightBooking"].ConnectionString);
            SqlCommand cmd = new SqlCommand("DeleteFlightDetails", cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@flightno", admin.FlightNo);

            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public List<Reservation> GetReservations()
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["FlightBooking"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select * from fn_ShowReservations()", cn);
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            List<Reservation> rlist = new List<Reservation>();
            while (dr.Read())
            {
                Reservation rs = new Reservation();

                rs.Flightno= dr["Flight_No"].ToString();
                rs.Rdate= Convert.ToDateTime(dr["R_date"]);
                rs.Userid= dr["UserId"].ToString();
                rs.Nooftickets= Convert.ToInt32(dr["No_of_Tickets"]);
               
                rlist.Add(rs);
            }
            cn.Close();
            return rlist;

        }

    }
}
