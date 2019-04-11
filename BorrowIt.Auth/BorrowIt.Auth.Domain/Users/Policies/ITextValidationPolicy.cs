using System.Text.RegularExpressions;

namespace BorrowIt.Auth.Domain.Users.Policies
{
    public interface ITextValidationPolicy
    {
        Regex Expression { get; }
    }
}