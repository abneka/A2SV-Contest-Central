
using MediatR;
using AutoMapper;
using Application.DTOs.Contest;
using Application.Contracts.Persistence;
using Application.Features.Contest.Queries.GetAll;
using Application.DTOs.Common;

namespace Application.Features.Contest.Queries.GetSingleContest;

public class GetAllContestsRequestHandler : IRequestHandler<GetAllContestsRequest,PaginatedResult<ContestResponseDto>>
{
    public readonly IMapper _mapper;
    public readonly IUnitOfWork _unitOfWork;
    public GetAllContestsRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedResult<ContestResponseDto>> Handle(GetAllContestsRequest request, CancellationToken cancellationToken)
    {
        // Calculate the number of items to skip based on the page and pageSize
        int skip = (request.Page - 1) * request.PageSize;

        // Retrieve paginated data from the repository
        var contests = await _unitOfWork.ContestRepository.GetPagedEntitiesAsync(skip, request.PageSize);

        // Map the entities to DTOs
        var contestsDto = _mapper.Map<IReadOnlyList<ContestResponseDto>>(contests);

        // Get the total number of items without pagination
        int totalItems = await _unitOfWork.ContestRepository.GetTotalEntitiesCount();

        // Calculate the total number of pages
        int totalPages = (int)Math.Ceiling((double)totalItems / request.PageSize);

        // Return paginated result along with metadata
        var paged_contests = new PaginatedResult<ContestResponseDto>
        {
            Data = contestsDto,
            TotalItems = totalItems,
            CurrentPage = request.Page,
            PageSize = request.PageSize,
            TotalPages = totalPages
        };

        return paged_contests;
    }
}