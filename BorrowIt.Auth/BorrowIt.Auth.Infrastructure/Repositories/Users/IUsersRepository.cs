using BorrowIt.Auth.Domain.Users;
using BorrowIt.Auth.Infrastructure.Entities.Users;
using BorrowIt.Common.Domain.Repositories;

namespace BorrowIt.Auth.Infrastructure.Repositories.Users
{
    public interface IUsersRepository : IGenericRepository<User, UserEntity>
    {
        
    }
}