namespace Application.DataTransferObjects
{
    /// <summary>
    /// DTO para solicitud de intento de adivinanza
    /// </summary>
    public class GuessNumberRequest
    {
        public int GameId { get; set; }
        public string AttemptedNumber { get; set; }
    }

    /// <summary>
    /// DTO para respuesta de intento de adivinanza
    /// </summary>
    public class GuessNumberResponse
    {
        public int GameId { get; set; }
        public string AttemptedNumber { get; set; }
        public string Message { get; set; }
        public int? Famas { get; set; }
        public int? Picas { get; set; }
    }
}