using BusinessLogic.Resources.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.Helpers;

namespace BusinessLogic.Resources
{
    public class AuthenticationServiceResources : IAuthenticationServiceResources
    {
        public virtual ResourceMessage AccountExistKey()
        {
            return new ResourceMessage()
            {
                Code = nameof(AccountExistKey),
                Description = AuthenticationServiceResource.AccountExistKey
            };
        }

        public virtual ResourceMessage AccountIdNotExist()
        {
            return new ResourceMessage()
            {
                Code = nameof(AccountIdNotExist),
                Description = AuthenticationServiceResource.AccountIdNotExist
            };
        }

        public virtual ResourceMessage AccountNotExist()
        {
            return new ResourceMessage()
            {
                Code = nameof(AccountNotExist),
                Description = AuthenticationServiceResource.AccountNotExist
            };
        }

        public virtual ResourceMessage AccountPasswordInvalid()
        {
            return new ResourceMessage()
            {
                Code = nameof(AccountPasswordInvalid),
                Description = AuthenticationServiceResource.AccountPasswordInvalid
            };
        }

        public virtual ResourceMessage EmailAlreadlyConfirm()
        {
            return new ResourceMessage()
            {
                Code = nameof(EmailAlreadlyConfirm),
                Description = AuthenticationServiceResource.EmailAlreadlyConfirm
            };
        }

        public virtual ResourceMessage TokenFormatInvalid()
        {
            return new ResourceMessage()
            {
                Code = nameof(TokenFormatInvalid),
                Description = AuthenticationServiceResource.TokenFormatInvalid
            };
        }

        public virtual ResourceMessage TokenExpiredException()
        {
            return new ResourceMessage()
            {
                Code = nameof(TokenExpiredException),
                Description = AuthenticationServiceResource.TokenExpiredException
            };
        }
    }
}
