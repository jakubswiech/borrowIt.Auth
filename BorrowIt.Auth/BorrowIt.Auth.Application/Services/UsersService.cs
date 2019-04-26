using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using BorrowIt.Auth.Domain.Users;
using BorrowIt.Auth.Domain.Users.DataStructure;
using BorrowIt.Auth.Domain.Users.Events;
using BorrowIt.Auth.Domain.Users.Factories;
using BorrowIt.Auth.Infrastructure.Repositories.Users;
using BorrowIt.Common.Exceptions;
using BorrowIt.Common.Rabbit.Abstractions;

namespace BorrowIt.Auth.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IUserFactory _userFactory;
        private readonly IBusPublisher _busPublisher;

        public UsersService(IUsersRepository usersRepository, IUserFactory userFactory, IBusPublisher busPublisher)
        {
            _usersRepository = usersRepository;
            _userFactory = userFactory;
            _busPublisher = busPublisher;
        }
        
        public async Task AddUserAsync(UserDataStructure userDataStructure)
        {
            try
            {
                await GetOneOrThrowAsync(userDataStructure.UserName);
            }
            catch (BusinessLogicException ex)
            {
                
            }

            var user = _userFactory.CreateUser(userDataStructure);
            ValidatePasswords(userDataStructure.Password, userDataStructure.ConfirmPassword);
            user.SetPassword(userDataStructure.Password);

            await _usersRepository.CreateAsync(user);

            var userCratedEvent = new UserCreatedEvent(user.Id, user.UserName, user.Email,
                user.Roles?.Select(x => x.Name), user.FirstName, user.SecondName, user.BirthDate, user.ModifyDate,
                new AddressEventData()
                {
                    City = user.Address.City,
                    PostalCode = user.Address.City,
                    Street = user.Address.Street
                });

            await _busPublisher.PublishAsync(userCratedEvent);
        }

        public async Task UpdateUserAsync(UserDataStructure userDataStructure)
        {
            if (userDataStructure?.Id == null)
            {
                throw new BusinessLogicException("Missing id");
            }

            var user = await GetOneOrThrowAsync(userDataStructure.Id.Value);
            
            user.UpdateUser(userDataStructure.Email,
                userDataStructure.UserName,
                userDataStructure.FirstName,
                userDataStructure.SecondName,
                userDataStructure.BirthDate, userDataStructure.Roles, new Address(userDataStructure.PostalCode, userDataStructure.Street, userDataStructure.City));

            await _usersRepository.UpdateAsync(user);
        }

        public async Task RemoveUserAsync(Guid id)
        {
            var user = await GetOneOrThrowAsync(id);
            await _usersRepository.RemoveAsync(user);
        }

        public async Task SetPasswordAsync(string userName, string password, string confirmPassword)
        {
            var user = await GetOneOrThrowAsync(userName);
            ValidatePasswords(password, confirmPassword);
            user.SetPassword(password);

            await _usersRepository.UpdateAsync(user);
        }

        public async Task ChangePasswordAsync(string userName, string oldPassword, string newPassword, string confirmPassword)
        {
            var user = await GetOneOrThrowAsync(userName);

            if (!BCrypt.Net.BCrypt.Verify(oldPassword, user.PasswordHash))
            {
                throw new BusinessLogicException("Invalid password");
            }
            
            ValidatePasswords(newPassword, confirmPassword);
            user.SetPassword(newPassword);

            await _usersRepository.UpdateAsync(user);
        }

        private async Task<User> GetOneOrThrowAsync(Guid id)
        {
            var user = await _usersRepository.GetAsync(id);
            if (user == null)
            {
                throw new BusinessLogicException("User doesn't exist");
            }

            return user;
        }

        private async Task<User> GetOneOrThrowAsync(string userName)
        {
            var user = (await _usersRepository.GetWithExpressionAsync(x => x.UserName == userName)).SingleOrDefault();
            if (user == null)
            {
                throw new BusinessLogicException("User doesn't exist");
            }

            return user;
        }

        private void ValidatePasswords(string password, string confirmPassword)
        {
            if (!password.Equals(confirmPassword))
            {
                throw new BusinessLogicException("Passwords are not equal");
            }
        }
    }
}