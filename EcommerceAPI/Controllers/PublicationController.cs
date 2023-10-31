using EcommerceAPI.Models.Publication.Dto;
using EcommerceAPI.Models.Role;
using EcommerceAPI.Models.User.Dto;
using EcommerceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Route("api/publications")]
    [ApiController]
    [Authorize]
    public class PublicationController : ControllerBase
    {
        private readonly PublicationService _publicationService;
        private readonly AuthService authService;

        public PublicationController(PublicationService publicationService, AuthService authService)
        {
            _publicationService = publicationService;
            this.authService = authService;
        }


        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicationsDto>>> Get()
        {
            return Ok(await _publicationService.GetAll());
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicationDto>> Get(int id)
        {
            try
            {
                return Ok(await _publicationService.GetById(id));
            }
            catch
            {
                return NotFound(new { message = $"No publication with Id = {id}" });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PublicationDto>> Post([FromBody] CreatePublicationDto createPublicationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var publicationCreated = await _publicationService.Create(createPublicationDto);

            return Created("PublicationCreated", publicationCreated);

        }


        [HttpPut("{idUser}/{idToUpdate:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]


        public async Task<ActionResult<PublicationDto>> Put(int idUser, int idToUpdate, [FromBody] UpdatePublicationDto updatePublicationDto)
        {
            try
            {
                await authService.IsUserAuthorized(idUser, idToUpdate);


            }catch(Exception ex)
            {
                return Unauthorized();
            }

            
            try
            {
                var updatedPublication = await _publicationService.UpdateById(idToUpdate, updatePublicationDto);
                return Ok(updatedPublication);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
