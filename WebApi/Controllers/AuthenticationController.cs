using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        ///GET api/authentication/login
        [HttpGet("login")]
        public string Login(string email, string password)
        {
            var res =  _userService.GetLoginData(email, password);
            return res;
            //return res ? Ok() : BadRequest();
        }

        ///POST api/create
        [HttpPost("create")]
        public async Task<IActionResult> Create(UserDTO user)
        {
            try
            {
                await _userService.SignUp(user.Email, user.Password);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
