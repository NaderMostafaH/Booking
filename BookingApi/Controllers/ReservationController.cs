using Booking.DataAccess.Repository.IRepository;
using Booking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookingApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ReservationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReservationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _unitOfWork.Reservation.GetAllAsync(includeProperties:"Trip,User"));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error While retrieving data from database");
            }
            
        }
        [HttpGet("id:int")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var reservationFromDb = await _unitOfWork.Reservation.GetFirstOrDefaultAsync(i => i.Id == id , includeProperties: "User,Trip");
                if (reservationFromDb == null)
                {
                    return NotFound();
                }
                return Ok(reservationFromDb);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error While retrieving data from database");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(Reservation reservation )
        {
            try
            {
                if (reservation == null)
                    return BadRequest();

                 await _unitOfWork.Reservation.AddAsync(reservation);
                _unitOfWork.Save();
                return CreatedAtAction(nameof(Get), new { id = reservation.Id }, reservation);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error Creating New Reservation");
            }
        }
        [HttpPut("{id:int}")]
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
                return CreatedAtAction(nameof(Get), new { id = reservation.Id }, reservation);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error Updating Reservation");
            }
        }
        [HttpDelete("{id:int}")]
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
