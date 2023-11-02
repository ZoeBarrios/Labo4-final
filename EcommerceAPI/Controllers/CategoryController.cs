using EcommerceAPI.Models.Category.Dto;
using EcommerceAPI.Models.Comment.Dto;
using EcommerceAPI.Models.Publication.Dto;
using EcommerceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Controllers
{
    [Route("api/categories")]
    [ApiController]

    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService) 
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> Get()
        {
            return Ok(await _categoryService.GetAll());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> Get(int id)
        {
            return Ok(await _categoryService.GetOne(id));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<CategoryDto>> Put(int id, [FromBody] UpdateCategoryDto updateCategoryDto)
        {

            try
            {
                var updatedCategory = await _categoryService.UpdateById(id,updateCategoryDto);
                return Ok(updatedCategory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {

            try
            {
                await _categoryService.DeleteById(id);
                return Ok(new
                {
                    message = $"Publication with Id = {id} was deleted"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PublicationDto>> Post([FromBody] CreateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryCreated = await _categoryService.Create(categoryDto);

            return Created("CategoryCreated", categoryCreated);
        }



    }
}
