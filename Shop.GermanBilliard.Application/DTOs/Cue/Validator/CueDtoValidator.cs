using FluentValidation;
using Shop.GermanBilliard.Application.Contracts.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Application.DTOs.Cue.Validator
{
    public class CueDtoValidator : AbstractValidator<CueDto>
    {
        private readonly IBrandRepository _brandRepository;
        public CueDtoValidator(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
            RuleFor(cue => cue.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(cue => cue.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
            RuleFor(cue => cue.Sale)
                .GreaterThanOrEqualTo(0)
                .LessThan(100)
                .WithMessage("Sale must be greater than or equal to 0 and less than 100.");
            RuleFor(cue => cue.BrandId)
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    var brand = await _brandRepository.Exists(id);
                    return brand;
                })
                .WithMessage("Not find brand");
        }
    }
}
