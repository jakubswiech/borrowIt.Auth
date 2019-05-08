using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BorrowIt.Auth.Domain.Users;
using BorrowIt.Auth.Domain.Users.DataStructure;
using BorrowIt.Auth.Domain.Users.Events;
using BorrowIt.Auth.Domain.Users.Factories;
using BorrowIt.Auth.Infrastructure.Repositories.Users;
using BorrowIt.Common.Exceptions;
using BorrowIt.Common.Rabbit.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BorrowIt.Auth.Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IUserFactory _userFactory;
        private readonly IBusPublisher _busPublisher;
        private readonly IConfiguration _configuration;

        public UsersService(IUsersRepository usersRepository, IUserFactory userFactory, IBusPublisher busPublisher, IConfiguration configuration)
        {
            _usersRepository = usersRepository;
            _userFactory = userFactory;
            _busPublisher = busPublisher;
            _configuration = configuration;
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
                user.Roles.Select(x => x.ToString()), user.FirstName, user.SecondName, user.BirthDate, user.ModifyDate,
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

        public async Task<string> SignInAsync(UserDataStructure userDataStructure)
        {
            var user = await GetOneOrThrowAsync(userDataStructure.UserName);

            if (!BCrypt.Net.BCrypt.Verify(userDataStructure.Password, user.PasswordHash))
            {
                throw new BusinessLogicException("Invalid credentials.");
            }

            return GenerateToken(user);
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

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
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