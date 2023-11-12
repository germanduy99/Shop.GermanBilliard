using AutoMapper;
using MediatR;
using Shop.GermanBilliard.Application.Contracts.Infrastructure;
using Shop.GermanBilliard.Application.DTOs.Order;
using Shop.GermanBilliard.Application.DTOs.Order.Validation;
using Shop.GermanBilliard.Application.Exceptions;
using Shop.GermanBilliard.Application.Features.Brand.Requests.Commands;
using Shop.GermanBilliard.Application.Features.Order.Requests.Commands;
using Shop.GermanBilliard.Application.Responses;


namespace Shop.GermanBilliard.Application.Features.Order.Handlers.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var respond = new BaseCommandResponse();
            var validator = new CreateOrderValidator(_unitOfWork.CueRepositoty);
            var validatorResult = await validator.ValidateAsync(request.Order);

            if (validatorResult.IsValid == false)
            {
                respond.Success = false;
                respond.Message = "Creation Failed";
                respond.Errors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList();
                return respond;
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var order = new Shop.GermanBilliard.Domain.Order
                {
                    Name = request.Order.Name,
                    Email = request.Order.Email,
                    Address = request.Order.Address,
                    Phone = request.Order.Phone,
                    OrderPlaced = DateTime.Now
                };
                order = await _unitOfWork.OrderRepository.Add(order);
                var total = 0;
                foreach (var cue in request.Order.ListCue)
                {
                    var orderItem = new Shop.GermanBilliard.Domain.OrderItem
                    {
                        Quantity = cue.Quantity,
                        Price = await PriceSale(cue),
                        CueId = cue.Id,
                        OrderId = order.Id,
                    };
                    total = (int) (total + orderItem.Price);
                    await _unitOfWork.OrderItemRepository.Add(orderItem);
                }
                order.OrderTotal = total;
                await _unitOfWork.OrderRepository.Update(order);

                await _unitOfWork.CommitAsync();

                respond.Success = true;
                respond.Message = "Creation Successful";
                respond.Id = order.Id;

            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new BadRequestException(ex.Message);
            }

            return respond;

        }

        private async Task<int> PriceSale(CueOrder cueOrder)
        {
            var price = 0;
            var cue = await _unitOfWork.CueRepositoty.Get(cueOrder.Id);
            if (cue.Sale > 0)
            {
                double sale = (double)((100 - cue.Sale) / 100.0);
                double realprice = ((double)(cue.Price * cueOrder.Quantity));
                price = (int)(realprice * sale);
            }
            else
            {
                price = (int)(cue.Price * cueOrder.Quantity);
            }
            return price;
        }
    }
}
