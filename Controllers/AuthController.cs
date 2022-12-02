using DotNetAssignment.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService loginService;
        public AuthController(ILoginService _loginService)
        {
            this.loginService = _loginService;
        }

        [HttpGet]
        public async Task<ActionResult<Response<string>>> Get(LoginModel loginmodel)
        {
            var response = await loginService.GenerateToken(loginmodel);

            if (response.Success == true)
            {
                return Ok(response);
            }
            return Unauthorized(response);
        }
    }
}
