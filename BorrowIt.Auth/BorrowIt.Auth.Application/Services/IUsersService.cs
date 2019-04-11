using System;
using System.Threading.Tasks;
using System.Xml.Linq;
using BorrowIt.Auth.Domain.Users;
using BorrowIt.Auth.Domain.Users.DataStructure;
using BorrowIt.Common.Application.Services;

namespace BorrowIt.Auth.Application.Services
{
    public interface IUsersService : IService
    {
        Task AddUserAsync(UserDataStructure userDataStructure);
        Task UpdateUserAsync(UserDataStructure userDataStructure);
        Task RemoveUserAsync(Guid id);
        Task SetPasswordAsync(string userName, string password, string confirmPassword);
        Task ChangePasswordAsync(string userName, string oldPassword, string newPassword, string confirmPassword);
    }
}