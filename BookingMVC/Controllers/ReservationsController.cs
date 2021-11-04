using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Booking.DataAccess.Data;
using Booking.Models;
using Booking.DataAccess.Repository.IRepository;

namespace BookingMVC.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReservationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var reservationList = await _unitOfWork.Reservation.GetAllAsync(includeProperties: "Trip,User");

            return View(reservationList);
        }

    }   
}
