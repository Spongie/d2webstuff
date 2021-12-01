using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using RuneAPI.Models;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace RuneAPI.Controllers
{
    public partial class AuthorizationHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new AuthorizationMiddlewareResultHandler();
        private readonly IConnectionMultiplexer _redisConnection;

        public AuthorizationHandler(IConnectionMultiplexer piRedisConnection)
        {
            _redisConnection = piRedisConnection;
        }

        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            var authPolicy = policy.Requirements.OfType<DenyAnonymousAuthorizationRequirement>().FirstOrDefault();

            if (authPolicy == null)
            {
                await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
            }
            else
            {
                if (!await ValidateToken(context.Request.Headers["Authorization"]))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    await context.Response.WriteAsync("Unauthorized");
                }
                else
                {
                    await next.Invoke(context);
                }
            }
        }

        private async Task<bool> ValidateToken(string token)
        {
            var redisEntry = await _redisConnection.GetDatabase().StringGetAsync(token.Substring("Bearer ".Length));

            if (!redisEntry.HasValue)
            {
                return false;
            }

            var userInfo = JsonSerializer.Deserialize<UserInfo>(redisEntry.ToString());

            return userInfo.ExpiresAt > DateTime.Now;
        }
    }
}
