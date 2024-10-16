using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusBooking.Models
{
    public class User
    {
        [Display(Name = "First name")]
        public string Firstname{ get; set;}
        [Display(Name = "Last name")]
        public string Lastname{ get; set;}
        [Display(Name = "Email")]
        [Required(ErrorMessage = "First name is required")]
        public string Email{ get; set;}
        [Display(Name = "Password")]
        public string Password{ get; set;}
        [Display(Name = "Mobile Number")]
        public long Mobilenumber{ get; set;}
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set;}
        [Display(Name = "Age")]
        public int Age { get; set;}
        [Display(Name = "State")]
        public string State { get; set;}
        [Display(Name = "City")]
        public string City{ get; set;}
        [Display(Name = "Address")]
        public string Address{ get; set;}
        [Display(Name = "Origin")]
        public string fromPlace { get; set; }
        [Display(Name = "Destination")]
        public string toPlace { get; set; }
        [Display(Name = "Travel date")]
        public DateTime travelDate { get; set;}
        [Display(Name = "Background URL")]
        public string BackgroundUrl { get; set; }
    }
}