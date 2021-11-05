using Booking.DataAccess.Data;
using Booking.DataAccess.Repository.IRepository;
using Booking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DataAccess.Repository
{
    public class ReservationRepository : RepositoryAsync<Reservation>, IReservationRepository
    {
        private readonly ApplicationDbContext _db;
        public ReservationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void update(Reservation reservation)
        {
            var objFromDb = _db.Reservation.FirstOrDefault(s => s.Id == reservation.Id);
            if (objFromDb != null)
            {
                //_db.Update(reservation);
                objFromDb.CustomerName = reservation.CustomerName;
                objFromDb.ReservationDate = reservation.ReservationDate;
                objFromDb.CreationDate = reservation.ReservationDate;
                objFromDb.Notes = reservation.Notes;


                if (reservation.TripId != 0)
                {
                    objFromDb.TripId = reservation.TripId;
                }
                //if (reservation.Trip != null)
                //{
                //    objFromDb.TripId = reservation.Trip.Id;
                //}
                
                if (reservation.UserId != 0)
                {
                    objFromDb.UserId = reservation.UserId;
                }
                //if (reservation.User != null)
                //{
                //    objFromDb.UserId = reservation.User.Id;
                //}
               
            }
            

        }
    }
}
