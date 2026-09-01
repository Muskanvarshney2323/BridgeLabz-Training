using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace FundooNotesApp.BusinessLayer.Helper
{
    public class JwtTokenHelper
    {
        public string CreateToken(
            int userId,
            string name,
            string email,
            string secretKey)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secretKey)
            );

            var signingCredentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256
            );

            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    userId.ToString()
                ),

                new Claim(
                    ClaimTypes.Name,
                    name
                ),

                new Claim(
                    ClaimTypes.Email,
                    email
                )
            };

            var jwtToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler()
                .WriteToken(jwtToken);
        }
    }
}