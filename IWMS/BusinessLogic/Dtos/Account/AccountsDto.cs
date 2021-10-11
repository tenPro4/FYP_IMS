using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Dtos.Account
{
    public class AccountsDto
    {
        public AccountsDto()
        {
            Accounts = new List<AccountDto>();
        }

        public List<AccountDto> Accounts { get; set; }

        public int TotalCount { get; set; }

        public int PageSize { get; set; }
    }
}
