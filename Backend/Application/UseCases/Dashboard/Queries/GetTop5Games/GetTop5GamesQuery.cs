using Application.DataTransferObjects;
using Core.Application;

namespace Application.UseCases.Dashboard.Queries.GetTop5Games
{
    public class GetTop5GamesQuery : QueryRequest<QueryResult<TopGameDto>>
    {
    }
}