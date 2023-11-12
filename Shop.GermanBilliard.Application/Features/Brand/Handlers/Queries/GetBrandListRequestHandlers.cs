using AutoMapper;
using MediatR;
using Shop.GermanBilliard.Application.Contracts.Infrastructure;
using Shop.GermanBilliard.Application.DTOs.Brand;
using Shop.GermanBilliard.Application.DTOs.Cue;
using Shop.GermanBilliard.Application.Features.Brand.Requests.Queries;
using Shop.GermanBilliard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Application.Features.Brand.Handlers.Queries
{
    public class GetBrandListRequestHandlers : IRequestHandler<GetBrandListRequest, List<BrandDto>>
    {
        public readonly IBrandRepository _brandRepository;
        public readonly IMapper _mapper;

        public GetBrandListRequestHandlers(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<List<BrandDto>> Handle(GetBrandListRequest request, CancellationToken cancellationToken)
        {
            var brands = await _brandRepository.GetAll();
            return _mapper.Map<List<BrandDto>>(brands);
        }
    }
}
