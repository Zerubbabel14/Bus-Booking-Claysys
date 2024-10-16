using BusBooking.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using System.Web.Helpers;

namespace BusBooking.Repository
{
    public class SeatSelectionRepository
    {
        public static string GetConnect()
        {
            string sqlConnect = ConfigurationManager.ConnectionStrings["Booking"].ConnectionString;
            return sqlConnect;
        }
        public string connectionString = GetConnect();
        public void ConfirmBooking(SeatSelection seatSelectionObj, string busType, string busName, float fare, string email, int availableSeats)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {       
                try
                {
                    SqlCommand cmd = new SqlCommand("SPI_ConfirmBooking", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@busName", busName);
                    cmd.Parameters.AddWithValue("@busType", busType);
                    var fareCalculation = fare * seatSelectionObj.totalSeatsBooked;
                    cmd.Parameters.AddWithValue("@fare",fareCalculation);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@travelDate", seatSelectionObj.travelDate);
                    cmd.Parameters.AddWithValue("@origin", seatSelectionObj.origin);
                    cmd.Parameters.AddWithValue("@destination", seatSelectionObj.destination);
                    cmd.Parameters.AddWithValue("@aadharNo", seatSelectionObj.aadharNo);
                    cmd.Parameters.AddWithValue("@modeOfPayment", seatSelectionObj.modeOfPayment);
                    cmd.Parameters.AddWithValue("@totalSeatsBooked", seatSelectionObj.totalSeatsBooked);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public List<SeatSelection> DisplayAllBookings(SeatSelection seatSelectionObj)
        {
            List<SeatSelection> bookinglist = new List<SeatSelection>();
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPI_DisplayAllBookings", sqlConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sqlConn.Open();
                    adapter.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        bookinglist.Add(new SeatSelection
                        {
                            bookingId = Convert.ToInt32(dr["bookingId"]),
                            firstName = Convert.ToString(dr["firstName"]),
                            busName = Convert.ToString(dr["busName"]),
                            email = Convert.ToString(dr["email"]),
                            travelDate = Convert.ToDateTime(dr["travelDate"]),
                            origin = Convert.ToString(dr["origin"]),
                            destination = Convert.ToString(dr["destination"]),
                            fare = Convert.ToInt32(dr["fare"]),
                            modeOfPayment = Convert.ToString(dr["modeOfPayment"]),
                            aadharNo = Convert.ToString(dr["aadharNo"]),
                            totalSeatsBooked = Convert.ToInt32(dr["totalSeatsBooked"])
                        });
                    }
                    return bookinglist;
                }
                finally
                {
                    sqlConn.Close();
                }
            }
        }

        public List<SeatSelection> DisplayMyBookings(SeatSelection seatSelectionObj, string email)
        {
            List<SeatSelection> bookinglist = new List<SeatSelection>();
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPI_DisplayMyBookings", sqlConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", email);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sqlConn.Open();
                    adapter.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        bookinglist.Add(new SeatSelection
                        {
                            bookingId = Convert.ToInt32(dr["bookingId"]),
                            firstName = Convert.ToString(dr["firstName"]),
                            busName = Convert.ToString(dr["busName"]),
                            email = Convert.ToString(dr["email"]),
                            travelDate = Convert.ToDateTime(dr["travelDate"]),
                            origin = Convert.ToString(dr["origin"]),
                            destination = Convert.ToString(dr["destination"]),
                            fare = Convert.ToInt32(dr["fare"]),
                            modeOfPayment = Convert.ToString(dr["modeOfPayment"]),
                            aadharNo = Convert.ToString(dr["aadharNo"]),
                            totalSeatsBooked = Convert.ToInt32(dr["totalSeatsBooked"]),
                            cancellationStatus = Convert.ToString(dr["cancellationStatus"])
                        });
                    }
                    return bookinglist;
                }
                finally
                {
                    sqlConn.Close();
                }
            }
        }

        public void CancelTicket(int bookingId, string email)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try { 
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SPI_CancelTicket", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@bookingId", bookingId);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void CancelRequests(int requestId, int bookingId, string action)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd;
                    if (action == "Approve")
                    { 
                        cmd = new SqlCommand("SPI_CancelRequestsApprove", conn);
                    }
                    else
                    {
                        cmd = new SqlCommand("SPI_CancelRequestsReject", conn);
                    }
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@requestId", requestId);
                    cmd.Parameters.AddWithValue("@bookingId", bookingId);
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public List<CancelRequests> DisplayCancelRequests(CancelRequests cancelRequestsObj)
        {
            List<CancelRequests> CancelRequestslist = new List<CancelRequests>();
            using (SqlConnection sqlConn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SPI_DisplayCancelRequests", sqlConn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sqlConn.Open();
                    adapter.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        CancelRequestslist.Add(new CancelRequests
                        {
                            requestId = Convert.ToInt32(dr["requestId"]),
                            bookingId = Convert.ToInt32(dr["bookingId"]),
                            email = Convert.ToString(dr["email"]),
                            requestDate = Convert.ToString(dr["requestDate"]),
                            cancelStatus = Convert.ToString(dr["cancelStatus"])
                        });
                    }
                    return CancelRequestslist;
                }
                finally
                {
                    sqlConn.Close();
                }
            }
        }
    }
}