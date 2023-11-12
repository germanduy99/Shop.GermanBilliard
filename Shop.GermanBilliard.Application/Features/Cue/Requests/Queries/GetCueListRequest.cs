﻿using MediatR;
using Shop.GermanBilliard.Application.DTOs.Cue;


namespace Shop.GermanBilliard.Application.Features.Cue.Requests.Queries
{
    public class GetCueListRequest : IRequest<List<CueDto>>
    {
    }
}
