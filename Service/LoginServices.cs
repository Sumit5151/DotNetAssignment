using DotNetAssignment.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DotNetAssignment.Service
{
    public class LoginService : ILoginService
    {

        private readonly SpringCtdbContext db;
        private readonly IConfiguration configuration;
        public LoginService(SpringCtdbContext _db, IConfiguration _configuration)
        {
            this.db = _db;
            this.configuration = _configuration;
        }


        public async Task<Response<string>> GenerateToken(LoginModel loginModel)
        {
            Response<string> serviceResponse = new Response<string>();



            if (await IsValidUser(loginModel) == true)
            {
                var user = await GetUser(loginModel);

                //create claims details based on the user information
                var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserName", user.UserName),
                        new Claim("Name", user.Name)

                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    configuration["Jwt:Issuer"],
                    configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn);
                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);


                serviceResponse.Data = tokenString;

            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "UserName or Password is not valid";
            }

            return serviceResponse;
        }

        private async Task<User> GetUser(LoginModel loginModel)
        {
            return await db.Users.SingleAsync(x => x.UserName == loginModel.UserName && x.Password == loginModel.Password);
        }

        private async Task<bool> IsValidUser(LoginModel loginModel)
        {
            return await db.Users.AnyAsync(x => x.UserName == loginModel.UserName && x.Password == loginModel.Password);
        }
    }
}
