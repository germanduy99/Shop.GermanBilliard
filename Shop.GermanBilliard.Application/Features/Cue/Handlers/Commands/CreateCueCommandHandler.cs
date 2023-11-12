using AutoMapper;
using MediatR;
using Shop.GermanBilliard.Application.Contracts.Infrastructure;
using Shop.GermanBilliard.Application.DTOs.Cue.Validator;
using Shop.GermanBilliard.Application.Exceptions;
using Shop.GermanBilliard.Application.Features.Cue.Requests.Commands;
using Shop.GermanBilliard.Application.Responses;
using Shop.GermanBilliard.Domain;

namespace Shop.GermanBilliard.Application.Features.Cue.Handlers.Commands
{
    public class CreateCueCommandHandler : IRequestHandler<CreateCueCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCueCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateCueCommand request, CancellationToken cancellationToken)
        {
            var respond = new BaseCommandResponse();
            var validator = new CueDtoValidator(_unitOfWork.BrandRepository);
            var validatorResult = await validator.ValidateAsync(request.CueDto);

            if (validatorResult.IsValid == false)
            {
                respond.Success = false;
                respond.Message = "Creation Failed";
                respond.Errors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList();
                return respond;
            }

            var cue = _mapper.Map<Shop.GermanBilliard.Domain.Cue>(request.CueDto);

            try
            {
                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.CueRepositoty.Add(cue);
                await _unitOfWork.CommitAsync();

                respond.Success = true;
                respond.Message = "Creation Successful";
                respond.Id = cue.Id;

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
