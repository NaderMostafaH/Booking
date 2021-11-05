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
using AutoMapper;
using Booking.Models.Dtos;

namespace BookingMVC.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReservationsController(IUnitOfWork unitOfWork, IMapper mapper, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
       

        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var reservationList = await _unitOfWork.Reservation.GetAllAsync(includeProperties: "Trip,User");
            

            return View(reservationList);
        }
        public async Task<IActionResult> ReservationsList()
        {
            var reservationList = await _unitOfWork.Reservation.GetAllAsync(includeProperties: "Trip,User");

            // Mapping Reservation List To Response ReservationListDto
            var reservationListDto = _mapper.Map<List<ReservationListDto>>(reservationList);

            return View(reservationListDto);
        }
        public async Task<IActionResult> Details(int id)
        {
            var reservationFromDb= await _unitOfWork.Reservation.GetFirstOrDefaultAsync(r => r.Id == id ,includeProperties: "Trip,User");

            // Mapping Reservation List To Response ReservationListDto
            var reservationDetailDto = _mapper.Map<ReservationDetailsDto>(reservationFromDb);

            return View(reservationDetailDto);
        }

    }   
}
