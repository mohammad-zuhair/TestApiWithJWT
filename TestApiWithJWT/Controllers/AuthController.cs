using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestApiWithJWT.Models;
using TestApiWithJWT.Services;

namespace TestApiWithJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);

            //return Ok(new { token =result.Token ,expired = result.ExpiresOn});
        }

        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.GetTokenAsync(model);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);

            //return Ok(new { token =result.Token ,expired = result.ExpiresOn});
        }

        [HttpPost("addRole")]
        public async Task<IActionResult> AddroleAsync([FromBody] AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);
            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);


            return Ok(model);

            //return Ok(new { token =result.Token ,expired = result.ExpiresOn});
        }
    }
}
