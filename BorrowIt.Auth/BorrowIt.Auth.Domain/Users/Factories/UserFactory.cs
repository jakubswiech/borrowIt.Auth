using System;
using System.Collections.Generic;
using System.Linq;
using BorrowIt.Auth.Domain.Users.DataStructure;

namespace BorrowIt.Auth.Domain.Users.Factories
{
    public class UserFactory : IUserFactory
    {
        public User CreateUser(UserDataStructure userDataStructure)
        {
            
            var user = new User(
                GetOrCreateId(userDataStructure.Id)
                ,userDataStructure.Roles
                ,userDataStructure.Email
                ,userDataStructure.UserName
                ,userDataStructure.FirstName
                ,userDataStructure.SecondName
                ,userDataStructure.BirthDate);

            return user;
        }

        private Guid GetOrCreateId(Guid? id)
            => id ?? Guid.NewGuid();
    }
}