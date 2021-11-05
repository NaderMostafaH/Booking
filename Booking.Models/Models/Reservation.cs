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
        
        public string CustomerName {  get; set; }
        
        public DateTime ReservationDate { get; set; }
        
        public DateTime CreationDate { get; set; }
        
        public string Notes { get; set; }
        
        public int UserId { get; set; }
       
        public virtual User User { get; set; }
        
        public int TripId { get; set; }
       
        public virtual Trip Trip { get; set; }

    }
}
