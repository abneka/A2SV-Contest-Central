using Application.Contracts.Infrastructure.ExternalServices;
using Application.Contracts.Persistence;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Application.Features.CodeforcesApi.Commands
{
    public class FetchContestDataFromApiCommandHandler
        : IRequestHandler<FetchContestDataFromApiCommand, bool>
    {
        private readonly IFetchedDataProcessing _codeforcesApiService;
        private readonly IUnitOfWork _unitOfWork;

        public FetchContestDataFromApiCommandHandler(
            IUnitOfWork unitOfWork,
            IFetchedDataProcessing codeforcesApiService
        )
        {
            _unitOfWork = unitOfWork;
            _codeforcesApiService = codeforcesApiService;
        }

        public async Task<bool> Handle(
            FetchContestDataFromApiCommand command,
            CancellationToken cancellationToken
        )
        {
            var validator = new FetchContestDataFromApiCommandValidator(
                _unitOfWork,
                _codeforcesApiService
            );

            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            try
            {
                await _codeforcesApiService.FetchContestData("496477");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while fetching data from Codeforces", ex);
                throw new Exception(ex.Message);
            }
        }
    }
}
