using BusBooking.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using System.EnterpriseServices;

namespace BusBooking.Repository
{
    public class ScheduleRepository
    {
        public static string GetConnect()
        {
            string sqlConnect = ConfigurationManager.ConnectionStrings["Booking"].ConnectionString;
            return sqlConnect;
        }
        public string connectionString = GetConnect();
        //Add Route
        public void AddSchedule(Schedule scheduleObj, int busId, string busName, string origin, string destination)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPI_AddSchedule", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@busId", busId);
                    cmd.Parameters.AddWithValue("@busName", busName);
                    cmd.Parameters.AddWithValue("@travelDate", scheduleObj.travelDate);
                    cmd.Parameters.AddWithValue("@fare", scheduleObj.fare);
                    cmd.Parameters.AddWithValue("@arrivalTime", scheduleObj.arrivalTime);
                    cmd.Parameters.AddWithValue("@departureTime", scheduleObj.departureTime);
                    cmd.Parameters.AddWithValue("@origin", origin);
                    cmd.Parameters.AddWithValue("@destination", destination);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public List<Schedule> ScheduleDetails(Schedule scheduleObj,int busId)
        {
            List<Schedule> schedulelist = new List<Schedule>();
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPI_ScheduleDetails", sqlConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@busId", busId);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sqlConn.Open();
                    adapter.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        schedulelist.Add(new Schedule
                        {
                            travelDate = Convert.ToDateTime(dr["travelDate"]),
                            fare = Convert.ToInt32(dr["fare"]),
                            arrivalTime = Convert.ToString(dr["arrivalTime"]),
                            departureTime = Convert.ToString(dr["departureTime"])
                        });
                    }
                    return schedulelist;
                }
                finally
                {
                    sqlConn.Close();
                }
            }
        }
    }
}