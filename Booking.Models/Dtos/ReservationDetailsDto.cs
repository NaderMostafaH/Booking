using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Models.Dtos
{
    public class ReservationDetailsDto
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }
        
        public DateTime ReservationDate { get; set; }

        public DateTime CreationDate { get; set; }

        public string Notes { get; set; }
        public string UserEmail { get; set; }
        public string TripName { get; set; }
    }
}
