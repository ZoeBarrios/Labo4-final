using AutoMapper;
using EcommerceAPI.Models.Comment;
using EcommerceAPI.Models.Comment.Dto;
using EcommerceAPI.Repositories;
using System.Net;
using System.Web.Http;

namespace EcommerceAPI.Services
{
    public class CommentsService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentsService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<List<CommentDto>> GetAllByPublication(int id)
        {
            var lista = await _commentRepository.GetAll(c => c.PublicationId == id);
            var comments = lista.Where(c => c.isEliminated == false).ToList();
            return _mapper.Map<List<CommentDto>>(comments);
        }


        public async Task<List<CommentDto>> GetEliminatedCommentsByPublication(int id)
        {
            var lista = await _commentRepository.GetAll(c => c.PublicationId == id);
            var comments = lista.Where(c => c.isEliminated == true).ToList();

            return _mapper.Map<List<CommentDto>>(lista);
        }

        public async Task<CommentDto> Create(CreateCommentDto createCommentDto)
        {
            var comment = _mapper.Map<Comment>(createCommentDto);


            await _commentRepository.Add(comment);

            return _mapper.Map<CommentDto>(comment);
        }

        public async Task<CommentDto> UpdateById(int id, UpdateCommentDto updateCommentDto)
        {
            var comment = await _commentRepository.GetOne(p => p.CommentId == id);

            if (comment == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var updated = _mapper.Map(updateCommentDto,comment);

            return _mapper.Map<CommentDto>(await _commentRepository.Update(updated));
        }

        public async Task DeleteById(int id)
        {
            Comment comment = await _commentRepository.GetOne(p => p.CommentId == id);

            if (comment == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            comment.isEliminated = true;
            await _commentRepository.Update(comment);
        }

    }
}
