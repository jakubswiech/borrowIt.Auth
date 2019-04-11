using System;
using System.Text.RegularExpressions;
using BorrowIt.Auth.Domain.Users.Policies;
using BorrowIt.Common.Exceptions;
using BorrowIt.Common.Extensions;

namespace BorrowIt.Auth.Domain.Users.Helpers
{
    public static class PasswordValidator
    {
        public static void Validate<TPolicy>(this string password) where TPolicy : ITextValidationPolicy
        {
            password.ValidateNullOrEmptyString(nameof(password));
            var policy = Activator.CreateInstance<TPolicy>();

            CheckRegex(policy.Expression, password, nameof(password));
        }

        public static void Validate(this string text, Regex expression = null)
        {
            if (expression == null)
            {
                Validate<MinimalEightLettersPasswordPolicy>(text);
            }
            else
            {
                CheckRegex(expression, text);
            }  
        }

        private static void CheckRegex(Regex regex, string text, string propertyName = null)
        {
            if (!regex.IsMatch(text))
            {
                throw new BusinessLogicException(
                    $"{propertyName ?? nameof(text)} value does not match expression");
            }
        }
        
        
    }
}