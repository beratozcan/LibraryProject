using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace NLayer.API.Security
{
    public class TokenHandler
    {
        public static Token TokenHandler(IConfiguration configuration)
        {
            Token token = new Token();

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Token:SecurityKey"]));

            SigningCredentials credentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256 );

            token.Expiration = DateTime.Now.AddMinutes(Convert.ToInt16(configuration["Token:Expiration"]));

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                
                issuer : configuration["Token: Issuer"],
                audience : configuration["Token:Audience"],
                
                
                )
        }
    }
}
