using Application.DataTransferObjects;
using Application.Repositories;
using Core.Application;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.Dashboard.Queries.GetUsersPerDay
{
    internal class GetUsersPerDayHandler : IRequestQueryHandler<GetUsersPerDayQuery, QueryResult<UserRegistrationByDayDto>>
    {
        private readonly IPlayerRepository _playerRepository;

        public GetUsersPerDayHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
        }

        public async Task<QueryResult<UserRegistrationByDayDto>> Handle(GetUsersPerDayQuery request, CancellationToken cancellationToken)
        {
            var startDate = DateTime.Now.AddDays(-request.Days).Date;
            var allPlayers = await _playerRepository.FindAllAsync();

            var usersPerDay = allPlayers
                .Where(p => p.RegistrationDate >= startDate)
                .GroupBy(p => p.RegistrationDate.Date)
                .Select(g => new UserRegistrationByDayDto
                {
                    Date = g.Key.ToString("dd/MM/yyyy"),
                    Count = g.Count()
                })
                .OrderBy(u => u.Date)
                .ToList();

            return new QueryResult<UserRegistrationByDayDto>(usersPerDay, usersPerDay.Count, request.PageIndex, request.PageSize);
        }
    }
}