using Application.DataTransferObjects;
using Core.Application;

namespace Application.UseCases.Dashboard.Queries.GetUsersPerDay
{
    public class GetUsersPerDayQuery : QueryRequest<QueryResult<UserRegistrationByDayDto>>
    {
        public int Days { get; set; } = 7;
    }
}