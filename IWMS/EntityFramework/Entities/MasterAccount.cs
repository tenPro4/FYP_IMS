using EntityFramework.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EntityFramework.Entities
{
    public partial class MasterAccount: IdentityUser<int>, IBaseEntity
    {
        public DateTime ChangeDate { get; set; }
        //public DateTime? Verified { get; set; }
        public DateTime? PasswordReset { get; set; }
        //public bool isVerified => Verified.HasValue || PasswordReset.HasValue;

        //password reset token
        public DateTime? ResetTokenExpires { get; set; }

        public Employee Employee { get; set; }
        public RefreshToken RefreshToken { get; set; }
        public string VerificationToken { get; set; }
    }
}
