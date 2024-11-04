using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.IdentityModel.Tokens;
using SilverPE_Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SilverPE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTController : ODataController
    {
        private readonly IAccountRepo _accountRepo;
        private readonly IConfiguration _configuration;
        public JWTController(IAccountRepo accountRepo)
        {
            _accountRepo=accountRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string email, string password)
        {
            var acc = _accountRepo.GetBranchAccount(email, password);
            if (acc == null)
            {
                return NotFound();
            }
            var claims = new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, acc.AccountId.ToString()),
                            new Claim(ClaimTypes.Role, acc.Role.ToString()),
                        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("D35t1nyM4tchJ450nW3bt0k3nS3cr3tK3y"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            var code = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new
            {
                token = code, 
                role = acc.Role
            });
        }
    }
}
