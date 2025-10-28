using Application.DataTransferObjects;
using Core.Application;

namespace Application.UseCases.Dashboard.Queries.GetAllGamesWithAttempts
{
    public class GetAllGamesWithAttemptsQuery : QueryRequest<QueryResult<GameAttemptsDto>>
    {
    }
}