using AutoMapper;
using MediatR;
using Shop.GermanBilliard.Application.Contracts.Infrastructure;
using Shop.GermanBilliard.Application.DTOs.Brand.Validator;
using Shop.GermanBilliard.Application.Exceptions;
using Shop.GermanBilliard.Application.Features.Brand.Requests.Commands;
using Shop.GermanBilliard.Application.Responses;
using Shop.GermanBilliard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Application.Features.Brand.Handlers.Commands
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBrandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var respond = new BaseCommandResponse();
            var validator = new BrandDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.BrandDto);

            if (validatorResult.IsValid == false)
            {
                respond.Success = false;
                respond.Message = "Creation Failed";
                respond.Errors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList();
                return respond;
            }

            var brand = _mapper.Map<Shop.GermanBilliard.Domain.Brand>(request.BrandDto);

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.BrandRepository.Add(brand);
                await _unitOfWork.CommitAsync();

                respond.Success = true;
                respond.Message = "Creation Successful";
                respond.Id = brand.Id;

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
