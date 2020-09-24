using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Model.User
{
    public interface IUserService
    {
        Task CreateUserAsync();
        Task CreateRoleAsync();
        Task DeleteUserAsync();
        Task DeleteRoleAsync();
        Task UpdateUserAsync();
        Task AssignRoleToUserAsync(Guid userId, Guid roleId);
    }
}
