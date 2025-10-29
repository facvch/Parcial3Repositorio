namespace PicasYFamas.BlazorApp.Components.Models
{
    // Request/Response
    public class RegisterPlayerRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    public class RegisterPlayerResponse
    {
        public int PlayerId { get; set; }
    }

    public class StartGameRequest
    {
        public int PlayerId { get; set; }
    }

    public class StartGameResponse
    {
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class GuessNumberRequest
    {
        public int GameId { get; set; }
        public string AttemptedNumber { get; set; }
    }

    public class GuessNumberResponse
    {
        public int GameId { get; set; }
        public string AttemptedNumber { get; set; }
        public string Message { get; set; }
        public int? Famas { get; set; }
        public int? Picas { get; set; }
    }

    public class ErrorResponse
    {
        public string Message { get; set; }
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public string Code { get; set; }
        public int StatusCode { get; set; }
    }

    // Dashboard
    public class DashboardData
    {
        public DashboardStats Stats { get; set; }
        public List<UserRegistrationByDay> UsersPerDay { get; set; }
        public List<TopGame> Top5Games { get; set; }
        public List<GameAttempts> AttemptsPerGame { get; set; }
    }

    public class DashboardStats
    {
        public int TotalPlayers { get; set; }
        public int TotalGames { get; set; }
        public int GamesFinished { get; set; }
        public double AverageAttempts { get; set; }
    }

    public class UserRegistrationByDay
    {
        public string Date { get; set; }
        public int Count { get; set; }
    }

    public class TopGame
    {
        public int GameId { get; set; }
        public string PlayerName { get; set; }
        public int Attempts { get; set; }
    }

    public class GameAttempts
    {
        public int GameId { get; set; }
        public string PlayerName { get; set; }
        public int Attempts { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}