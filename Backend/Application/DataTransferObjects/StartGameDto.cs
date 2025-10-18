namespace Application.DataTransferObjects
{
    /// <summary>
    /// DTO para solicitud de inicio de juego
    /// </summary>
    public class StartGameRequest
    {
        public int PlayerId { get; set; }
    }

    /// <summary>
    /// DTO para respuesta de inicio de juego exitoso
    /// </summary>
    public class StartGameResponse
    {
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// DTO para respuesta de error
    /// </summary>
    public class ErrorResponse
    {
        public string Message { get; set; }
    }
}