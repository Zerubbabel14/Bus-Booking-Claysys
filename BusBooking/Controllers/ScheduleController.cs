using BusBooking.Models;
using BusBooking.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusBooking.Controllers
{
    public class ScheduleController : Controller
    {
        // GET: Schedule
        public ActionResult Index()
        {
            return View();
        }

        // GET: Schedule/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Schedule/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Schedule/Create
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

        // GET: Schedule/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Schedule/Edit/5
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

        // GET: Schedule/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Schedule/Delete/5
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

        public ActionResult AddSchedule()
        {
            if (Session["email"] == null || Convert.ToString(Session["roleType"]) != "Admin")
            {
                Session.Clear();
                Session.Abandon();
                return RedirectToAction("Login", "User");
            }
            return View();
        }

        // POST: Bus/Create
        [HttpPost]
        public ActionResult AddSchedule(Schedule scheduleObj, int busId, string busName, string origin, string destination)
        {
            try
            { 
                ScheduleRepository schedulerepository = new ScheduleRepository();
                schedulerepository.AddSchedule(scheduleObj, busId, busName,origin, destination);
                return RedirectToAction("BusDetails","Bus");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ScheduleDetails(Schedule scheduleObj, int busId)
        {
            if (Session["email"] == null || Convert.ToString(Session["roleType"]) != "Admin")
            {
                Session.Clear();
                Session.Abandon();
                return RedirectToAction("Login", "User");
            }
            ScheduleRepository schedulerepository = new ScheduleRepository();
            List<Schedule> list = schedulerepository.ScheduleDetails(scheduleObj, busId);
            if (list.Count == 0)
            {
                ViewBag.Message = "No schedules found.";
            }
            return View(list);
        }
    }
}
