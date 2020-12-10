using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Security.IdentityServer.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Security.IdentityServer.Describer
{
    public class CustomErrorDescriber : IdentityErrorDescriber
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        public CustomErrorDescriber(IStringLocalizer<SharedResource> localizer)
        {
            _localizer = localizer;
        }
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError { Code = "DuplicateUserName", Description = string.Format(_localizer["DuplicateUserName"], userName) };
        }
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError { Code = "DuplicateEmail", Description = string.Format(_localizer["DuplicateEmail"], email) };
        }
        public override IdentityError DuplicateRoleName(string role)
        {
            return new IdentityError { Description = string.Format(_localizer["DuplicateRoleName"], role), Code = "DuplicateRoleName" };
        }
        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError { Code = "InvalidEmail", Description = string.Format(_localizer["InvalidEmail"], email) };
        }
        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError { Code = "InvalidUserName", Description = string.Format(_localizer["InvalidUserName"], userName) };
        }
        public override IdentityError InvalidRoleName(string role)
        {
            return new IdentityError { Code = "InvalidRoleName", Description = string.Format(_localizer["InvalidRoleName"], role) };
        }
        public override IdentityError InvalidToken() => new IdentityError { Code = "InvalidToken", Description = _localizer["InvalidToken"] };
        public override IdentityError PasswordMismatch() => new IdentityError { Code = "PasswordMismatch", Description = _localizer["PasswordMismatch"] };
        public override IdentityError UserAlreadyHasPassword() => new IdentityError { Code = "UserAlreadyHasPassword", Description = _localizer["UserAlreadyHasPassword"] };
        public override IdentityError UserAlreadyInRole(string role)
        {
            return new IdentityError { Code = "UserAlreadyInRole", Description = string.Format(_localizer["UserAlreadyInRole"], role) };
        }
        public override IdentityError UserNotInRole(string role)
        {
            return new IdentityError { Code = "UserNotInRole", Description = string.Format(_localizer["UserNotInRole"], role) };
        }
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError { Code = "PasswordTooShort", Description = string.Format(_localizer["PasswordTooShort"], length) };
        }
        public override IdentityError UserLockoutNotEnabled() => new IdentityError { Code = "UserLockoutNotEnabled", Description = _localizer["UserLockoutNotEnabled"] };
        public override IdentityError ConcurrencyFailure() => new IdentityError { Code = "ConcurrencyFailure", Description = _localizer["ConcurrencyFailure"] };
        public override IdentityError LoginAlreadyAssociated() => new IdentityError { Code = "LoginAlreadyAssociated", Description = _localizer["LoginAlreadyAssociated"] };
        public override IdentityError RecoveryCodeRedemptionFailed() => new IdentityError { Code = "RecoveryCodeRedemptionFailed", Description = _localizer["RecoveryCodeRedemptionFailed"] };
        public override IdentityError DefaultError() => new IdentityError { Code = "DefaultError", Description = _localizer["DefaultError"] };
        public override IdentityError PasswordRequiresDigit() => new IdentityError { Code = "PasswordRequiresDigit", Description = _localizer["PasswordRequiresDigit"] };
        public override IdentityError PasswordRequiresLower() => new IdentityError { Code = "PasswordRequiresLower", Description = _localizer["PasswordRequiresLower"] };
        public override IdentityError PasswordRequiresNonAlphanumeric() => new IdentityError { Code = "PasswordRequiresNonAlphanumeric", Description = _localizer["PasswordRequiresNonAlphanumeric"] };
        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
        {
            return new IdentityError { Code = "PasswordRequiresUniqueChars", Description = string.Format(_localizer["PasswordRequiresUniqueChars"], uniqueChars) };
        }
        public override IdentityError PasswordRequiresUpper() => new IdentityError { Code = "PasswordRequiresUpper", Description = _localizer["PasswordRequiresUpper"] };
    }
}
