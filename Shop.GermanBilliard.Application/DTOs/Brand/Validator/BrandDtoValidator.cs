using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Application.DTOs.Brand.Validator
{
    public class BrandDtoValidator : AbstractValidator<BrandDto>
    {
        public BrandDtoValidator()
        {
            RuleFor(brand => brand.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(brand => brand.Country).NotEmpty().WithMessage("Country is required.");
        }
    }
}
