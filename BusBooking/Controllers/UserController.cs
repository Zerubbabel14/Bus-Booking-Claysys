using BusBooking.Models;
using BusBooking.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Serilog;

namespace BusBooking.Controllers
{
    public class UserController : Controller
    {

        private readonly string _connectionString = "your_connection_string_here";

        public ActionResult GetImage(int id)
        {
            byte[] imageData = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetImageById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ImageId", id);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            imageData = (byte[])reader["ImageData"];
                        }
                    }
                }
            }

            if (imageData != null)
            {
                return File(imageData, "image/jpeg"); // Adjust MIME type as needed
            }
            else
            {
                return HttpNotFound(); // Handle the case where the image is not found
            }
        }

    // Home Page
        public ActionResult Home()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Home(User userObj)
        {
            UserRepository userrepository = new UserRepository();

            return RedirectToAction("SearchBusResult",userObj);
        }

        // Home Page
        public ActionResult UserHome()
        {
            if (Session["email"] == null || Convert.ToString(Session["roleType"]) != "User")
            {
                Session.Clear();
                Session.Abandon();
                return RedirectToAction("Login","User");
            }
            return View();
        }
        [HttpPost]
        public ActionResult UserHome(User userObj)
        {
            UserRepository userrepository = new UserRepository();

            return RedirectToAction("SearchBusResult", userObj);
        }
        // Home Page
        public ActionResult AdminHome()
        {
            if (Session["email"] == null || Convert.ToString(Session["roleType"]) != "Admin")
            {
                Session.Clear();
                Session.Abandon();
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        [HttpPost]
        public ActionResult AdminHome(User userObj)
        {
            UserRepository userrepository = new UserRepository();

            return RedirectToAction("SearchBusResult", userObj);
        }

        //Search Bus Result
        public ActionResult SearchBusResult(User userObj)
        {
            /*if (Session["email"] == null || Convert.ToString(Session["roleType"]) != "User")
            {
                Session.Clear();
                Session.Abandon();
                return RedirectToAction("Login", "User");
            }*/
            Log.Information("Search Bus Result");
            Session["travelDate"] = userObj.travelDate;
            Session["origin"] = userObj.fromPlace;
            Session["destination"] = userObj.toPlace;
            UserRepository userrepository = new UserRepository(); 
            List<Schedule> list = userrepository.SearchBusResult(userObj);
            if (list.Count == 0)
            {
                ViewBag.Message = "No buses found.";
            }
            return View(list);
        }

        public ActionResult AddAdmin()
        {
            if (Session["email"] == null || Convert.ToString(Session["roleType"]) != "Admin")
            {
                Session.Clear();
                Session.Abandon();
                return RedirectToAction("Login", "User");
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddAdmin(User userObj)
        {
            UserRepository userrepository = new UserRepository();
            userrepository.AddAdmin(userObj);
            return RedirectToAction("AdminHome", "User");
        }
        // About Page
        public ActionResult About()
        {
            return View();
        }

        // Contact Page
        public ActionResult Contact()
        {
            return View();
        }

        // Display the users
        public ActionResult DisplayUser()
        {
            return View();
        }

        // Signup
        [HttpGet]
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User userObj)
        {
            try { 
                UserRepository userrepository = new UserRepository();   
                userrepository.Signup(userObj);
                ViewBag.SignupSuccess = true;
                return View();
            }
            catch
            {
                return View();
            }
        }

        // Login
        public ActionResult Login(string returnUrl = null)
        {
            TempData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(User userObj, string returnUrl)
        {
            UserRepository userrepository = new UserRepository();
            if (!userrepository.IsUserExists(userObj))
            {
                TempData["ErrorMessage"] = "Invalid password";
                return RedirectToAction("Login"); 
            }

            Session["email"] = userObj.Email;
            string role = userrepository.Login(userObj);
            if ( role == "Admin")
            {
                Session["roleType"] = "Admin";
                return RedirectToAction("AdminHome", "User");
            }
            else if (role == "User")
            {
                Session["roleType"] = "User";
                if (returnUrl != null)
                    return Redirect(returnUrl);
                return RedirectToAction("UserHome","User");
            }
            else
            {
                return View();
            }
        }



        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Home");
        }


        // Delete
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(User userObj)
        {
            UserRepository userrepository = new UserRepository();
            userrepository.DeleteUser(userObj);
            return RedirectToAction("Home");
        }

        // Update
        public ActionResult Update()
        {

        return View(); }
        [HttpPost]
        public ActionResult Update(User userObj)
        {
            UserRepository userrepository = new UserRepository();
            userrepository.UpdateUser(userObj);
            return RedirectToAction("Home");
        }

        
    }
}
