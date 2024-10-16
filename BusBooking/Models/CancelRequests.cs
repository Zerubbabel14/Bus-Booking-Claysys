using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusBooking.Models
{
    public class CancelRequests
    {
        [Display(Name = "Request ID")]
        public int requestId{ get; set; }
        [Display(Name = "Booking ID")]
        public int bookingId {  get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "Request Date")]
        public string requestDate {  get; set; }
        [Display(Name = "Cancel Status")]
        public string cancelStatus {  get; set; }
    }
}