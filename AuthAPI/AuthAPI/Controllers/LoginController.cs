using AuthAPI.Database;
using AuthAPI.Models;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace AuthAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly UserDbContext _dbContext;
        private readonly IConnectionMultiplexer _redisConnection;

        public LoginController(UserDbContext dbContext, IConnectionMultiplexer redisConnection)
        {
            _dbContext = dbContext;
            _redisConnection = redisConnection;
        }

        [HttpPost]
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var user = _dbContext.Users.Where(u => u.Username == request.Username).FirstOrDefault();

            if (user == null || user.PasswordHash != GetPasswordHash(request.Password))
            {
                return new LoginResponse
                {
                    IsOk = false,
                    Message = "Login failed",
                    SessionId = ""
                };
            }

            var userInfo = new UserInfo()
            {
                Username = user.Username,
                SessionId = Guid.NewGuid(),
                ExpiresAt = DateTime.Now.AddMinutes(30),
                Claims = new List<Claim> { new Claim(ClaimTypes.Role, "Admin") }
            };

            await _redisConnection.GetDatabase().StringSetAsync(new RedisKey(userInfo.SessionId.ToString()), JsonSerializer.Serialize(userInfo));

            return new LoginResponse
            {
                IsOk = true,
                Message = "Login successfull",
                SessionId = userInfo.SessionId.ToString()
            };
        }

        private string GetPasswordHash(string password)
        {
            using (var sha = SHA256.Create())
            {
                var hashBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                return string.Join("", hashBytes.Select(x => x.ToString("X2")));
            }
        }
    }
}
