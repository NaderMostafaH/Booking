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
    public class ReservationsValidators : AbstractValidator<Reservation> ,  IEntityTypeConfiguration<Reservation>
    {
        public ReservationsValidators()
        {
           // RuleFor(r => r.CustomerName).NotEmpty().WithMessage("Custmer Name Required").MinimumLength(10).MaximumLength(100);
          //  RuleFor(r => r.Notes).NotEmpty().WithMessage("Custmer Name Required").MinimumLength(10).MaximumLength(100);
            
        }
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(p => p.Notes).IsRequired().HasMaxLength(150);
            builder.Property(s => s.CustomerName).HasMaxLength(100).IsRequired();

        }
    }
}
