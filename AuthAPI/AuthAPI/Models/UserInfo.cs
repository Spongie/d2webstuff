using System.Security.Claims;

namespace AuthAPI.Models
{
    public class UserInfo
    {
        public string Username { get; set; } = "";
        public List<Claim> Claims { get; set; } = new List<Claim>();
        public Guid SessionId { get; internal set; }
        public DateTime ExpiresAt { get; set; }
    }
}
