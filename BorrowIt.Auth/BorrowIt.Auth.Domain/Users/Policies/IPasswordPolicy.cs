using System.Text.RegularExpressions;

namespace BorrowIt.Auth.Domain.Users.Policies
{
    public interface IPasswordPolicy
    {
        Regex Expression { get; }
    }
}