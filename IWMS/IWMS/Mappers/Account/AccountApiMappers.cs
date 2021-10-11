using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Mappers.Account
{
    public static class AccountApiMappers
    {
        static AccountApiMappers()
        {
            Mapper = new MapperConfiguration(cfg => cfg.AddProfile<AccountApiMapperProfile>())
                .CreateMapper();
        }

        internal static IMapper Mapper { get; }

        public static T ToAccountApiModel<T>(this object source)
        {
            return Mapper.Map<T>(source);
        }
    }
}
