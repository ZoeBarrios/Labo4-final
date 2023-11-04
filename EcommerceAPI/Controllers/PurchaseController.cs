using EcommerceAPI.Models.Publication.Dto;
using EcommerceAPI.Models.Purchase.Dto;
using EcommerceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Route("api/purchases")]
    [Authorize]
    [ApiController]
    
    public class PurchaseController : ControllerBase
    {
        private readonly PurchaseService _purchaseService;
        private readonly PublicationService _publicationService;

        public PurchaseController(PurchaseService purchaseService, PublicationService publicationService)
        {
            _purchaseService = purchaseService;
            _publicationService = publicationService;

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PublicationDto>> Post([FromBody] CreatePurchaseDto createPurchaseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var PurchaseCreated = await _purchaseService.Create(createPurchaseDto);

            var publications = await _publicationService.GetPublicationsByIds(createPurchaseDto.PublicationsIds);

            

            var UpdatePurchase = await _purchaseService.UpdateById(PurchaseCreated.PurchaseId, publications);

            return Created("PurchaseCreated", UpdatePurchase);

        }

        [HttpGet("user/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PurchaseDto>>> GetByUserId(int id)
        {
            
                return Ok(await _purchaseService.GetAllByUserId(id));
            
        }

        [HttpGet("seller/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PurchaseDto>>> GetBySellerId(int id)
        {

            return Ok(await _purchaseService.GetAllBySelllerId(id));

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PurchaseDto>> GetOneById(int id)
        {
            try
            {
                return Ok(await _purchaseService.GetOneById(id));
            }
            catch
            {
                return NotFound(new { message = $"No purchase with Id = {id}" });
            }
        }
    }
}
