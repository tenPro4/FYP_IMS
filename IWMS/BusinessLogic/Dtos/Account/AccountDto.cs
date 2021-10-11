using BusinessLogic.Dtos.Employee;
using BusinessLogic.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Dtos.Account
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Remember { get; set; }

        [NoMap]
        public string Password { get; set; }
        [NoMap]
        public string ResetToken { get; set; }

        public string PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public string SecurityStamp { get; set; }
        public DateTime ChangeDate { get; set; }

        public DateTime? PasswordReset { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public string VerificationToken { get; set; }

        public EmployeeDto Employee { get; set; }
    }
}
