using MediatR;
using Shop.GermanBilliard.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Application.Features.Brand.Requests.Commands
{
    public class DeleteBrandCommand : IRequest
    {
        public int Id { get; set; }
    }
}
