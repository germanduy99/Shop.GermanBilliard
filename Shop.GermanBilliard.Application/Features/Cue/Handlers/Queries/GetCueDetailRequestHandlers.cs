using AutoMapper;
using MediatR;
using Shop.GermanBilliard.Application.Contracts.Infrastructure;
using Shop.GermanBilliard.Application.DTOs.Cue;
using Shop.GermanBilliard.Application.Exceptions;
using Shop.GermanBilliard.Application.Features.Cue.Requests.Queries;

namespace Shop.GermanBilliard.Application.Features.Cue.Handlers.Queries
{
    public class GetCueDetailRequestHandlers : IRequestHandler<GetCueDetailRequest, CueDto>
    {
        private readonly ICueRepositoty _cueRepositoty;
        private readonly IMapper _mapper;

        public GetCueDetailRequestHandlers(ICueRepositoty cueRepositoty, IMapper mapper)
        {
            _cueRepositoty = cueRepositoty;
            _mapper = mapper;
        }
        public async Task<CueDto> Handle(GetCueDetailRequest request, CancellationToken cancellationToken)
        {
            var cue = await _cueRepositoty.Get(request.Id);
            if(cue == null)
            {
                throw new NotFoundException(nameof(cue), request.Id);
            }
            return _mapper.Map<CueDto>(cue);
        }
    }
}
