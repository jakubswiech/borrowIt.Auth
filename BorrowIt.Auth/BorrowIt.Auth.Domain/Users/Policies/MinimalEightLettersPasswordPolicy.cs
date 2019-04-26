using System.Text.RegularExpressions;

namespace BorrowIt.Auth.Domain.Users.Policies
{
    public class MinimalEightLettersPasswordPolicy : ITextValidationPolicy
    {
        public Regex Expression  => new Regex("^.{8,}$");
    }
}