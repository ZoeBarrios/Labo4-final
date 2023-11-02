using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EcommerceAPI.Models.Role;
using EcommerceAPI.Models.User.Dto;
using EcommerceAPI.Services;



namespace EcommerceAPI.Controllers
{
    [Route("api/users")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly AuthService _authService;
        public UserController(UserService userService,AuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UsersDto>>> Get()
        {
            return Ok(await _userService.GetAll());
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            try
            {
                return Ok(await _userService.GetById(id));
            }
            catch
            {
                return NotFound(new { message = $"No user with Id = {id}" });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        
        public async Task<ActionResult<UserDto>> Post([FromBody] CreateUserDto createUserDto,int UserId)
        {
            try
            {
                var user = await _userService.GetById(UserId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userCreated = await _userService.Create(createUserDto);

            return Created("CreateUser", userCreated);

        }

        [HttpPut("{UserId}/{IdToUpdate}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<ActionResult<UserDto>> Put(int UserId, int IdToUpdate, [FromBody] UpdateUserDto updateUserDto)
        {
            try
            {
                var user = await _userService.GetById(UserId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            List<Role> roles = await _userService.GetRolesOfUserById(UserId);
            bool isAdmin = false;

            foreach (Role role in roles)
            {
                if (role.Name == "Admin")
                {
                    isAdmin = true;
                    break;
                }
            }

            if (!isAdmin)
            {
                if (IdToUpdate != UserId)

                {
                    return Unauthorized();
                }
            }
            try
            {
                var userUpdated = await _userService.UpdateById(IdToUpdate, updateUserDto);
                return Ok(userUpdated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{UserId}/{IdToDelete}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Delete(int IdToDelete, int UserId)
        {
            try
            {
                await _authService.IsUserAuthorized(UserId, IdToDelete);
            }catch(Exception ex)
            {
                return Unauthorized();
            }
            
            try
            {
                await _userService.DeleteById(IdToDelete);
                return Ok(new
                {
                    message = $"User with Id = {IdToDelete} was deleted"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
