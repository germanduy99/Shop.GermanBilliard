using MediatR;
using Shop.GermanBilliard.Application.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Application.Features.Order.Requests.Queries
{
    public class GetOrderDetailRequest : IRequest<OrderDto>
    {
        public int Id { get; set; }
    }
}
