using MediatR;
using Shop.GermanBilliard.Application.DTOs.Order;
using Shop.GermanBilliard.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Application.Features.Order.Requests.Commands
{
    public class CreateOrderCommand : IRequest<BaseCommandResponse>
    {
        public CreateOrder Order { get; set; }
    }
}
