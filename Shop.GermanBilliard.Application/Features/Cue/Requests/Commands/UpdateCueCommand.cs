using MediatR;
using Shop.GermanBilliard.Application.DTOs.Cue;
using Shop.GermanBilliard.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Application.Features.Cue.Requests.Commands
{
    public class UpdateCueCommand : IRequest<BaseCommandResponse>
    {
        public CueDto CueDto { get; set; }
    }

}
