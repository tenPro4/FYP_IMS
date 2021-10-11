using AutoMapper;
using BusinessLogic.Dtos.Account;
using IWMS.Dtos.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Mappers.Account
{
    public class AccountApiMapperProfile : Profile
    {
        public AccountApiMapperProfile()
        {
            CreateMap<AccountDto, AccountApiDto>(MemberList.Destination);

            //api to model
            CreateMap<AccountApiDto, AccountDto>(MemberList.Destination);
        }

    }
}
