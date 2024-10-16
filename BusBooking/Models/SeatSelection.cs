using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace BusBooking.Models
{
    public class SeatSelection
    {
        [Display(Name = "Registration ID")]
        public int RegId { get; set; }
        [Display(Name = "Booking ID")]
        public int bookingId { get; set; }
        [Display(Name = "Bus ID")]
        public int busId { get; set; }
        [Display(Name = "Bus name")]
        public string busName { get; set; }
        [Display(Name = "Schedule ID")]
        public int scheduleId { get; set; }
        [Display(Name = "First name")]
        public string firstName { get; set; }
        [Display(Name = "Last name")]
        public string lastName { get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Mobile Number")]
        public long mobileNumber { get; set; }
        [Display(Name = "Travel Date")]
        public DateTime travelDate { get; set; }
        [Display(Name = "Origin")]
        public string origin { get; set; }
        [Display(Name = "Destination")]
        public string destination { get; set; }
        [Display(Name = "Fare")]
        public float fare { get; set; }
        [Display(Name = "Aadhar Number")]
        public string aadharNo { get; set; }
        [Display(Name = "Mode of payment")]
        public string modeOfPayment { get; set; }
        [Display(Name = "No.of Seats")]
        public int totalSeatsBooked { get; set; }
        [Display(Name = "Cancellation Status")]
        public string cancellationStatus { get; set; }
    }
}