

using api.Models;
using api.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api.Services
{
    /// <summary>
    /// Provides token generation services for <see cref="UserSubscriptionPlan"/> entities.
    /// </summary>
    public class UserSubscriptionPlanTokenService : ITokenService<UserSubscriptionPlan>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="UserSubscriptionPlanTokenService"/> class.
        /// </summary>
        /// <param name="configuration">The configuration containing the JWT settings.</param>
        public UserSubscriptionPlanTokenService()
        {
        }

        /// <summary>
        /// Generates a JWT token for the specified <see cref="UserSubscriptionPlan"/>.
        /// </summary>
        /// <param name="item">The <see cref="UserSubscriptionPlan"/> for which the token is generated.</param>
        /// <returns>The generated JWT token as a string.</returns>
        public string GenerateToken(UserSubscriptionPlan item)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_UserSubscriptionPlan_SECRET") ?? "temp");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", item.UserSubscriptionPlanId.ToString()),
                    new Claim("UserId", item.UserId.ToString()),
                }),
                Expires = item.SubscriptionEndDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public int ValidateToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}