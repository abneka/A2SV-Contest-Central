using Application.Contracts.Persistence;
using Application.DTOs.Contest;
using Application.Features.Contest.Queries.Common;
using AutoMapper;
using MediatR;

namespace Application.Features.Contest.Queries;

public class GetContestLeaderboardHandler : IRequestHandler<GetContestLeaderboardRequest, ContestWithGraphsDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetContestLeaderboardHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ContestWithGraphsDto> Handle(GetContestLeaderboardRequest request, CancellationToken cancellationToken)
    {
        var skip = (request.Filter.PageNumber - 1) * request.Filter.PageSize;
        
        // todo: filter the leaderboard using the request.filter provided
        var contestLeaderboard = await _unitOfWork.ContestRepository.GetContestLeaderboard(request.ContestId);
        var contestLeaderboardDto = _mapper.Map<List<UserContestAndQuestionDto>>(contestLeaderboard);
        
        var contestLeaderboardQuery = FilterByCountry(contestLeaderboardDto.AsQueryable(), request.Filter.Country);
        contestLeaderboardQuery = FilterByGroup(contestLeaderboardQuery, request.Filter.Group);
        contestLeaderboardQuery = FilterByGeneration(contestLeaderboardQuery, request.Filter.Generation);
        contestLeaderboardQuery = FilterByLocation(contestLeaderboardQuery, request.Filter.Location);
        
        var contestLeaderboardFiltered = contestLeaderboardQuery
            .Skip(skip)
            .Take(request.Filter.PageSize)
            .ToList();
        
        // todo: get the bar graph data from contestLeaderboard
        var barGraphData = new List<GraphDataPointsDto>();
        
        // get questions from contestLeaderboard
        var questions = contestLeaderboardFiltered.SelectMany(ucq => ucq.Questions).Distinct().ToList();
        
        // get the number of students that did the question
        foreach (var question in questions)
        {
            var questionIndex = question.Index;
            // count the number of uses for questionIndex that have the questionSolved = true
            var questionSolvedCount = contestLeaderboardFiltered.Count(ucq => ucq.Questions.Any(q => q.Index == questionIndex && q.Solved));
            
            var graphDataPoint = new GraphDataPointsDto
            {
                x = questionIndex,
                y = questionSolvedCount,
            };
            
            barGraphData.Add(graphDataPoint);
        }
        
        var barGraph = new BarGraphDto
        {
            BarGraphData = barGraphData,
            StudentsNumber = contestLeaderboardFiltered.Count,
        };
        
        // get conversion rate from contestLeaderboardFiltered
        // avg. conversion rate = 
        
        var pieChart = new PieChartDto
        {
            ConversionRate = "value"
        };
        
        var contestWithGraph = new ContestWithGraphsDto
        {
            Leaderboard = contestLeaderboardFiltered,
            PageNumber = request.Filter.PageNumber,
            PageSize = request.Filter.PageSize,
            BarGraph = barGraph,
            PieChart = pieChart,
        };
        
        return contestWithGraph;
    }

    private static IQueryable<UserContestAndQuestionDto> FilterByLocation(IQueryable<UserContestAndQuestionDto> query,
        string? filterLocation)
    {
        if (!string.IsNullOrEmpty(filterLocation))
        {
            // the Group is in the User Entity
            return query
                .Where(ucq => ucq.User.UserGroup.Location != null && string.Equals(ucq.User.UserGroup.Location.Country,
                    filterLocation, StringComparison.CurrentCultureIgnoreCase));
        }

        return query;
    }

    private static IQueryable<UserContestAndQuestionDto> FilterByGeneration(IQueryable<UserContestAndQuestionDto> query,
        string? filterGeneration)
    {
        if (!string.IsNullOrEmpty(filterGeneration))
        {
            return query
                .Where(ucq => ucq.User.UserGroup.Generation.ToLower() == filterGeneration.ToLower());
        }

        return query;
    }

    private static IQueryable<UserContestAndQuestionDto> FilterByGroup(IQueryable<UserContestAndQuestionDto> query, string? filterGroup)
    {
        if (!string.IsNullOrEmpty(filterGroup))
        {
            filterGroup = filterGroup.ToLower();
            
            return query
                .Where(ucq => ucq.User.UserGroup.Name.ToLower() == filterGroup
                              || ucq.User.UserGroup.Abbreviation.ToLower() == filterGroup);
        }

        return query;
    }

    private static IQueryable<UserContestAndQuestionDto> FilterByCountry(IQueryable<UserContestAndQuestionDto> query, string? filterCountry)
    {
        Console.WriteLine("Filter Country: " + filterCountry);
        if (!string.IsNullOrEmpty(filterCountry))
        {
            filterCountry = filterCountry.ToLower();
            
            return query
                .Where(ucq => ucq.User.UserGroup.Location != null && ucq.User.UserGroup.Location.Country.ToLower() == filterCountry);
        }

        return query;
    }
}