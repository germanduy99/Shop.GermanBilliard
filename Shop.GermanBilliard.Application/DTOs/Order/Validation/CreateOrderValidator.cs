using FluentValidation;
using Shop.GermanBilliard.Application.Contracts.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Application.DTOs.Order.Validation
{
    public class CreateOrderValidator : AbstractValidator<CreateOrder>
    {
        private readonly ICueRepositoty _cueRepositoty;
        public CreateOrderValidator(ICueRepositoty cueRepositoty)
        {
            _cueRepositoty = cueRepositoty;
            RuleFor(order => order.ListCue)
                .NotNull().WithMessage("List of cues cannot be null.")
                .MustAsync(async (listCue, cancellation) => await CueExists(listCue)).WithMessage("One of cues not in exits or quantity must more than 0");
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

        private async Task<bool> CueExists(List<CueOrder> listCue)
        {
            foreach (var cue in listCue)
            {
                if (!await _cueRepositoty.Exists(cue.Id))
                {
                    return false;
                }
                if(cue.Quantity <= 0)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
