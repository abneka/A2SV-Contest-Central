using System.Net.Http.Json;
using Application.Contracts.Infrastructure.ExternalServices;
using Application.Contracts.Persistence;
using FluentValidation;
using MediatR;

namespace Application.Features.CodeforcesApi.Queries.CheckContestUrl
{
    public class ContestUrlCheckerRequestHandler : IRequestHandler<ContestUrlCheckerRequest, bool>
    {
        private IUnitOfWork _unitOfWork;
        private ICodeforcesApiService _codeforcesApiService;

        public ContestUrlCheckerRequestHandler(
            ICodeforcesApiService codeforcesApiService,
            IUnitOfWork unitOfWork
        )
        {
            _unitOfWork = unitOfWork;
            _codeforcesApiService = codeforcesApiService;
        }

        public async Task<bool> Handle(ContestUrlCheckerRequest request, CancellationToken cancellationToken)
        {
            var validator = new ContestUrlCheckerRequestValidator(_unitOfWork, _codeforcesApiService);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return true;
        }
    }
}
