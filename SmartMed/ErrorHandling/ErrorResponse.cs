namespace SmartMed.API.ErrorHandling
{
    public class ErrorResponse
    {
        public string Messaage { get; }
        public string? Details { get; }
        public ErrorResponse(string message, string? details)
        {
            Messaage =message;
            Details = details;
        }
    }
}
