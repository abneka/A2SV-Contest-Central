using Application.Contracts.Persistence;
using Application.DTOs.OverallStatus;
using AutoMapper;
using MediatR;

namespace Application.Features.OverallStatus.Queries;

public class GetOverallStatusHandler : IRequestHandler<GetOverallStatusQuery, OverallStatusResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetOverallStatusHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<OverallStatusResponseDto> Handle(GetOverallStatusQuery request, CancellationToken cancellationToken)
    {
        // year is given in the format of 2023/24, we destructure the string and get two year values
        return new OverallStatusResponseDto();
    }
}