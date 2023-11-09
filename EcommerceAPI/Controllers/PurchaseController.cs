using EcommerceAPI.Models.Publication.Dto;
using EcommerceAPI.Models.Purchase;
using EcommerceAPI.Models.Purchase.Dto;
using EcommerceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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

            try
            {
                var publications = await _publicationService.GetPublicationsByIds(createPurchaseDto.PublicationsIds);
                var purchase = await _purchaseService.Create(createPurchaseDto, publications);
                return Created("PurchaseCreated",purchase);
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
           
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

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PurchaseDto>> UpdateDeliveredPurchase(int id)
        {
            try
            {
                return await _purchaseService.UpdateWasDeliveredById(id);

            }
            catch
            {
                return NotFound(new { message = $"No purchase with Id = {id}" });
            }
        }
    }
}
