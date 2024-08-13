using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarbonEmissions.Backend.Helpers
{
    public class JwtEntesions
    {
        public static string GetToken(IConfiguration config)
        {
            string _secretKey = config["JwtBearer:SecretKey"]!;
            string _issuer = config["JwtBearer:Issuer"]!;
            string _audience = config["JwtBearer:Audience"]!;
            string _userId = config["Loggin:UserId"]!;
            string _user = config["Loggin:UserName"]!;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _user),
                new Claim("idUsuario", _userId)
            };

            var token = new JwtSecurityToken(
               issuer: _issuer,
               audience: _audience,
               claims,
               expires: DateTime.Now.AddDays(1),
               signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static int GetTokenIdUsuario(ClaimsIdentity identity)
        {
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                foreach (var claim in claims)
                {
                    if (claim.Type == "idUsuario")
                    {
                        return int.Parse(claim.Value);
                    }
                }
            }
            return 0;
        }
    }
}
