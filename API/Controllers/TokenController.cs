using API.Context;
using API.Model;
using API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly MyContext _context;

        public TokenController(IConfiguration config, MyContext context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ForgotPasswordRequestVM _userData)
        {

            if (_userData.NIK != null && _userData.NewPassword != null)
            {
                var user = await GetUser(_userData.NIK, _userData.NewPassword);

                if (user != null)
                {
                    //create claims details based on the user information
                    var employee = _context.Employees.Find(user.NIK);
                    var role = _context.Roles.Find("01");
                    var claims = new[] {
                    new Claim("Email", employee.Email),
                    new Claim("Role", role.RoleName)
                   };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], "", claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                    var result = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<Account> GetUser(string NIK, string password)
        {
            return await _context.Accounts.FirstOrDefaultAsync(u => u.NIK == NIK && u.Password == password);
        }
    }
}
