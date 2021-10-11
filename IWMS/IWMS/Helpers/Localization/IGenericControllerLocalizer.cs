using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Helpers.Localization
{
    public interface IGenericControllerLocalizer<T> : IStringLocalizer<T>
    {
    }
}
