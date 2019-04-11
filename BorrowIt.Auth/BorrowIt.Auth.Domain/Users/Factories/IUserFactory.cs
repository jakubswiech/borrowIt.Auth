using BorrowIt.Auth.Domain.Users.DataStructure;

namespace BorrowIt.Auth.Domain.Users.Factories
{
    public interface IUserFactory
    {
        User CreateUser(UserDataStructure userDataStructure);
    }
}