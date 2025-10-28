using System;

namespace Application.DataTransferObjects
{
    public class DashboardStatsDto
    {
        public int TotalPlayers { get; set; }
        public int TotalGames { get; set; }
        public int GamesFinished { get; set; }
        public double AverageAttempts { get; set; }
    }

    public class UserRegistrationByDayDto
    {
        public string Date { get; set; }
        public int Count { get; set; }
    }

    public class TopGameDto
    {
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int Attempts { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class GameAttemptsDto
    {
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int Attempts { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}