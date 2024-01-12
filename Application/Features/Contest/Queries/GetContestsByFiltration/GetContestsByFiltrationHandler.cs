using Application.Contracts.Persistence;
using Application.DTOs.Contest;
using Application.Features.Contest.Queries.Common;
using Application.Features.User.Queries.GetUsersByFiltration;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Contest.Queries.GetContestsByFiltration;

public class GetContestsByFiltrationHandler : IRequestHandler<GetContestsByFiltrationQuery, PaginatedContestResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetContestsByFiltrationHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginatedContestResponseDto> Handle(GetContestsByFiltrationQuery request,
        CancellationToken cancellationToken)
    {
        var skip = (request.Filter.PageNumber - 1) * request.Filter.PageSize;
        var result = await _unitOfWork.ContestRepository.GetContestsWithGroups();

        var contests = _mapper.Map<List<ContestResponseDto>>(result);
        
        // sort by createdDate in descending order
        var orderedContests = contests.OrderByDescending(c => c.StartTimeSeconds);

        var query = ContestFiltration.FilterByAny(orderedContests.AsQueryable(), request.Filter.SearchString);
        query = ContestFiltration.FilterByCountry(query, request.Filter.Country);
        query = ContestFiltration.FilterByGroup(query, request.Filter.Group);
        query = ContestFiltration.FilterByGeneration(query, request.Filter.Generation);
        query = ContestFiltration.FilterByLocation(query, request.Filter.Location);

        query = query.Skip(skip).Take(request.Filter.PageSize);
        
        // update each contest with participants and questions number
        foreach (var contest in query)
        {
            contest.ParticipantsNumber = contest.UserContestResults.Count;
            contest.QuestionsNumber = contest.Questions.Count;
        }

        return new PaginatedContestResponseDto
        {
            ContestsList = query.ToList(), ItemsCount = query.Count(), PageNumber = request.Filter.PageNumber
        };
    }
}