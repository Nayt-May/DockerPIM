using LincolnAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LincolnAPI.Identity
{
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _config;

        private readonly IdentityRepository _repo;

        private static readonly TimeSpan TokenLifeTime = TimeSpan.FromMinutes(15);
        public TokenController(IConfiguration config, IdentityRepository repo)
        {
            _config = config;
            _repo = repo;
        }

        [HttpPost("token")]
        public async Task<IActionResult> GenerateToken([FromBody] TokenGenerationRequest request)
        {

            if (await _repo.GetUserByUserNameAsync(request.UserName) == null)
            {
                return BadRequest("No User was found with that username");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]!);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, request.Email),
                new Claim(JwtRegisteredClaimNames.Email, request.Email),
                new Claim("userId", request.UserId.ToString())
            };

            foreach (var claimPair in request.Roles!)
            {
               if((claimPair.Value != string.Empty || claimPair.Value != ""))
                {
                    if (Boolean.TryParse(claimPair.Value, out bool result))
                    {
                        if (result)
                        {
                            var claim = new Claim(IdentityData.RoleClaimName, claimPair.Key);
                            claims.Add(claim);
                        }
                    }   
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(TokenLifeTime),
                Issuer = _config["JwtSettings:Issuer"],
                Audience = _config["JwtSettings:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token  = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(token);
            return Ok(jwt);
            
        }
    }
}
