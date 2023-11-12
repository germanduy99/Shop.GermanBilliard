using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Application.Features.Cue.Requests.Commands
{
    public class DeleteCueCommand : IRequest
    {
        public int Id { get; set; }
    }
}
