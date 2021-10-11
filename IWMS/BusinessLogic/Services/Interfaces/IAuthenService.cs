using BusinessLogic.Dtos.Account;
using BusinessLogic.Dtos.Employee;
using BusinessLogicShared.Common;
using BusinessLogicShared.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Interfaces
{
    public interface IAuthenService
    {
        Task<bool> CreateAccountAsync(AccountDto model, string origin);
        Task<AuthenticationResult> SignInAsync(AccountDto model,string ipAddress);
        Task<bool> AccountExistsAsync(string username);
        Task<bool> UpdateAsync(AccountDto model);
        Task<AccountDto> GetByIdAsync(int id);
        Task<AccountDto> GetByEmailAsync(string email);
        Task<AuthenticationResult> RefreshAsync(string refreshToken,string ipAddress);
        Task<bool> VerifyEmail(string email,string token);
        Task ForgotPassword(string email, string origin);
        Task ResetPassword(ResetPasswordRequest model);
        Task<bool> ChangePassword(int id,ResetPasswordRequest model);
        Task ResendVerificationEmail(string email, string origin);
        Task<bool> LogoutAsync(int accountId);
    }
}
