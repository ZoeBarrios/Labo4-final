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


        public PublicationController(PublicationService publicationService)
        {
            _publicationService = publicationService;
        }


        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicationDto>>> Get([FromQuery] int page = 1, [FromQuery]int pageSize = 20)
        {
            if(page > 0 && pageSize > 0)
            {
                var publications = await _publicationService.GetAll(page, pageSize);
                return Ok(publications);
            }

            return BadRequest();
            
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PublicationDto>> Get(int id)
        {
            return Ok(await _publicationService.GetById(id));
        }

        [HttpGet("category/{CategoryId}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicationsDto>>> GetByCategory(int CategoryId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            return Ok(await _publicationService.GetAllByCategory(CategoryId,page,pageSize));
        }



        [HttpGet("name/{name}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PublicationsDto>> GetByName(string name, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
           
            return Ok(await _publicationService.GetAllByName(name, page, pageSize));
          
        }

        [HttpGet("user/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PublicationsDto>> GetByUserId(int id, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {

            return Ok(await _publicationService.GetAllByUserId(id));

        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<ActionResult<PublicationDto>> Post([FromForm] CreatePublicationDto createPublicationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           
            var publicationCreated = await _publicationService.Create(createPublicationDto);

            return Created("PublicationCreated", publicationCreated);
        }



        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<ActionResult<PublicationDto>> Put(int id, [FromBody] UpdatePublicationDto updatePublicationDto)
        {
            if (updatePublicationDto.Stock != 0)
            {
                updatePublicationDto.IsPaused = false;
            }
            else
            {
                updatePublicationDto.IsPaused = true;
            }
            
            try
            {
                var updatedPublication = await _publicationService.UpdateById(id, updatePublicationDto);
                return Ok(updatedPublication);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Delete(int id)
        {

            try
            {
                await _publicationService.DeleteById(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Images/{imageName}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetImage(string imageName)
        {
            var imagePath = Path.Combine("Images", imageName);
            if (System.IO.File.Exists(imagePath))
            {
                var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                return File(imageBytes, "image/jpg"); 
            }
            return NotFound(); 
        }



    }

}
