using Booking.Models.Dtos;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Models.Validators
{
    public class ReservationDtoValidators : AbstractValidator<ReservationDto> 
    {
        public ReservationDtoValidators()
        {
            RuleFor(r => r.CustomerName).NotEmpty().WithMessage("Custmer Name Required").MinimumLength(10).MaximumLength(100);
            RuleFor(r => r.Notes).NotEmpty().WithMessage("Custmer Name Required").MinimumLength(10).MaximumLength(100);
            
        }
       
    }
}
