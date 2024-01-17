// using Application.Contracts.Persistence;
// using Application.Contracts.Persistence.Auth;
// using Application.DTOs.Auth;
// using AutoMapper;
// using FluentValidation;
// using MediatR;

// namespace Application.Features.Auth.ResetPassword;

// public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, string>
// {
//     private readonly IUserRepository _userRepository;
//     private readonly IMapper _mapper;
//     private readonly IAuth _auth;
    
//     public ResetPasswordCommandHandler(IUserRepository userRepository, IAuth auth, IMapper mapper)
//     {
//         _userRepository = userRepository;
//         _auth = auth;
//         _mapper = mapper;
//     }
    
    
//     public async Task<string> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
//     {
//         var validator = new ResetPasswordCommandValidator(_userRepository);
//         var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
//         if (!validationResult.IsValid)
//         {
//             throw new ValidationException(validationResult.Errors);
//         }
        
//         var user = await _userRepository.GetUserByEmail(request.ForgetPasswordRequest.Email);
        
//         if (user!.ConfirmationCodeExpiration < DateTime.UtcNow)
//         {
//             throw new Exception("Confirmation code is expired");
//         }
        
//         if (user.ConfirmationCode != request.ForgetPasswordRequest.ConfirmationCode)
//         {
//             throw new Exception("Confirmation code is invalid");
//         }

//         user.Password = BCrypt.Net.BCrypt.HashPassword(request.ForgetPasswordRequest.NewPassword);
//         await _userRepository.UpdateAsync(user.Id, user);

//         var response = await _auth.Login(new AuthRequest()
//         {
//             Email = user.Email,
//             Password = request.ForgetPasswordRequest.NewPassword
//         });
        
//         return response.Token;
//     }
// }