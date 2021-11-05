using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Models.Dtos
{
    public class ReservationForUserDto
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }
        public DateTime ReservationDate { get; set; }
        public string UserEmail { get; set; }

    }
}
