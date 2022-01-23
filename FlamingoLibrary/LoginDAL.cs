using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace FlamingoLibrary
{
   public class LoginDAL
    {
        public bool AuthenticateUser(Passenger p)
        {
            bool loginStatus = false;
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["FlightBooking"].ConnectionString);
            SqlCommand cmd = new SqlCommand("select * from passenger where Email= '" + p.Email + "' and Pwd='" + p.Pwd + "'", cn);
            cn.Open();
           SqlDataReader dr= cmd.ExecuteReader();
            if(dr.Read())
            {
                loginStatus = true;
            }
            else
            {
                loginStatus = false;
            }
            cn.Close();
            return loginStatus;

        }
        public void Registration(Passenger p)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["FlightBooking"].ConnectionString);
            SqlCommand cmd = new SqlCommand("PassengerInsert", cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fname",p.FName);
            cmd.Parameters.AddWithValue("@lname", p.LName);
            cmd.Parameters.AddWithValue("@email",p.Email);
            cmd.Parameters.AddWithValue("@pwd", p.Pwd);
            cmd.Parameters.AddWithValue("@phone", p.PhoneNo);
            cmd.Parameters.AddWithValue("@age",p.Age);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }
        public void ChangePassword(Passenger p)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["FlightBooking"].ConnectionString);
            SqlCommand cmd = new SqlCommand("ChangePass", cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@email", p.Email);
            cmd.Parameters.AddWithValue("@pwd", p.Pwd);
            cn.Open();
            cmd.ExecuteNonQuery();

            cn.Close();
        }
        public bool EmailExist(String p)
        {
            SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["FlightBooking"].ConnectionString);
            SqlCommand cmd = new SqlCommand("CheckEmail", cn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@email", p);
            cmd.Parameters.Add("@c", SqlDbType.Int);
            cmd.Parameters["@c"].Direction = ParameterDirection.Output;
            cn.Open();
            cmd.ExecuteNonQuery();
            byte loginStatus = Convert.ToByte(cmd.Parameters["@c"].Value);

            cn.Close();
            return (loginStatus == 1);
        }

        }
    }
