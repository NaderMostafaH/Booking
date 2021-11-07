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
        /// <summary>
        /// Get All Reservations From Db.
        /// </summary>
        /// <returns>All Reservation Records</returns>
        /// <remarks>
        /// Sample Response:
        /// {
        ///  "Id": int,
        ///  "CustomerName" : string
        ///  "ReservationDate": DateTime,
        ///  "UserEmail": string,
        ///  "TripName" : string
        ///  }
        /// </remarks>
        /// <response code="200">Returns the list of  reservations</response>
        /// <response code="500">Internal Server Error While Creating</response> 
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
        /// <summary>
        /// Get One Reservation Data From Db.
        /// </summary>
        /// <returns>One Single Row Of Reservation</returns>
        /// <returns> Reservation Record</returns>
        /// <remarks>
        /// Sample Response:
        /// {
        ///  "Id": int,
        ///  "CustomerName" : string
        ///  "ReservationDate": DateTime,
        ///  "CreationDateDate": DateTime,
        ///  "UserEmail": string,
        ///  "TripName" : string,
        ///  "Notes" : string
        ///  }
        /// </remarks>
        /// <param name="id"></param>
        /// <response code="200">Returns the reservation data</response>
        /// <response code="404">No record id in Db match parameter id</response> 
        /// <response code="500">Internal Server Error While Creating</response>
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
        /// <summary>
        /// Get One Reservation User Data From Db.
        /// </summary>
        /// <returns>One Single Row Of Reservation User Data</returns>
        /// <returns> Reservation User Data</returns>
        /// <remarks>
        /// Sample Response:
        /// {
        ///  "Id": int,
        ///  "CustomerName" : string
        ///  "ReservationDate": DateTime,
        ///  "UserEmail": string
        ///  }
        /// </remarks>
        /// <param name="id"></param>
        /// <response code="200">Returns the reservation data</response>
        /// <response code="404">No record id in Db match parameter id</response> 
        /// <response code="500">Internal Server Error While Creating</response>
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
        /// <summary>
        /// Create a Reservation Item.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /ReservationDto
        ///     {
        ///        "id": 0 -- Identity,
        ///         "customerName": "any name",
        ///         "reservationDate": "2021-11-06T12:20:32.502Z",
        ///         "creationDate": "2021-11-06T12:20:32.502Z",
        ///         "notes": "string",
        ///         "userId": 1,
        ///         "tripId": 2
        ///     }
        ///
        /// </remarks>
        /// <param name="reservationDto"></param>
        /// <returns>A newly created Reservation item</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>  
        /// <response code="500">Internal Server Error While Creating</response>  
        [HttpPost]
        [Route("/Create/")]
        public async Task<IActionResult> Create(ReservationDto reservationDto )
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                if (reservationDto == null)
                    return BadRequest();

                //Mapping
                var reservationToDb = _mapper.Map<Reservation>(reservationDto);
                 await _unitOfWork.Reservation.AddAsync(reservationToDb);
                _unitOfWork.Save();
                return CreatedAtAction(nameof(GetReservationUserData), new {id = reservationToDb.Id }, reservationToDb);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error Creating New Reservation");
            }
        }
        /// <summary>
        /// Update a Reservation Item.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        /// Put /ReservationDto
        ///         int "id" : 1,
        ///     {
        ///        "id": 1,
        ///         "customerName": "any name",
        ///         "reservationDate": "2021-11-06T12:20:32.502Z",
        ///         "creationDate": "2021-11-06T12:20:32.502Z",
        ///         "notes": "string",
        ///         "userId": 1,
        ///         "tripId": 2
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="reservationDto"></param>
        /// <returns>A newly Updated Reservation item</returns>
        /// <response code="201">Returns the newly Updated item</response>
        /// <response code="400">If the item is null</response>  
        /// <response code="404">If id that request not equal any record id in database</response> 
        /// /// <response code="500">Internal Server Error While Creating</response>  

        [HttpPut()]
        [Route("/Update/{id:int}")]
        public async Task<IActionResult> Update(int id , ReservationDto reservationDto)
        {
            try
            {
                if (id != reservationDto.Id)
                    return BadRequest("Reservetion ID Dismatch");

                var reservationToUpdate = await _unitOfWork.Reservation.GetAsync(reservationDto.Id);
                if (reservationToUpdate == null)
                {
                    return NotFound($"Reservation With ID {id} Not Found");
                }
                //Mapping
                var reservationToUpdateInDb = _mapper.Map<Reservation>(reservationDto);

                _unitOfWork.Reservation.update(reservationToUpdateInDb);
                _unitOfWork.Save();
                return CreatedAtAction(nameof(GetReservationUserData), new { id = reservationToUpdateInDb.Id }, reservationToUpdateInDb);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error Updating Reservation");
            }
        }
        /// <summary>
        /// Deletes a specific Reservation item.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A newly created Reservation item</returns>
        /// <remarks> This api for deleting one reservation from db</remarks>
        /// <response code="200">Returns item was Deleted </response>
        /// <response code="404">If no record in database notmatch with parameter id</response>  
        /// <response code="500">Internal Server Error While Deleting</response>  
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
