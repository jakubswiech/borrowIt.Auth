using BorrowIt.Common.Infrastructure.Abstraction.DTOs;

namespace BorrowIt.Auth.Application.DTOs
{
    public class UserSignedInDto : IDto
    {
        public string Token { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}