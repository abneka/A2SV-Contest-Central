using System.Globalization;
using System.Net.Mail;
using Application.Contracts.Persistence;
using Application.DTOs.User;
using CsvHelper;
using Domain.Entities;
using MediatR;

namespace Application.Features.User.Commands.AddUserUsingCsvFile
{
    public class AddUserUsingCsvFileCommandHandler : IRequestHandler<AddUserUsingCsvFileCommand, IReadOnlyList<InvalidUserRecord>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddUserUsingCsvFileCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }
        public async Task<IReadOnlyList<InvalidUserRecord>> Handle(AddUserUsingCsvFileCommand command, CancellationToken cancellationToken)
        {
            // Define the expected header fields
            var expectedFields = new List<string>
            {
                "FirstName", "LastName", "Email", "Username", "CodeforcesHandle",
                "Phone", "Gender", "Password", "Role", "Group"
            };

            using var streamReader = new StreamReader(command.UserFile.OpenReadStream());
            using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);

            // Get the actual header fields from the CSV file
            csvReader.ReadHeader();
            var actualFields = csvReader.HeaderRecord;


            // Check if all expected fields are present in the actual header fields
            if (!expectedFields.All(field => actualFields.Contains(field)))
            {
                throw new Exception("CSV file is missing or contains incorrect header fields.");
            }

            var users = new List<UserEntity>();
            var invalidUserRecords = new List<InvalidUserRecord>();

            while (csvReader.Read())
            {
                var firstName = csvReader.GetField<string>("FirstName") ?? string.Empty;
                var lastName = csvReader.GetField<string>("LastName") ?? string.Empty;
                var email = csvReader.GetField<string>("Email") ?? string.Empty;
                var username = csvReader.GetField<string>("Username") ?? string.Empty;
                var codeforcesHandle = csvReader.GetField<string>("CodeforcesHandle") ?? string.Empty;
                var phone = csvReader.GetField<string>("Phone") ?? string.Empty;
                var gender = csvReader.GetField<string>("Gender") ?? string.Empty;
                var password = csvReader.GetField<string>("Password") ?? string.Empty;
                var role = csvReader.GetField<string>("Role") ?? string.Empty;
                var group = csvReader.GetField<string>("Group") ?? string.Empty;

                // Check if any required field is empty
                if (string.IsNullOrWhiteSpace(firstName) ||
                    string.IsNullOrWhiteSpace(lastName) ||
                    string.IsNullOrWhiteSpace(codeforcesHandle) ||
                    string.IsNullOrWhiteSpace(phone) ||
                    string.IsNullOrWhiteSpace(gender) ||
                    string.IsNullOrWhiteSpace(password) ||
                    string.IsNullOrWhiteSpace(role) ||
                    string.IsNullOrWhiteSpace(group) ||
                    (string.IsNullOrWhiteSpace(email) && !await IsValidEmailAsync(email)) ||
                    (string.IsNullOrWhiteSpace(username) && !await IsValidUsernameAsync(username)))
                {
                    // If any required field is missing or invalid, add the user to the invalid users list
                    var invalidUserRecord = new InvalidUserRecord
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        Username = username,
                        CodeforcesHandle = codeforcesHandle,
                        Phone = phone,
                        Gender = gender,
                        Password = password,
                        Role = role,
                        Group = group
                    };

                    invalidUserRecords.Add(invalidUserRecord);
                    continue;
                }

                var user = new UserEntity
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        UserName = username,
                        CodeforcesHandle = codeforcesHandle,
                        Phone = phone,
                        Gender = gender,
                        Password = password,
                        IsVerified = true,
                    };

                user.UserTypeId = await _unitOfWork.UserTypeRepository.GetUserTypeIdByUserTypeName(role);
                user.GroupId = await _unitOfWork.A2SVGroupRepository.GetGroupIdByGroupName("Group-" + group);

                users.Add(user);
            }
            
            await _unitOfWork.UserRepository.CreateListAsync(users);

            return invalidUserRecords;
        }

        // Validation for Username
        public async Task<bool> IsValidUsernameAsync(string username)
        {
            // Username should not be empty and should contain only alphanumeric characters
            // You can customize this validation based on your specific requirements
            if (string.IsNullOrEmpty(username) || !username.All(char.IsLetterOrDigit))
            {
                return false;
            }

            // Check if the username already exists in the database
            var existingUser = await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);
            return existingUser == null;
        }

        // Validation for Email
        public async Task<bool> IsValidEmailAsync(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);

                // Check that the domain part is exactly "a2sv.org"
                if (mailAddress.Host != "a2sv.org")
                {
                    return false;
                }

                // Check each part of the local part
                var emailParts = mailAddress.User.Split('.');
                foreach (var part in emailParts)
                {
                    // Ensure each part contains at least one alphanumeric character
                    if (string.IsNullOrEmpty(part) || !part.Any(char.IsLetterOrDigit))
                    {
                        return false;
                    }
                }

                // Check if the email already exists in the database
                var existingUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(email);
                return existingUser == null;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
