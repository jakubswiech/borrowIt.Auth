using AutoMapper;
using BorrowIt.Auth.Domain.Users;
using BorrowIt.Auth.Infrastructure.Entities.Users;
using BorrowIt.Common.Mongo.Repositories;
using MongoDB.Driver;

namespace BorrowIt.Auth.Infrastructure.Repositories.Users
{
    public class UsersRepository : GenericMongoRepository<User, UserEntity>, IUsersRepository
    {
        public UsersRepository(IMongoDatabase database, IMapper mapper) : base(database, mapper)
        {
        }
    }
}