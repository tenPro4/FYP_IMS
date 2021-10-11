using BusinessLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Resources.Interfaces
{
    public interface IAuthenticationServiceResources
    {
        ResourceMessage AccountExistKey();
        ResourceMessage AccountNotExist();
        ResourceMessage AccountIdNotExist();
        ResourceMessage AccountPasswordInvalid();
        ResourceMessage EmailAlreadlyConfirm();
        ResourceMessage TokenFormatInvalid();
        ResourceMessage TokenExpiredException();
    }
}
