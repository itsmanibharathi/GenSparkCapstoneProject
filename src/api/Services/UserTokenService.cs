

using api.Models;
using api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api.Services
{
    /// <summary>
    /// Provides token generation services for <see cref="User"/> entities.
    /// </summary>
    public class UserTokenService : ITokenService<User>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="UserTokenService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration containing the JWT settings.</param>
        public UserTokenService()
        {
        }

        /// <summary>
        /// Generates a JWT token for the specified <see cref="User"/>.
        /// </summary>
        /// <param name="item">The <see cref="User"/> for which the token is generated.</param>
        /// <returns>The generated JWT token as a string.</returns>
        public string GenerateToken(User item)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_USER_SECRET") ?? "temp");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", item.UserId.ToString()),
                    new Claim("isOwner", item.IsOwner.ToString()),
                    new Claim(ClaimTypes.Name, item.UserName)
                }),
                Expires = DateTime.UtcNow.AddDays(100),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public int ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_USER_SECRET") ?? "temp");
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "Id").Value);
            return userId;
        }
    }
}