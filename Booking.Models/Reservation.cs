using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Models
{
    public class Reservation
    {
        public int Id {  get; set; }
        [Required(ErrorMessage ="Customer Name Required")]
        [Display(Name ="Customer")]
        public string CustomerName {  get; set; }
        [Display(Name = "Reservation Date")]
        public DateTime ReservationDate { get; set; }
        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }
        [MaxLength(200)]
        public string Notes { get; set; }
        
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        [Display(Name = "User Email")]
        public User User { get; set; }
        
        public int TripId { get; set; }
        [ForeignKey("TripId")]
        [Display(Name = "Trip Name")]
        public Trip Trip { get; set; }

    }
}
