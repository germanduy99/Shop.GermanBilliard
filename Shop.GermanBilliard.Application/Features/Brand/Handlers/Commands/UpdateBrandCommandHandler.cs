using AutoMapper;
using MediatR;
using Shop.GermanBilliard.Application.Contracts.Infrastructure;
using Shop.GermanBilliard.Application.DTOs.Brand.Validator;
using Shop.GermanBilliard.Application.Exceptions;
using Shop.GermanBilliard.Application.Features.Brand.Requests.Commands;
using Shop.GermanBilliard.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Application.Features.Brand.Handlers.Commands
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBrandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var respond = new BaseCommandResponse();
            var validator = new BrandDtoValidator();
            var validatorResult = await validator.ValidateAsync(request.BrandDto);

            if (validatorResult.IsValid == false)
            {
                respond.Success = false;
                respond.Message = "Update Failed";
                respond.Errors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList();
            }

            var brand = _mapper.Map<Shop.GermanBilliard.Domain.Brand>(request.BrandDto);
            brand = await _unitOfWork.BrandRepository.Get(brand.Id);

            if(brand == null)
            {
                throw new NotFoundException(nameof(brand), request.BrandDto.Id);
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.BrandRepository.Update(brand);
                await _unitOfWork.CommitAsync();

                respond.Success = true;
                respond.Message = "Update Successful";
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
