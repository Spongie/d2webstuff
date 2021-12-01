using System;

namespace RuneAPI.Models
{
    public class UserInfo
    {
        public string Username { get; set; } = "";
        public Guid SessionId { get; internal set; }
        public DateTime ExpiresAt { get; set; }
    }
}
