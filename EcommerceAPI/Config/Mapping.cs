using AutoMapper;
using EcommerceAPI.Models.Publication;
using EcommerceAPI.Models.Publication.Dto;
using EcommerceAPI.Models.Purchase.Dto;
using EcommerceAPI.Models.Purchase;
using EcommerceAPI.Models.User;
using EcommerceAPI.Models.User.Dto;
using EcommerceAPI.Models.Comment;
using EcommerceAPI.Models.Comment.Dto;
using EcommerceAPI.Models.UserFavorite;
using EcommerceAPI.Models.UserFavorite.Dto;
using EcommerceAPI.Models.Category.Dto;
using EcommerceAPI.Models.Category;

namespace EcommerceAPI.Config
{
    public class Mapping : Profile
    {

        public Mapping()
        {
            // User
            CreateMap<User, UsersDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<CreateUserDto, User>().ReverseMap();
            // no mapear los null en el update
            CreateMap<UpdateUserDto, User>().ForAllMembers(opts => opts.Condition((_, _, srcMember) => srcMember != null));

            // Publications
            CreateMap<Publication, PublicationsDto>().ReverseMap();
            CreateMap<Publication, PublicationDto>().ReverseMap();
            CreateMap<CreatePublicationDto, Publication>().ReverseMap();
            // no mapear los null en el update
            CreateMap<UpdatePublicationDto, Publication>().ForAllMembers(opts => opts.Condition((_, _, srcMember) => srcMember != null));


            //Purchases
            CreateMap<PurchaseDto, Purchase>();
            CreateMap<Purchase, PurchasesDto>().ReverseMap();
            CreateMap<Purchase, PurchaseDto>()
            .ForMember(dest => dest.Publications, opt => opt.MapFrom(src => src.Publications));

            //Comments
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<CreateCommentDto, Comment>().ReverseMap();
            CreateMap<UpdateCommentDto, Comment>().ForAllMembers(opts => opts.Condition((_, _, srcMember) => srcMember != null));

            //UserFavorites
            CreateMap<UserFavorite, CreateUserFavoriteDto>().ReverseMap();

            //Category
            CreateMap<Category, CategoryDto>().ReverseMap();

            
        }
    }
}
