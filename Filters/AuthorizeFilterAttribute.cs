using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DATN_back_end.Common;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace DATN_back_end.Filters
{
    [AttributeUsage(AttributeTargets.All)]
    public class AuthorizeFilterAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly Role[]? _roles;

        public AuthorizeFilterAttribute(Role[] roles)
        {
            _roles = roles;
        }

        public AuthorizeFilterAttribute() { }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();
                var token = GetValue(context, "Authorization");
                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("Token is required");
                }

                if (token.ToString().StartsWith("Bearer "))
                {
                    token = token.ToString().Substring(7);
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(configuration["SecretKey"]);
                tokenHandler.ValidateToken(
                    token,
                    new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    },
                    out SecurityToken validatedToken
                );

                var jwtToken =
                    (JwtSecurityToken)validatedToken
                    ?? throw new Exception("Invalid token");
                var claims = jwtToken.Claims;
                var userId = claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
                var fullName = claims.FirstOrDefault(x => x.Type == "FullName")?.Value;
                var role = claims.FirstOrDefault(x => x.Type == "Role")?.Value;
                var email = claims.FirstOrDefault(x => x.Type == "Email")?.Value;

                if (_roles is not null && !_roles.Contains((Role)Enum.Parse(typeof(Role), role)))
                {
                    throw new UnauthorizedAccessException();
                }

                var identity = new ClaimsIdentity(context.HttpContext.User.Identity);
                identity.AddClaim(new Claim("UserId", userId));
                identity.AddClaim(new Claim("FullName", fullName));
                identity.AddClaim(new Claim("Role", role));
                identity.AddClaim(new Claim("Email", email));


                var principal = new ClaimsPrincipal(identity);
                context.HttpContext.User = principal;
                await Task.CompletedTask;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new InvalidTokenException();

            }
        }

        static string GetValue(AuthorizationFilterContext context, string key)
        {
            try
            {
                var apiKeyHeader = context.HttpContext.Request.Headers[key].ToString();
                if (string.IsNullOrEmpty(apiKeyHeader))
                    apiKeyHeader = context.HttpContext.Request.Cookies[key].ToString();
                return apiKeyHeader;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
