using BusBooking.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;

namespace BusBooking.Repository
{
    public class UserRepository
    {
        public static string GetConnect()
        {
            string sqlConnect = ConfigurationManager.ConnectionStrings["Booking"].ConnectionString;
            return sqlConnect;
        }
        public string connectionString = GetConnect();

        /*public string GetBackgroundImage()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPI_GetBackgroundImage", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    User userObj = new User();
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    userObj.BackgroundUrl = reader["backgroundUrl"].ToString();
                    return userObj.BackgroundUrl;
                }
                finally
                {
                    conn.Close();
                }
            }
        }*/

        public string GetLogo()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPI_GetLogo", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    string logoUrl = reader["logoUrl"].ToString();
                    return logoUrl;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public List<Schedule> SearchBusResult(User userObj)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPI_SearchBusResult", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@fromPlace", userObj.fromPlace);
                    cmd.Parameters.AddWithValue("@toPlace", userObj.toPlace);
                    cmd.Parameters.AddWithValue("@travelDate", userObj.travelDate);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    conn.Open();
                    sqlDataAdapter.Fill(dt);
                    conn.Close();
                    List<Schedule> list = new List<Schedule>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Debug.Write(dr["busName"]);
                        Debug.Write(dr["busType"]);
                        Debug.Write(dr["fare"]);
                        Debug.Write(dr["travelDate"]);
                        Debug.Write(dr["availableSeats"]);

                        list.Add(new Schedule
                        {
                            busName = Convert.ToString(dr["busName"]),
                            busType = Convert.ToString(dr["busType"]),
                            fare = Convert.ToInt32(dr["fare"]),
                            travelDate = Convert.ToDateTime(dr["travelDate"]),
                            arrivalTime = Convert.ToString(dr["arrivalTime"]),
                            departureTime = Convert.ToString(dr["departureTime"]),
                            bookedSeats = Convert.ToInt32(dr["bookedSeats"]),
                            availableSeats = Convert.ToInt32(dr["availableSeats"])
                        });
                    }
                    return list;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        
        public void Signup(User userObj)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPI_addDetails", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@firstName", userObj.Firstname);
                    cmd.Parameters.AddWithValue("@lastName", userObj.Lastname);
                    cmd.Parameters.AddWithValue("@email", userObj.Email);
                    cmd.Parameters.AddWithValue("@userPassword", HashPassword(userObj.Password));
                    cmd.Parameters.AddWithValue("@mobileNumber", userObj.Mobilenumber);
                    cmd.Parameters.AddWithValue("@gender", userObj.Gender);
                    cmd.Parameters.AddWithValue("@dateOfBirth", userObj.DateOfBirth);
                    cmd.Parameters.AddWithValue("@age", userObj.Age);
                    cmd.Parameters.AddWithValue("@userState", userObj.State);
                    cmd.Parameters.AddWithValue("@city", userObj.City);
                    cmd.Parameters.AddWithValue("@userAddress", userObj.Address);
                    cmd.Parameters.AddWithValue("@roleType", "User");
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void AddAdmin(User userObj)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPI_addDetails", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@firstName", userObj.Firstname);
                    cmd.Parameters.AddWithValue("@lastName", userObj.Lastname);
                    cmd.Parameters.AddWithValue("@email", userObj.Email);
                    cmd.Parameters.AddWithValue("@userPassword", HashPassword(userObj.Password));
                    cmd.Parameters.AddWithValue("@mobileNumber", userObj.Mobilenumber);
                    cmd.Parameters.AddWithValue("@gender", userObj.Gender);
                    cmd.Parameters.AddWithValue("@dateOfBirth", userObj.DateOfBirth);
                    cmd.Parameters.AddWithValue("@age", userObj.Age);
                    cmd.Parameters.AddWithValue("@userState", userObj.State);
                    cmd.Parameters.AddWithValue("@city", userObj.City);
                    cmd.Parameters.AddWithValue("@userAddress", userObj.Address);
                    cmd.Parameters.AddWithValue("@roleType", "Admin");
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void DeleteUser(User userObj)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPI_deleteDetails", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", userObj.Email);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void UpdateUser(User userObj)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPI_updateDetails", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@firstName", userObj.Firstname);
                    cmd.Parameters.AddWithValue("@lastName", userObj.Lastname);
                    cmd.Parameters.AddWithValue("@email", userObj.Email);
                    cmd.Parameters.AddWithValue("@userPassword", userObj.Password);
                    cmd.Parameters.AddWithValue("@mobileNumber", userObj.Mobilenumber);
                    cmd.Parameters.AddWithValue("@gender", userObj.Gender);
                    cmd.Parameters.AddWithValue("@dateOfBirth", userObj.DateOfBirth);
                    cmd.Parameters.AddWithValue("@age", userObj.Age);
                    cmd.Parameters.AddWithValue("@userState", userObj.State);
                    cmd.Parameters.AddWithValue("@city", userObj.City);
                    cmd.Parameters.AddWithValue("@userAddress", userObj.Address);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public static string HashPassword(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
        public string Login(User userObj)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SPI_LoginUser", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", userObj.Email);
                    cmd.Parameters.AddWithValue("@userPassword", HashPassword(userObj.Password));

                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        if (dataReader.Read()) 
                        {
                            string role = dataReader["roleType"].ToString().Trim();
                            return role;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public bool IsUserExists(User userObj)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommand = new SqlCommand("SPI_CheckUserExists", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@email", userObj.Email);
                sqlCommand.Parameters.AddWithValue("@userPassword", HashPassword(userObj.Password));
                sqlConnection.Open();
                int count = (int)sqlCommand.ExecuteScalar();
                sqlConnection.Close();

                return count > 0;
            }
        }

        // Admin Login
        public int AdminLogin(User userObj)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SPI_LoginAdmin", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", userObj.Email);
                    cmd.Parameters.AddWithValue("@userPassword", userObj.Password);
                    // Execute the stored procedure and get the count
                    object result = cmd.ExecuteScalar();

                    // Convert the result to an integer (assuming it is not null)
                    int i = result != DBNull.Value ? Convert.ToInt32(result) : 0;

                    // Output the result
                    if (i >= 1)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}