using AutoMapper;
using MediatR;
using Shop.GermanBilliard.Application.Contracts.Infrastructure;
using Shop.GermanBilliard.Application.DTOs.Cue;
using Shop.GermanBilliard.Application.Features.Cue.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Application.Features.Cue.Handlers.Queries
{
    public class GetCueListRequestHandler : IRequestHandler<GetCueListRequest, List<CueDto>>
    {
        private readonly ICueRepositoty _cueRepositoty;
        private readonly IMapper _mapper;

        public GetCueListRequestHandler(ICueRepositoty cueRepositoty, IMapper mapper)
        {
            _cueRepositoty = cueRepositoty;
            _mapper = mapper;
        }

        public async Task<List<CueDto>> Handle(GetCueListRequest request, CancellationToken cancellationToken)
        {
            var cues = await _cueRepositoty.GetAll();
            return _mapper.Map<List<CueDto>>(cues);
        }
    }
}
