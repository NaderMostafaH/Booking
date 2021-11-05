using AutoMapper;
using Booking.Models;
using Booking.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApi.Mapping
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {


            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Trip, TripDto>().ReverseMap();
            CreateMap<Reservation, ReservationListDto>().ReverseMap();
            CreateMap<Reservation, ReservationDetailsDto>().ReverseMap();
            CreateMap<Reservation, ReservationForUserDto>().ReverseMap();
        }
    }
}
