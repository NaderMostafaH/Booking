using AutoMapper;
using Booking.DataAccess.Data;
using Booking.DataAccess.Repository.IRepository;
using Booking.Models;
using Booking.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ReservationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public ReservationController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
         

        }
        [HttpGet]
        [Route("/GetReservationsList")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var ReservationListFromDb = await _unitOfWork.Reservation.GetAllAsync(includeProperties: "User,Trip");
               
                // Mapping Reservation List To Response ReservationListDto
                var reservationListDto = _mapper.Map<List<ReservationListDto>>(ReservationListFromDb);
       
                return Ok(reservationListDto);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error While retrieving data from database");
            }
            
        }
        [HttpGet()]
        [Route("/GetReservationData/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var reservationFromDb = await _unitOfWork.Reservation.GetFirstOrDefaultAsync(i => i.Id == id, includeProperties: "User,Trip");
         
                if (reservationFromDb == null)
                {
                    return NotFound();
                }
                //Mapping To Response Dto
                var reservationListDto = _mapper.Map<ReservationDetailsDto>(reservationFromDb);
                
                return Ok(reservationListDto);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error While retrieving data from database");
            }
        }

        [HttpGet()]
        [Route("/GetReservationUserData/{id:int}")]
        public async Task<IActionResult> GetReservationUserData(int id)
        {

            try
            {
                var reservationFromDb = await _unitOfWork.Reservation.GetFirstOrDefaultAsync(i => i.Id == id, includeProperties: "User,Trip");
                if (reservationFromDb == null)
                {
                    return NotFound();
                }
                //Mapping Data To Response Dto
                var reservationRespnseDto = _mapper.Map<ReservationForUserDto>(reservationFromDb);
                
                return Ok(reservationRespnseDto);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error While retrieving data from database");
            }
        }
        [HttpPost]
        [Route("/Create/")]
        public async Task<IActionResult> Create(Reservation reservation )
        {
            try
            {
                if (reservation == null)
                    return BadRequest();

                 await _unitOfWork.Reservation.AddAsync(reservation);
                _unitOfWork.Save();
                return CreatedAtAction(nameof(GetReservationUserData), new { id = reservation.Id });
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error Creating New Reservation");
            }
        }
        [HttpPut()]
        [Route("/Update/{id:int}")]
        public async Task<IActionResult> Update(int id , Reservation reservation)
        {
            try
            {
                if (id != reservation.Id)
                    return BadRequest("Reservetion ID Dismatch");

                var reservationToUpdate = await _unitOfWork.Reservation.GetAsync(reservation.Id);
                if (reservationToUpdate == null)
                {
                    return NotFound($"Reservation With ID {id} Not Found");
                }
                  _unitOfWork.Reservation.update(reservation);
                _unitOfWork.Save();
                return CreatedAtAction(nameof(GetReservationUserData), new { id = reservation.Id }, reservation);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error Updating Reservation");
            }
        }
        [HttpDelete()]
        [Route("/Delete{id:int}/")]
        public async Task<IActionResult> Delete(int id)
        {

            try
            {
                var reservationToDelete = await _unitOfWork.Reservation.GetAsync(id);
                if (reservationToDelete == null)
                {
                    return NotFound($"Reservation with Id = {id} not found");
                }    
                await _unitOfWork.Reservation.RemoveAsync(id);
                _unitOfWork.Save();
                return Ok($"Reservation with Id = {id} deleted");
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error Delete Reservation");
            }
        }
    }
}
