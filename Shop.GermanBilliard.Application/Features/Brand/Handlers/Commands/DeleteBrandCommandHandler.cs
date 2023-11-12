using AutoMapper;
using MediatR;
using Shop.GermanBilliard.Application.Contracts.Infrastructure;
using Shop.GermanBilliard.Application.Exceptions;
using Shop.GermanBilliard.Application.Features.Brand.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Application.Features.Brand.Handlers.Commands
{
    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteBrandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.BrandRepository.Get(request.Id);

            if(brand == null)
            {
                throw new NotFoundException(nameof(brand),request.Id);
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.BrandRepository.Delete(brand);
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
