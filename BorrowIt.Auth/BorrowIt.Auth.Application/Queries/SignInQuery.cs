using System.Runtime.Serialization;
using BorrowIt.Common.Infrastructure.Abstraction.Queries;

namespace BorrowIt.Auth.Application.Queries
{
    public class SignInQuery : IQuery
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}