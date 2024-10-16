using BusBooking.Models;
using BusBooking.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BusBooking.Controllers
{
    public class SeatSelectionController : Controller
    {
        // GET: SeatSelection
        public ActionResult SeatSelection(string busType, string busName, float fare, int scheduleId, int availableSeats)
        {
            if (Convert.ToString(Session["email"]) == "")
            {
                return RedirectToAction("Login", "User", new {returnUrl = Url.Action("SeatSelection", "SeatSelection", new
                {
                    busName = busName,
                    busType = busType,
                    fare = fare,
                    scheduleId = scheduleId,
                    email = Session["email"],
                    origin = Session["origin"],
                    destination = Session["destination"],
                    availableSeats = availableSeats
                }) });
            }
            return View();
        }
        [HttpPost]
        public ActionResult SeatSelection(SeatSelection seatSelectionObj, string busType, string busName, float fare, string email, int availableSeats)
        {
            try
            {
                SeatSelectionRepository seatSelectionRepository = new SeatSelectionRepository();
                if ( seatSelectionObj.totalSeatsBooked > availableSeats)
                {
                    ViewBag.BookingSuccess = false;
                    return View();
                }
                seatSelectionRepository.ConfirmBooking(seatSelectionObj, busType, busName, fare, email,availableSeats);
                ViewBag.BookingSuccess = true;
                return View();
            }
            catch
            {
                ViewBag.BookingSuccess = false;
                return View();
            }
        }

        public ActionResult DisplayMyBookings(SeatSelection seatSelectionObj, string email)
        {
            if (Session["email"] == null || Convert.ToString(Session["roleType"]) != "User")
            {
                Session.Clear();
                Session.Abandon();
                return RedirectToAction("Login", "User");
            }
            SeatSelectionRepository SeatSelectionRepository = new SeatSelectionRepository();
            return View(SeatSelectionRepository.DisplayMyBookings(seatSelectionObj, email));
        }

        public ActionResult DisplayAllBookings(SeatSelection seatSelectionObj)
        {
            if (Session["email"] == null || Convert.ToString(Session["roleType"]) != "Admin")
            {
                Session.Clear();
                Session.Abandon();
                return RedirectToAction("Login", "User");
            }
            SeatSelectionRepository SeatSelectionRepository = new SeatSelectionRepository();
            return View(SeatSelectionRepository.DisplayAllBookings(seatSelectionObj));
        }

        /*public ActionResult CancelTicket(SeatSelection seatSelectionObj, int bookingId)
        {
            if (Session["email"] == null)
            {
                return RedirectToAction("Login");
            }
            SeatSelectionRepository SeatSelectionRepository = new SeatSelectionRepository();
            return View("DisplayMyBookings","SeatSelection");
        }*/

        public static string GetConnect()
        {
            string sqlConnect = ConfigurationManager.ConnectionStrings["Booking"].ConnectionString;
            return sqlConnect;
        }
        public string connectionString = GetConnect();



        public ActionResult CancelTicket(int bookingId)
        {
            if (Session["email"] == null || Convert.ToString(Session["roleType"]) != "User")
            {
                Session.Clear();
                Session.Abandon();
                return RedirectToAction("Login", "User");
            }

            string email = Convert.ToString(Session["email"]);
            SeatSelectionRepository seatSelectionRepository = new SeatSelectionRepository();
            seatSelectionRepository.CancelTicket(bookingId, email);

            return RedirectToAction("DisplayMyBookings", "SeatSelection", new { email = email });
        }


        public ActionResult CancelRequests(CancelRequests CancelRequestsObj)
        {
            if (Session["email"] == null || Convert.ToString(Session["roleType"]) != "Admin")
            {
                Session.Clear();
                Session.Abandon();
                return RedirectToAction("Login", "User");
            }
            SeatSelectionRepository seatSelectionRepository = new SeatSelectionRepository();
            return View(seatSelectionRepository.DisplayCancelRequests(CancelRequestsObj));
        }

        [HttpPost]
        public ActionResult CancelRequests(int requestId, int bookingId, string action)
        {
            SeatSelectionRepository seatSealectionrepository = new SeatSelectionRepository();
            seatSealectionrepository.CancelRequests(requestId, bookingId, action);
            return RedirectToAction("CancelRequests", "SeatSelection");
        }


        // GET: SeatSelection/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SeatSelection/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SeatSelection/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SeatSelection/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SeatSelection/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SeatSelection/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SeatSelection/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
