using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessLogicShared.Request
{
    public class ResetPasswordRequest
    {
        public string Email { get; set; }

        public string Token { get; set; }

        public string OldPassword { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
