using System;
using BorrowIt.Common.Infrastructure.Abstraction.Queries;

namespace BorrowIt.Auth.Application.Queries
{
    public class GetUserQuery : IQuery
    {
        public Guid Id { get; set; }
    }
}