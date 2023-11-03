using AutoMapper;
using EcommerceAPI.Models.Publication;
using EcommerceAPI.Models.Publication.Dto;
using EcommerceAPI.Models.Role;
using EcommerceAPI.Models.User;
using EcommerceAPI.Models.UserFavorite;
using EcommerceAPI.Models.UserFavorite.Dto;
using EcommerceAPI.Repositories;

namespace EcommerceAPI.Services
{
    public class UserFavoriteService
    {
        private readonly IUserFavoriteRepository _userFavorite;
        private readonly IMapper _mapper;
        public UserFavoriteService(IUserFavoriteRepository userFavorite,IMapper mapper,IPublicationRepository publicationRepository) 
        {
            _mapper = mapper;
            _userFavorite = userFavorite;
        }

        public async Task<UserFavorite> Create(CreateUserFavoriteDto createUserFavoriteDto)
        {
            var favorite = await _userFavorite.GetOne(f => f.UserId ==createUserFavoriteDto.UserId && f.PublicationId == createUserFavoriteDto.PublicationId);
            if (favorite != null)
            {
                throw new Exception("Already Exist");
            }

            var userfavorite = _mapper.Map<UserFavorite>(createUserFavoriteDto);
            await _userFavorite.Add(userfavorite);
            return userfavorite;


        }

        public async Task<UserFavorite> GetOne(int UserId,int PublicationId)
        {
            var favorite = await _userFavorite.GetOne(f => f.UserId == UserId && f.PublicationId == PublicationId);
            
            
            return favorite;
        }

        public async Task Delete(UserFavorite userFavorite)
        {

            await _userFavorite.Delete(userFavorite); 
        }

        public async Task<List<int>> GetAllByUserId(int id)
        {
            var favorites=await _userFavorite.GetAll(f=>f.UserId==id);

            var favoritePublicationIds = favorites.Select(uf => uf.PublicationId).ToList();

            
            return favoritePublicationIds;
        }



       
    }
}
