using Domain.Model.User;
using Domain.Service.Model.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.User
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public UserService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task AssignRoleToUserAsync(Guid userId, Guid roleId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                return;
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
                return;

            var result = await _userManager.AddToRoleAsync(user, role.Name);
            
        }

        public Task CreateRoleAsync()
        {
            throw new NotImplementedException();
        }

        public Task CreateUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteRoleAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync()
        {
            throw new NotImplementedException();
        }
    }
}
