using AutoMapper;
using MediatR;
using Shop.GermanBilliard.Application.Contracts.Infrastructure;
using Shop.GermanBilliard.Application.DTOs.Cue;
using Shop.GermanBilliard.Application.DTOs.Order;
using Shop.GermanBilliard.Application.Features.Cue.Requests.Queries;
using Shop.GermanBilliard.Application.Features.Order.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Application.Features.Order.Handlers.Queries
{
    public class GetOrderListRequestHandler : IRequestHandler<GetOrderListRequest, List<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderListRequestHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderDto>> Handle(GetOrderListRequest request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAll();
            return _mapper.Map<List<OrderDto>>(orders);
        }
    }
}
