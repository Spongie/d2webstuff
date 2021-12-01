namespace AuthAPI.Models
{
    public struct LoginResponse
    {
        public bool IsOk { get; set; }
        public string Message { get; set; }
        public string SessionId { get; set; }
    }
}
