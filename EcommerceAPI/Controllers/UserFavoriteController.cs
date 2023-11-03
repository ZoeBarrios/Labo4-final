using EcommerceAPI.Models.Publication.Dto;
using EcommerceAPI.Models.Purchase.Dto;
using EcommerceAPI.Models.UserFavorite.Dto;
using EcommerceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EcommerceAPI.Controllers
{
    [Route("api/favorites")]
    [ApiController]
    [Authorize]
    public class UserFavoriteController : ControllerBase
    {
        private readonly UserFavoriteService _userFavoriteService;
        private readonly PublicationService _publicationService;

        public UserFavoriteController(UserFavoriteService userFavoriteService, PublicationService publicationService)
        {
            _userFavoriteService = userFavoriteService;
            _publicationService = publicationService;
        }

        [HttpGet("/user/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PublicationsDto>>> GetAllByUserId(int id)
        {
            var publicationsId = await _userFavoriteService.GetAllByUserId(id);
            try
            {
                var publications = await _publicationService.GetPublicationDtoByIds(publicationsId);

                return Ok(publications);
            }
            catch(Exception rc)
            {
                return Ok(new List<PurchasesDto>());
            }
            
            
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PublicationDto>> Post([FromBody] CreateUserFavoriteDto createUserFavoriteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var FavoriteCreated = await _userFavoriteService.Create(createUserFavoriteDto);

            return Created("FavoriteCreated", FavoriteCreated);

        }

        [HttpDelete("{userId}/{publicationId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<ActionResult> DeleteById(int userId,int publicationId)
        {
            var favorite = await _userFavoriteService.GetOne(userId, publicationId);

            if (favorite == null)
            {
                throw new System.Web.Http.HttpResponseException(HttpStatusCode.NotFound);
            }

            await _userFavoriteService.Delete(favorite);

            return NoContent();
        }
    }
}
