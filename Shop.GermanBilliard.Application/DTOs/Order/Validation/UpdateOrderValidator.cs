using FluentValidation;
using Shop.GermanBilliard.Application.Contracts.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Application.DTOs.Order.Validation
{
    public class UpdateOrderValidator  : AbstractValidator<UpdateOrder>
    {
        public UpdateOrderValidator()
        {
            RuleFor(order => order.Id).NotEmpty().WithMessage("Id is required.");
            RuleFor(order => order.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(order => order.Address).NotEmpty().WithMessage("Address is required.");
            RuleFor(order => order.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[0-9]*$").WithMessage("Invalid phone number format.");
            RuleFor(order => order.Email)
                .NotNull().WithMessage("Email cannot be null.")
                .NotEmpty().WithMessage("Email Email required.")
                .EmailAddress().WithMessage("Email must be a valid email address.");

        }
    }
}

