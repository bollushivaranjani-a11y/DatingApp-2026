 
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services;

public class TokenService(IConfiguration config) : ITokenService
{

 public string CreateToken(AppUser user)
    {
       
        var tokenkey = config["TokenKey"] ?? throw new Exception("Cannot get token key");

        if(tokenkey.Length <64)
        {
            throw new Exception("Yopur token needs to be >=64 characters");
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenkey));

        var claims = new List<Claim>
        {
            new (ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokendescription  = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials= creds
        };

        var tokenhandler = new JwtSecurityTokenHandler();

        var token = tokenhandler.CreateToken(tokendescription);

        return tokenhandler.WriteToken(token);                    
        
    }
}
