using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UsersApi.Models.Auth;
using UsersApi.Models.Auth.Dto;
using UsersApi.Models.Role;
using UsersApi.Models.Role.Dto;
using UsersApi.Models.User;
using UsersApi.Models.User.Dto;
using UsersApi.Services;

namespace UsersApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IEncoderService _encoderService;
        private readonly AuthService _authService;
        private readonly RoleService _roleService;

        public AuthController(UserService userService, IEncoderService encoderService, AuthService authService, RoleService roleService, IMapper mapper)
        {
            _userService = userService;
            _encoderService = encoderService;
            _authService = authService;
            _roleService = roleService;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (login.Email == null && login.UserName == null)
                {
                    ModelState.AddModelError("Error", "Credentials are incorrect");
                    return BadRequest(ModelState);
                }

                var user = await _userService.GetByUsernameOrEmail(login.UserName, login.Email);

                if (user == null || !_encoderService.Verify(login.Password, user.Password))
                {
                    ModelState.AddModelError("Error", "Credentials are incorrect");
                    return BadRequest(ModelState);
                }
                string token = _authService.GenerateJwtToken(user);

                return Ok(new LoginResponseDto { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> Register([FromBody] CreateUserDto register)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = await _userService.GetByUsernameOrEmail(register.UserName, register.Email);

                if (user != null)
                {
                    ModelState.AddModelError("Error", "User already exists");
                    return BadRequest(ModelState);
                }

                var userCreated = await _userService.Create(register);

                var defaultRole = await _roleService.GetRoleByName("User");

                await _userService.UpdateUserRolesById(userCreated.Id, new List<Role> { defaultRole });

                return Created("RegisterUser", userCreated);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("roles/user/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> Put(int id, [FromBody] UpdateUserRolesDto updateUserRolesDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var roles = await _roleService.GetRolesByIds(updateUserRolesDto.RoleIds);
                var userUpdated = await _userService.UpdateUserRolesById(id, roles);
                return Ok(userUpdated);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
