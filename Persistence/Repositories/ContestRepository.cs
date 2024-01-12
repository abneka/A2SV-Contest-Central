using Application.Contracts.Persistence;
using Application.DTOs.Contest;
using Application.DTOs.Group;
using Application.DTOs.Question;
using Application.DTOs.User;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;


namespace Persistence.Repositories
{
    public class ContestRepository : GenericRepository<ContestEntity>, IContestRepository
    {
        private readonly AppDBContext _dbContext;
        private readonly IMapper _mapper;
        public ContestRepository(AppDBContext dbContext, IMapper mapper) : base(dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<bool> ExistsContestGlobalIdAsync(string contest_id)
        {
            var item = await GetContestByGlobalIdAsync(contest_id);
            return item != null;
        }

        public async Task<ContestEntity> GetContestByGlobalIdAsync(string contest_id)
        {
            var item = await _dbContext.Contests
            .Where(contest => contest.ContestGlobalId == contest_id).FirstOrDefaultAsync();
            return item;
        }

        public async Task<Unit> UpdateContestByGlobalIdAsync(Guid contest_id, ContestEntity update_contest)
        {
            // find contest using contest id and update it using a new ContestEntity object
            var existingContest = await _dbContext.Contests
                .FirstOrDefaultAsync(c => c.Id == contest_id);

            if (existingContest != null)
            {
                // Update existingContest properties with values from update_contest
                existingContest.Type = update_contest.Type;
                existingContest.DurationSeconds = update_contest.DurationSeconds;
                existingContest.StartTimeSeconds = update_contest.StartTimeSeconds;
                existingContest.RelativeTimeSeconds = update_contest.RelativeTimeSeconds;
                existingContest.PreparedBy = update_contest.PreparedBy;
                existingContest.WebsiteUrl = update_contest.WebsiteUrl;
                existingContest.Description = update_contest.Description;
                existingContest.Difficulty = update_contest.Difficulty;
                existingContest.Kind = update_contest.Kind;
                existingContest.Season = update_contest.Season;
                existingContest.Status = update_contest.Status;
                
                // Save changes to the database
                await _dbContext.SaveChangesAsync();
            }

            // Return Unit.Value or any appropriate result
            return Unit.Value;
        }
        
        public async Task<string> GetGlobalIdByContestGuid(Guid contest_id)
        {
            var item = await _dbContext.Contests
                .Where(contest => contest.Id == contest_id).FirstOrDefaultAsync();
            return item.ContestGlobalId;
        }

        public async Task<List<ContestEntity>> GetContestsWithGroups()
        {
            var contests = await _dbContext.Contests
                .Include(c => c.Questions)
                .ThenInclude(q => q.UserQuestionResults)
                .Include(c => c.UserContestResults)
                .Include(c => c.ContestGroups)
                .ThenInclude(c => c.Group)
                .ThenInclude(c => c.Location)
                .ToListAsync();

            return contests;
        }
        
        public async Task<List<UserContestAndQuestionDto>> GetContestLeaderboard(Guid contest_id)
        {
            var contest = await _dbContext.UserContestResults
                .Where(u => u.ContestId == contest_id)
                .Include(u => u.Contest)
                .ThenInclude(c => c.ContestGroups)
                .Include(u => u.User)
                .ThenInclude(u => u.Group)
                .ThenInclude(g => g.Location)
                .Include(u => u.User.UserQuestionResults)
                .ThenInclude(u => u.Question)
                .ThenInclude(u => u.UserQuestionResults)
                .ToListAsync();
            
            Console.WriteLine("Contest Id: " + contest_id);
            Console.WriteLine("Nahom: " + contest.Count);
            
            // contest leaderboard should contain student_name, codeforces_handle, username, total_submission, wrong_submission, problem_solved, conversion_rate, rank
            var contestLeaderboard = new List<UserContestAndQuestionDto>();

            foreach (var userContestResult in contest)
            {
                var userContestAndQuestionDto = new UserContestAndQuestionDto
                {
                    UserId = userContestResult.UserId,
                    User = new ContestUserDto
                    {
                        UserName = userContestResult.User.UserName,
                        CodeforcesHandle = userContestResult.User.CodeforcesHandle,
                        FullName = userContestResult.User.FirstName + " " + userContestResult.User.LastName,
                        Email = userContestResult.User.Email,
                        UserGroup = _mapper.Map<GroupResponseDto>(userContestResult.User.Group)
                    },
                    SuccessfulHackCount = userContestResult.SuccessfulHackCount,
                    UnsuccessfulHackCount = userContestResult.UnsuccessfulHackCount,
                    Points = userContestResult.Points,
                    Penalty = userContestResult.Penalty,
                    Rank = userContestResult.Rank,
                    ContestId = userContestResult.ContestId,
                    TotalSubmissions = userContestResult.User.UserQuestionResults.Where(uqr => uqr.UserId == userContestResult.UserId).Sum(uqr => uqr.RejectedAttemptCount) + userContestResult.User.UserQuestionResults.Where(uqr => uqr.UserId == userContestResult.UserId).Count(uqr => uqr.Points > 0),
                    // wrong submission is the sum of the property "rejectedAttemptCount" in Question 
                    WrongSubmissions = userContestResult.User.UserQuestionResults.Sum(uqr => uqr.RejectedAttemptCount),
                    ProblemSolved = userContestResult.User.UserQuestionResults.Count(uqr => uqr.Points > 0),
                    ConversionRatePercent = (userContestResult.User.UserQuestionResults.Count(uqr => uqr.Points > 0) /
                                      userContestResult.User.UserQuestionResults.Count) * 100,
                    Id = userContestResult.Id,
                    CreatedAt = userContestResult.CreatedAt,
                    ModifiedAt = userContestResult.ModifiedAt,
                    Questions = userContestResult.User.UserQuestionResults.Select(uqr => new QuestionForUserDto
                    {
                        Points = uqr.Points,
                        RejectedAttemptCount = uqr.RejectedAttemptCount,
                        BestSubmissionTimeSeconds = uqr.BestSubmissionTimeSeconds,
                        GlobalQuestionUrl = uqr.Question.GlobalQuestionUrl,
                        Name = uqr.Question.Name,
                        Index = uqr.Question.Index,
                        Solved = uqr.Points > 0
                    }).ToList()
                };
                
                contestLeaderboard.Add(userContestAndQuestionDto);
            }

            return contestLeaderboard;
        }
    }
}