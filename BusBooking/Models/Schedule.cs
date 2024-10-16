using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusBooking.Models
{
    public class Schedule
    {
        [Display(Name = "Schedule ID")]
        public int scheduleId { get; set; }

        [Display(Name = "Bus ID")]
        public int busId { get; set; }

        [Display(Name = "Bus Name")]
        public string busName { get; set; }
        [Display(Name = "Bus Type")]
        public string busType { get; set; }

        [Display(Name = "Travel Date")]
        public DateTime travelDate { get; set; }

        [Display(Name = "Fare")]
        public float fare {  get; set; }

        [Display(Name = "Arrival Time")]
        public string arrivalTime { get; set; }

        [Display(Name = "Departure Time")]
        public string departureTime { get; set; }
        [Display(Name = "Route ID")]
        public int routeId { get; set; }
        [Display(Name = "Booked seats")]
        public int bookedSeats { get; set; }
        [Display(Name = "Available seats")]
        public int availableSeats { get; set; }
    }
}