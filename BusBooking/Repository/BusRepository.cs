using BusBooking.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace BusBooking.Repository
{
    public class BusRepository
    {
        public static string GetConnect()
        {
            string sqlConnect = ConfigurationManager.ConnectionStrings["Booking"].ConnectionString;
            return sqlConnect;
        }
        public string connectionString = GetConnect();
        //Display Bus Details
        public List<Bus> BusDetails(Bus busObj)
        {
            List<Bus> buslist = new List<Bus>();
            using(SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPI_BusDetails",sqlConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sqlConn.Open();
                    adapter.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        buslist.Add(new Bus
                        {
                            BusId = Convert.ToInt32(dr["busId"]),
                            BusNo = Convert.ToString(dr["busNo"]),
                            BusName = Convert.ToString(dr["busName"]),
                            BusType = Convert.ToString(dr["busType"]),
                            SeatRow = Convert.ToInt32(dr["seatRow"]),
                            SeatColumn = Convert.ToInt32(dr["seatColumn"]),
                            Origin = Convert.ToString(dr["origin"]),
                            Destination = Convert.ToString(dr["destination"])
                        });
                    }
                    return buslist;
                }
                finally
                {
                    sqlConn.Close();
                }
            }
        }
        //Add new bus
        public void AddBus(Bus busObj)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPI_AddBus", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@busNo", busObj.BusNo);
                    cmd.Parameters.AddWithValue("@busType", busObj.BusType);
                    int totalSeat = busObj.SeatRow * busObj.SeatColumn;
                    cmd.Parameters.AddWithValue("@totalSeat", totalSeat);
                    cmd.Parameters.AddWithValue("@seatRow", busObj.SeatRow);
                    cmd.Parameters.AddWithValue("@seatColumn", busObj.SeatColumn);
                    int bookedSeats = 0;
                    cmd.Parameters.AddWithValue("@bookedSeats", bookedSeats);
                    cmd.Parameters.AddWithValue("@availableSeats", totalSeat);
                    cmd.Parameters.AddWithValue("@origin", busObj.Origin);
                    cmd.Parameters.AddWithValue("@destination", busObj.Destination);
                    cmd.Parameters.AddWithValue("@busName", busObj.BusName);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void UpdateBusDetails(Bus busObj)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPI_updateBusDetails", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@busNo", busObj.BusNo);
                    cmd.Parameters.AddWithValue("@busType", busObj.BusType);
                    cmd.Parameters.AddWithValue("@seatRow", busObj.SeatRow);
                    cmd.Parameters.AddWithValue("@seatColumn", busObj.SeatColumn);
                    cmd.Parameters.AddWithValue("@origin", busObj.Origin);
                    cmd.Parameters.AddWithValue("@destination", busObj.Destination);
                    cmd.Parameters.AddWithValue("@busName", busObj.BusName);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
        }


        
    }
}