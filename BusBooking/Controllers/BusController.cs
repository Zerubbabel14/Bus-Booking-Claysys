using BusBooking.Models;
using BusBooking.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusBooking.Controllers
{
    public class BusController : Controller
    {
        // GET: Bus
        public ActionResult Index()
        {
            return View();
        }

        // GET: Bus/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Bus/Create
        public ActionResult AddBus()
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
        public ActionResult AddBus(Bus busObj)
        {
            try
            {
                // TODO: Add insert logic here
                BusRepository busrepository = new BusRepository();
                busrepository.AddBus(busObj);
                return RedirectToAction("AdminHome","User");
            }
            catch
            {
                return View();
            }
        }


        // Update Bus Details
        public ActionResult UpdateBusDetails()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateBusDetails(Bus busObj)
        {
            BusRepository busRepository = new BusRepository();
            busRepository.UpdateBusDetails(busObj);
            return RedirectToAction("BusDetails");
        }

        public ActionResult BusDetails(Bus busObj)
        {
            if (Session["email"] == null || Convert.ToString(Session["roleType"]) != "Admin")
            {
                Session.Clear();
                Session.Abandon();
                return RedirectToAction("Login", "User");
            }
            BusRepository busRepository = new BusRepository();  
            return View(busRepository.BusDetails(busObj));
        }
        // GET: Bus/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Bus/Edit/5
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

        // GET: Bus/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Bus/Delete/5
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
