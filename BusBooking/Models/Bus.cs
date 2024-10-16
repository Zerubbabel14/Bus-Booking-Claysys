using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusBooking.Models
{
    public class Bus
    {
        [Display(Name = "Bus ID")]
        public int BusId { get; set; }
        [Display(Name = "Bus No")]
        public string BusNo { get; set; }
        [Display(Name = "Bus Type")]
        public string BusType { get; set; }
        [Display(Name = "Seats(Row)")]
        public int SeatRow { get; set; }
        [Display(Name = "Seats(Column)")]
        public int SeatColumn { get; set; }
        [Display(Name = "Origin")]
        public string Origin { get; set; }
        [Display(Name = "Destination")]
        public string Destination { get; set; }
        [Display(Name = "Bus Name")]
        public string BusName { get; set; }
        
    }
}