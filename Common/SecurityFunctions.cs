using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DATN_back_end.Common
{
    public static class SecurityFunction
    {
        public static string GenerateRandomString(int length = 20, bool includeOnlyNumerics = false)
        {
            string validChars = includeOnlyNumerics switch
            {
                true => "1234567890",
                false => "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*",
            };
            char[] chars = new char[length];

            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[RandomNumberGenerator.GetInt32(validChars.Length - 1)];
            }

            return new string(chars);
        }

        public static string HashPassword(string password, string salt)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password + salt);
            byte[] hashBytes = SHA256.HashData(passwordBytes);

            var builder = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                builder.Append(hashBytes[i].ToString("x2"));
            }

            return builder.ToString();
        }

        public static string GenerateToken(ClaimData tokenDto, IConfiguration configuration)
        {
            var claims = new List<Claim>()
            {
                new Claim("UserId", tokenDto.UserId.ToString()),
                new Claim("Role", tokenDto.Role.ToString()),
                new Claim("FullName", tokenDto.FullName),
                new Claim("Email", tokenDto.Email),
            };

            var symmetricKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(configuration["SecretKey"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(int.Parse(configuration["TokenTimeOut"]!)),
                SigningCredentials = new SigningCredentials(
                    symmetricKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }

    public class ClaimData
    {
        public Guid UserId { get; set; }
        public Role Role { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}