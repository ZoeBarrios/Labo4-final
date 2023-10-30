using System.Net;
using System.Web.Http;
using EcommerceAPI.Models.Role;
using EcommerceAPI.Repositories;

namespace EcommerceAPI.Services
{
    public class RoleService
    {
        private readonly IRoleRepository _roleRepo;
        public RoleService(IRoleRepository roleRepo)
        {
            _roleRepo = roleRepo;
        }

        public async Task<Role> GetRoleByName(string name)
        {
            var role = await _roleRepo.GetOne(r => r.Name == name);
            return role;
        }

        public async Task<List<Role>> GetRolesByIds(List<int> roleIds)
        {
            if (roleIds == null || roleIds.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            var roles = await _roleRepo.GetAll(r => roleIds.Contains(r.Id));
            return roles.ToList();
        }
    }
}
