namespace Application.DataTransferObjects
{
    /// <summary>
    /// DTO para solicitud de registro de jugador
    /// </summary>
    public class RegisterPlayerRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    /// <summary>
    /// DTO para respuesta de registro exitoso
    /// </summary>
    public class RegisterPlayerResponse
    {
        public int PlayerId { get; set; }
    }
}