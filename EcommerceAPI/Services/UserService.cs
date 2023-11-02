using AutoMapper;
using System.Net;
using System.Web.Http;
using EcommerceAPI.Models.Role;
using EcommerceAPI.Models.User;
using EcommerceAPI.Models.User.Dto;
using EcommerceAPI.Repositories;


namespace EcommerceAPI.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly IEncoderService _encoderService;

        public UserService(IUserRepository userRepo, IMapper mapper, IEncoderService encoderService)
        {
            _userRepo = userRepo;
            _mapper = mapper;           
            _encoderService = encoderService;
        }

        public async Task<List<UsersDto>> GetAll()
        {

            var lista = await _userRepo.GetAll(u=>u.IsActive==true);
            return _mapper.Map<List<UsersDto>>(lista);
        }

        public async Task<UserDto> GetById(int id)
        {
            var user = await _userRepo.GetOne(u => u.UserId == id);

            if (user == null || user.IsActive==false)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            
            var mapped = _mapper.Map<UserDto>(user);

            

            return mapped;
        }

        public async Task<User> GetByUsernameOrEmail(string? username, string? email)
        {
            var users = Enumerable.Empty<User>();

            if (email != null)
            {
                users = await _userRepo.GetAll(u => u.Email == email);
            }
            else
            {
                users = await _userRepo.GetAll(u => u.UserName == username);
                
            }

            var user = (User)users.Where(u => u.IsActive).FirstOrDefault();

            return user;
        }

        public async Task<UserDto> Create(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);

            var users = await _userRepo.GetAll();
            var usersFiltered = users.Where(u => u.IsActive == true && u.UserName==user.UserName);

            user.Password = _encoderService.Encode(user.Password);

            await _userRepo.Add(user);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateById(int id, UpdateUserDto updateUserDto)
        {
            var user = await _userRepo.GetOne(u => u.UserId == id);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var updated = _mapper.Map(updateUserDto, user);

            return _mapper.Map<UserDto>(await _userRepo.Update(updated));
        }

        public async Task DeleteById(int id)
        {
            User user = await _userRepo.GetOne(u => u.UserId == id);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            user.IsActive = false;
            await _userRepo.Update(user);
        }

        
        public async Task<User> UpdateUserRolesById(int id, List<Role> roles)
        {
            var user = await _userRepo.GetOne(u => u.UserId == id);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            user.Roles = roles;

            return await _userRepo.Update(user);
        }

        public async Task<List<Role>> GetRolesOfUserById(int id)
        {
            var user = await _userRepo.GetOne(u => u.UserId == id);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return user.Roles.ToList();
        }

        

        
    }
}