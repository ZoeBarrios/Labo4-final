using EcommerceAPI.Models.Comment.Dto;
using EcommerceAPI.Models.Publication.Dto;
using EcommerceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace EcommerceAPI.Controllers
{
    [Route("api/comments")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly CommentsService _commentsService;

        public CommentController(CommentsService commentsService)
        {
            _commentsService = commentsService;
        
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CommentDto>>> Get(int id)
        {
            return Ok(await _commentsService.GetAllByPublication(id));
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

    }
}
