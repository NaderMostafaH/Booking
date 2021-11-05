using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Models.Dtos
{
    public class ReservationDt
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }
        public string Notes { get; set; }
    }
}
