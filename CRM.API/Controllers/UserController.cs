using Application.UsersBL;
using Infrastructure.Dtos.UserDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers
{

    public class UserController : BaseApiController
    {
      
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            return HandleResult(await Mediator.Send(new UserLogin.Command { Login = loginDto }));
        }
        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser(RegisterUserDto postUser)
        {
            return HandleResult(await Mediator.Send(new Register.Command { UserDto = postUser }));
        }
        [HttpPost("CreateRole")]
        public async Task<ActionResult> CreateRole(string roleName)
        {
            return HandleResult(await Mediator.Send(new CreateRole.Command { Role = roleName }));
        }

        [HttpGet("GetRoleList")]
        public async Task<IActionResult> RoleList()
        {
            return HandleResult(await Mediator.Send(new RoleList.Query()));
        }
    }
}
