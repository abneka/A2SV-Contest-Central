using Application.Contracts.Persistence;
using Application.DTOs.User;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Features.User.Commands.AddCoverPicture;

public class AddProfilePictureCommandHandler : IRequestHandler<AddCoverPictureCommand, UserResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddProfilePictureCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserResponseDto> Handle(AddCoverPictureCommand request, CancellationToken cancellationToken)
    {

        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            throw new NotFoundException(nameof(user), request.UserId);
        }
        
        var url = await _unitOfWork.FileUpload.UploadImage(request.ImageFile, "UserCoverPictures");

        user.CoverPicture = url;
        await _unitOfWork.UserRepository.UpdateAsync(user.Id, user);

        return _mapper.Map<UserResponseDto>(user);
    }
}