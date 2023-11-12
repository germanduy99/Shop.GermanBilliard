using AutoMapper;
using MediatR;
using Shop.GermanBilliard.Application.Contracts.Infrastructure;
using Shop.GermanBilliard.Application.Exceptions;
using Shop.GermanBilliard.Application.Features.Cue.Requests.Commands;
using Shop.GermanBilliard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.GermanBilliard.Application.Features.Cue.Handlers.Commands
{
    public class DeleteCueCommandHandler : IRequestHandler<DeleteCueCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteCueCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteCueCommand request, CancellationToken cancellationToken)
        {
            var cue = await _unitOfWork.CueRepositoty.Get(request.Id);

            if (cue == null)
            {
                throw new NotFoundException(nameof(cue), request.Id);
            }

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.CueRepositoty.Delete(cue);
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
