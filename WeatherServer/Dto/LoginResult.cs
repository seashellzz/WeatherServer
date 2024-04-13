namespace WeatherServer.Dto
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string? Message { get; set; } //generated in server and send to client
        public string? Token { get; set; }   
    }
}
