using AutoMapper;
using MediatR;
using Shop.GermanBilliard.Application.Contracts.Infrastructure;
using Shop.GermanBilliard.Application.DTOs.Order.Validation;
using Shop.GermanBilliard.Application.Exceptions;
using Shop.GermanBilliard.Application.Features.Order.Requests.Commands;
using Shop.GermanBilliard.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Application.Features.Order.Handlers.Commands
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {

            var respond = new BaseCommandResponse();
            var validator = new UpdateOrderValidator();
            var validatorResult = await validator.ValidateAsync(request.Order);

            if (validatorResult.IsValid == false)
            {
                respond.Success = false;
                respond.Message = "Update Failed";
                respond.Errors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList();
                return respond;
            }
            var order = await _unitOfWork.OrderRepository.Get(request.Order.Id);

            if (order == null)
            {
                throw new NotFoundException(nameof(order), request.Order.Id);
            }
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                order.Id = request.Order.Id;
                order.Name = request.Order.Name;
                order.Email = request.Order.Email;
                order.Address = request.Order.Address;

                await _unitOfWork.OrderRepository.Update(order);
               
                await _unitOfWork.CommitAsync();

                respond.Success = true;
                respond.Message = "Update Successful";
                respond.Id = order.Id;

            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new BadRequestException(ex.Message);
            }

            return respond;
        }
    }
}
