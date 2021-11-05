using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Booking.Models
{
    public class Trip
    {
        public int Id {  get; set; }
        public string Name {  get; set; }
        public string CityName {  get; set; }
        public decimal Price {  get; set; }
        public DateTime CreationDate {  get; set; }
        public string ImageUrl { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
