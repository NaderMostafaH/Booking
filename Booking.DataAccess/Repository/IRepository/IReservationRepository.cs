using Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DataAccess.Repository.IRepository
{
    public interface IReservationRepository : IRepositoryAsync<Reservation>
    {
        void update(Reservation reservation);
    }
}
