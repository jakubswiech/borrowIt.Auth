using System;
using BorrowIt.Auth.Domain.Users.Policies;
using BorrowIt.Common.Extensions;

namespace BorrowIt.Auth.Domain.Users.Helpers
{
    public static class PasswordValidator
    {
        public static void Validate<TPolicy>(this string password) where TPolicy : IPasswordPolicy
        {
            password.ValidateNullOrEmptyString(nameof(password));
            var policy = Activator.CreateInstance<TPolicy>();
            
            if (!policy.Expression.IsMatch(password))
            {
                throw new Exception("Password does not match policy");
            }
        }

        public static void Validate(this string password)
        {
            Validate<MinimalEightLettersPasswordPolicy>(password);
        }
    }
}