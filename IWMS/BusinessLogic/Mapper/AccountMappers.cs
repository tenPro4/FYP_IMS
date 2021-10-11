using AutoMapper;
using BusinessLogic.Dtos.Account;
using EntityFramework.Entities;
using EntityFrameworkExtension.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Mapper
{
    public static class AccountMappers
    {
        internal static IMapper Mapper { get; set; }

        static AccountMappers()
        {
            Mapper = new MapperConfiguration(
                cfg => cfg.AddProfile<AccountMapperProfile>())
                .CreateMapper();
        }

        public static AccountDto ToModel(this MasterAccount account)
        {
            return Mapper.Map<AccountDto>(account);
        }

        public static AccountsDto ToModel(this PagedList<MasterAccount> accounts)
        {
            return Mapper.Map<AccountsDto>(accounts);
        }

        public static MasterAccount ToEntity(this AccountDto account)
        {
            return Mapper.Map<MasterAccount>(account);
        }

        public static List<T> ToAccountListModel<T>(this object o)
        {
            return Mapper.Map<List<T>>(o);
        }

        public static T ToAccountModel<T>(this object o)
        {
            return Mapper.Map<T>(o);
        }
    }
}
