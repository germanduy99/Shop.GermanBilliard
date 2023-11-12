using MediatR;
using Shop.GermanBilliard.Application.DTOs.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Application.Features.Brand.Requests.Queries
{
    public class GetBrandDetailRequest : IRequest<BrandDto>
    {
        public int Id { get; set; }
    }
}
