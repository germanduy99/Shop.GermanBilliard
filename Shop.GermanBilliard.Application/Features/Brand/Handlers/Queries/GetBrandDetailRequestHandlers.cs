using AutoMapper;
using MediatR;
using Shop.GermanBilliard.Application.Contracts.Infrastructure;
using Shop.GermanBilliard.Application.DTOs.Brand;
using Shop.GermanBilliard.Application.DTOs.Cue;
using Shop.GermanBilliard.Application.Exceptions;
using Shop.GermanBilliard.Application.Features.Brand.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Application.Features.Brand.Handlers.Queries
{
    public class GetBrandDetailRequestHandlers : IRequestHandler<GetBrandDetailRequest, BrandDto>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public GetBrandDetailRequestHandlers(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<BrandDto> Handle(GetBrandDetailRequest request, CancellationToken cancellationToken)
        {
            var brand = await _brandRepository.Get(request.Id);
            if(brand == null)
            {
                throw new NotFoundException(nameof(brand),request.Id);
            }
            return _mapper.Map<BrandDto>(brand);
        }
    }
}
