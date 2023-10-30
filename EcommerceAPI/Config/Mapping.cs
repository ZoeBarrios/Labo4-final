using AutoMapper;
using UsersApi.Models.User;
using UsersApi.Models.User.Dto;

namespace UsersApi.Config
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

            
        }
    }
}
