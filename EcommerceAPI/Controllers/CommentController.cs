using EcommerceAPI.Models.Comment.Dto;
using EcommerceAPI.Models.Publication.Dto;
using EcommerceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EcommerceAPI.Controllers
{
    [Route("api/comments")]
    [ApiController]
    
    public class CommentController : ControllerBase
    {
        private readonly CommentsService _commentsService;

        public CommentController(CommentsService commentsService)
        {
            _commentsService = commentsService;
        
        }

        [HttpGet("publication/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CommentDto>>> Get(int id)
        {
            return Ok(await _commentsService.GetAllByPublication(id));
        }

        [HttpGet("publication/eliminated/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetEliminated(int id)
        {
            return Ok(await _commentsService.GetEliminatedCommentsByPublication(id));
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<ActionResult<CommentDto>> Post([FromBody] CreateCommentDto createCommentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var CommentCreated = await _commentsService.Create(createCommentDto);

            return Created("CommentCreated", CommentCreated);

        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public async Task<ActionResult<CommentDto>> Put(int id, [FromBody] UpdateCommentDto updateCommentDto)
        {

            try
            {
                var updatedComment = await _commentsService.UpdateById(id, updateCommentDto);
                return Ok(updatedComment);
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {

            try
            {
                await _commentsService.DeleteById(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
