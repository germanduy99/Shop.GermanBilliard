using AutoMapper;
using MediatR;
using Shop.GermanBilliard.Application.Contracts.Infrastructure;
using Shop.GermanBilliard.Application.DTOs.ItemOrder;
using Shop.GermanBilliard.Application.DTOs.Order;
using Shop.GermanBilliard.Application.Exceptions;
using Shop.GermanBilliard.Application.Features.Order.Requests.Queries;
using Shop.GermanBilliard.Domain;

namespace Shop.GermanBilliard.Application.Features.Order.Handlers.Queries
{
    public class GetOrderDetailRequestHandler : IRequestHandler<GetOrderDetailRequest, OrderDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOrderDetailRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(GetOrderDetailRequest request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.OrderRepository.Get(request.Id);

            if (order == null)
            {
                throw new NotFoundException(nameof(order), request.Id);
            }

            var orderItems = await _unitOfWork.OrderItemRepository.FindByOrder(request.Id);

            var listOrderItemDto = _mapper.Map<List<OrderItemDto>>(orderItems);
            var orderDto = _mapper.Map<OrderDto>(order);

            orderDto.Items = listOrderItemDto;

            return orderDto;
        }
    }
}
