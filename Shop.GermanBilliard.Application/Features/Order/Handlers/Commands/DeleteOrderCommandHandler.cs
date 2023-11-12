using AutoMapper;
using MediatR;
using Shop.GermanBilliard.Application.Contracts.Infrastructure;
using Shop.GermanBilliard.Application.Exceptions;
using Shop.GermanBilliard.Application.Features.Cue.Requests.Commands;
using Shop.GermanBilliard.Application.Features.Order.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Application.Features.Order.Handlers.Commands
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public  async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.OrderRepository.Get(request.Id);

            if (order == null)
            {
                throw new NotFoundException(nameof(order), request.Id);
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.OrderItemRepository.DeleteByOrder(request.Id);
                await _unitOfWork.OrderRepository.Delete(order);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new BadRequestException(ex.Message);
            }

            return Unit.Value;
        }
    }
}
