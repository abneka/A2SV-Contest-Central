using System.Globalization;
using Application.Contracts.Persistence;
using CsvHelper;
using Domain.Entities;
using MediatR;

namespace Application.Features.User.Commands.AddUserUsingCsvFile
{
    public class AddUserUsingCsvFileCommandHandler : IRequestHandler<AddUserUsingCsvFileCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddUserUsingCsvFileCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }
        public async Task<Unit> Handle(AddUserUsingCsvFileCommand command, CancellationToken cancellationToken)
        {
            using var streamReader = new StreamReader(command.UserFile.OpenReadStream());
            using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);

            var users = new List<UserEntity>();
            while (csvReader.Read())
            {
                var user = new UserEntity
                {
                    FirstName = csvReader.GetField<string>("FirstName") ?? string.Empty,
                    LastName = csvReader.GetField<string>("LastName") ?? string.Empty,
                    Email = csvReader.GetField<string>("Email") ?? string.Empty,
                    UserName = csvReader.GetField<string>("Username") ?? string.Empty,
                    CodeforcesHandle = csvReader.GetField<string>("CodeforcesHandle") ?? string.Empty,
                    Phone = csvReader.GetField<string>("Phone") ?? string.Empty,
                    Gender = csvReader.GetField<string>("Gender") ?? string.Empty,
                    Password = csvReader.GetField<string>("Password") ?? string.Empty, 
                    IsVerified = true, 
                    UserTypeId = Guid.NewGuid(), 
                    GroupId = Guid.NewGuid() 
                };
                string user_type = csvReader.GetField<string>("Role") ?? string.Empty;
                user.UserTypeId = await _unitOfWork.UserTypeRepository.GetUserTypeIdByUserTypeName(user_type);

                string group_name = csvReader.GetField<string>("Group") ?? string.Empty;
                user.GroupId = await _unitOfWork.A2SVGroupRepository.GetGroupIdByGroupName(group_name);

                users.Add(user);
            }
            
            await _unitOfWork.UserRepository.CreateListAsync(users);

            return Unit.Value;
        }
    }
}


