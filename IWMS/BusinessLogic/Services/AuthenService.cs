using BusinessLogic.Dtos.Account;
using BusinessLogic.Dtos.Employee;
using BusinessLogic.Mapper;
using BusinessLogic.Resources.Interfaces;
using BusinessLogic.Services.Interfaces;
using BusinessLogicShared.Common;
using BusinessLogicShared.ExceptionHandling;
using BusinessLogicShared.Request;
using BusinessLogicShared.Security;
using EntityFramework.Entities;
using EntityFramework.Repositories.Interfaces;
using EntityFramework.Specifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Extensions;

namespace BusinessLogic.Services
{
    public class AuthenService : IAuthenService
    {
        private readonly IAsyncRepository<MasterAccount> _accountRepository;
        private readonly UserManager<MasterAccount> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IAsyncRepository<RefreshToken> _refreshTokenRepository;
        private readonly JwtSettings _jwtSetting;
        private readonly TokenValidationParameters _tokenValidation;
        private readonly IAuthenticationServiceResources _authResouces;
        private readonly IEmployeeService _empService;
        private readonly IEmailService _emailService;
        private readonly IPermissionService permissionService;

        public AuthenService(
            IAsyncRepository<MasterAccount> accountRepository,
            JwtSettings jwtSetting, 
            TokenValidationParameters tokenValidation,
            IAuthenticationServiceResources authResouces,
            IEmployeeService empService,
            IAsyncRepository<RefreshToken> refreshTokenRepository,
            IEmailService emailService,
            UserManager<MasterAccount> userManager,
            RoleManager<IdentityRole<int>> _roleManager,
            IPermissionService permissionService
            )
        {
            _accountRepository = accountRepository;
            _jwtSetting = jwtSetting;
            _tokenValidation = tokenValidation;
            _authResouces = authResouces;
            _empService = empService;
            _refreshTokenRepository = refreshTokenRepository;
            _emailService = emailService;
            _userManager = userManager;
            this._roleManager = _roleManager;
            this.permissionService = permissionService;
        }

        public virtual async Task<bool> CreateAccountAsync(AccountDto model,string origin)
        {
            if (await AccountExistsAsync(model.Email))
            {
                //return new AuthenticationResult
                //{
                //    Errors = new[] { "Email or username alreadly exists!" }
                //};
                //sendAlreadyRegisteredEmail(model.Email, origin);
                //throw new UserFriendlyErrorPageException(
                //    string.Format(_authResouces.AccountExistKey().Description));
            }

            model.ChangeDate = DateTime.Now;
            //model.VerificationToken = randomTokenString();
            model.UserName = model.Email;
            var identityEntity = model.ToEntity();

            var result = await _userManager.CreateAsync(identityEntity,model.Password);
            
            if (!result.Succeeded)
            {
                List<ViewErrorMessage> vmList = new List<ViewErrorMessage>();
                foreach (var err in result.Errors)
                {
                    ViewErrorMessage vm = new ViewErrorMessage(err.Code, err.Description);
                    vmList.Add(vm);
                }
                throw new UserFriendlyViewException("Registration Fail",result.Errors.ToString(),vmList,model);

            }

            await ResendVerificationEmail(model.Email, origin);
            //model.SecurityStamp = await _userManager.GenerateEmailConfirmationTokenAsync(identityEntity); 

            var newEmployee = new EmployeeDto
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = "m",
                CardNo = Guid.NewGuid().ToString(),
                BirthDate = DateTime.Now,
                HireDate = DateTime.Now,
                ChangedDate = DateTime.Now,
                AccountId = identityEntity.Id
            };

            //sendVerificationEmail(model, origin);

            return await _empService.AddAsync(newEmployee) != null;
            //return await GenerateAuthenticationResultForUserAsync(result.AccountId);
        }

        private void sendVerificationEmail(AccountDto account, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var verifyUrl = $"{origin}/verify-email?token={account.SecurityStamp}&email={account.Email}";
                message = $@"<p>Please click the below link to verify your email address:</p>
                             <p><a href=""{verifyUrl}"">{verifyUrl}</a></p>";
            }
            else
            {
                var verifyUrl = $"http://localhost:3000/verify-email?token={account.SecurityStamp}&email={account.Email}";
                message = $@"<p>Please click the below link to verify your email address:</p>
                             <p><a href=""{verifyUrl}"">{verifyUrl}</a></p>";
            }

            _emailService.Send(
                to: account.Email,
                subject: "Sign-up Verification API - Verify Email",
                html: $@"<h4>Verify Email</h4>
                         <p>Thanks for registering!</p>
                         {message}"
            );
        }

        public async Task<bool> VerifyEmail(string email,string token)
        {
            var account = await _userManager.FindByEmailAsync(email);

            if (account == null)
            {
                throw new UserFriendlyErrorPageException(
                    string.Format(_authResouces.AccountNotExist().Description, email),
                    _authResouces.AccountNotExist().Code
                    );
            }

            //account.Verified = DateTime.UtcNow;
            var identityResult = await _userManager.ConfirmEmailAsync(account, token);
            //account.VerificationToken = null;

            return identityResult.Succeeded;
        }

        public async Task ForgotPassword(string email, string origin)
        {
            var account = await _accountRepository.GetSingleAsync(x => x.Email == email);

            if (account == null)
            {
                //throw new AppException("Verification failed");
            }

            //account.ResetToken = randomTokenString();
            account.ResetTokenExpires = DateTime.UtcNow.AddDays(1);

            await _accountRepository.UpdateAsync(account);
            var model = account.ToModel();
            model.ResetToken = await _userManager.GeneratePasswordResetTokenAsync(account);

            sendPasswordResetEmail(model, origin);
        }

        public async Task ResetPassword(ResetPasswordRequest model)
        {
            if (!String.Equals(model.Password, model.ConfirmPassword))
            {
                throw new UserFriendlyErrorPageException("Please ensure both password provided is same.");
            }
            var account = await _userManager.FindByEmailAsync(model.Email);

            if (account.ResetTokenExpires < DateTime.Now)
                throw new SecurityTokenExpiredException(_authResouces.TokenExpiredException().Description);

            var result = await _userManager.ResetPasswordAsync(account, model.Token, model.Password);

            if(!result.Succeeded)
                throw new UserFriendlyErrorPageException(result.Errors.FirstOrDefault().Description,
                   "ResetFail");

            //account.PasswordHash = BC.HashPassword(model.Password);
            account.PasswordReset = DateTime.UtcNow;
            account.ChangeDate = DateTime.Now;
            account.ResetTokenExpires = null;

            await _accountRepository.UpdateAsync(account);
        }

        private void sendPasswordResetEmail(AccountDto account, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var resetUrl = $"{origin}/resetPassword?token={account.ResetToken}&email={account.Email}";
                message = $@"<p>Please click the below link to reset your password, the link will be valid for 1 day:</p>
                             <p><a href=""{resetUrl}"">{resetUrl}</a></p>";
            }
            else
            {
                var resetUrl = $"http://localhost:3000/resetPassword?token={account.ResetToken}&email={account.Email}";
                message = $@"<p>Please click the below link to reset your password, the link will be valid for 1 day:</p>
                             <p><a href=""{resetUrl}"">{resetUrl}</a></p>";
            }

            _emailService.Send(
                to: account.Email,
                subject: "IMS Verification API - Reset Password",
                html: $@"<h4>Reset Password Email</h4>
                         {message}"
            );
        }

        public virtual async Task<AuthenticationResult> SignInAsync(AccountDto model,string ipAddress)
        {
            var account =await _userManager.FindByEmailAsync(model.Email);

            if (account == null)
            {
                throw new UserFriendlyErrorPageException(string.Format(_authResouces.AccountNotExist().Description, model.Email),
                    _authResouces.AccountNotExist().Code);
            }

            #region testing purpose
            if (await _refreshTokenRepository.ExistsAsync(x => x.AccountId == account.Id))
                await LogoutAsync(account.Id);

            #endregion

            if(!account.EmailConfirmed)
            {
                throw new UserFriendlyErrorPageException(
                    string.Format(_authResouces.AccountNotExist().Description,model.Email),
                    _authResouces.AccountNotExist().Description);
            }

            if (!await _userManager.CheckPasswordAsync(account, model.Password))
            {
                throw new UserFriendlyErrorPageException(
                   string.Format(_authResouces.AccountPasswordInvalid().Description, model.Password),
                   _authResouces.AccountPasswordInvalid().Description);
            }

            var jwtToken = await generateJwtToken(account.Id,1); //later transfor to enum class
            var refreshToken = model.Remember ? await generateRefreshToken(account.Id, ipAddress) : null;

            return new AuthenticationResult
            {
                Success = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                RefreshToken = refreshToken
            };

            //return await GenerateAuthenticationResultForUserAsync(account.AccountId);
        }

        private async Task<string> generateRefreshToken(int accountId, string ipAddress)
        {
            var refreshToken = new RefreshTokenDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(await generateJwtToken(accountId, 1)),
                AccountId = accountId,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(_jwtSetting.TokenLifeTime),
                CreatedByIp = ipAddress
            };

            var rtResult = await _refreshTokenRepository.AddAsync(refreshToken.ToAccountModel<RefreshToken>());
            //var oldRefreshToken = await _refreshTokenRepository
            //    .GetAsync(
            //    x => x.AccountId == accountId &&
            //    !x.IsActive &&
            //    x.CreationDate.Add(_jwtSetting.TokenLifeTime) <= DateTime.UtcNow);

            return rtResult.Token;
        }

        private async Task<SecurityToken> generateJwtToken(int accountId,int type)
        {
            var spec = new AccountSpecification(x => x.Id == accountId);
            var account = await _accountRepository.GetSingleAsync(spec);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = type == 1 ? Encoding.ASCII.GetBytes(_jwtSetting.Secret): Encoding.ASCII.GetBytes(_jwtSetting.Refresh);

            var claims = new List<Claim>
                {
                        new Claim(JwtRegisteredClaimNames.Sub,account.Email),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email,account.Email),
                        new Claim("Id",account.Id.ToString()),
                        new Claim("empId",account.Employee.EmployeeId.ToString()),
                };

            var userClaims =await _userManager.GetClaimsAsync(account);
            claims.AddRange(userClaims);

            var permissions = await permissionService.GetAllById(account.Employee.EmployeeId);
            if(permissions != null)
            {
                permissions.ForEach(x =>
                {
                    claims.Add(new Claim("permission", x.MasterPermission.PermissionName));
                });
            }

            var userRoles = await _userManager.GetRolesAsync(account);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
                var role = await _roleManager.FindByNameAsync(userRole);
                if (role == null) continue;
                var roleClaims = await _roleManager.GetClaimsAsync(role);

                foreach (var roleClaim in roleClaims)
                {
                    if (claims.Contains(roleClaim))
                        continue;
                    claims.Add(roleClaim);
                }
            }

            if (account.Employee.Department != null)
            {
                claims.Add(new Claim("department", account.Employee.Department.MasterDepartment.DepartmentName));
            }

            //if (account.Employee.Project != null)
            //{
            //    foreach(var project in account.Employee.Project)
            //    {
            //        claims.Add(new Claim("project", project.Project.Name));
            //    }
            //}

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(_jwtSetting.TokenLifeTime),
                SigningCredentials =
           new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.CreateToken(tokenDescriptor);
        }

        public async Task<bool> AccountExistsAsync(string email)
        {
            return await _accountRepository.ExistsAsync(x => x.Email == email || x.UserName == email);
        }

        public async Task<bool> UpdateAsync(AccountDto model)
        {
            var entity = await _accountRepository.GetByIdAsync(model.Id);

            entity.ChangeDate = model.ChangeDate;
            entity.PhoneNumber = model.PhoneNumber;
            entity.UserName = model.UserName;
            entity.Email = model.Email;

            return await _accountRepository.UpdateAsync(entity);
        }

        public async Task<AccountDto> GetByIdAsync(int id)
        {
            if (await _userManager.FindByIdAsync(id.ToString()) == null)
            {
                throw new UserFriendlyErrorPageException("account not exists");
            }

            var spec = new AccountSpecification(x => x.Id == id);
            var account = await _accountRepository.GetSingleAsync(spec);
            return account.ToModel();
        }

        public async Task<AuthenticationResult> RefreshAsync(string refreshToken,string origin)
        {
            var storedRefreshToken = await _refreshTokenRepository.GetSingleAsync(x => x.Token == refreshToken);
                
            if (storedRefreshToken == null)
            {
                return new AuthenticationResult {
                    Success = false,
                    Errors = new[] { "This refresh token does not exist" }
                };
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            {
                await LogoutAsync(storedRefreshToken.AccountId);
                return new AuthenticationResult {
                    Success = false,
                    Errors = new[] { "This refresh token has expired" }
                };
            }

            var jwtToken = await generateJwtToken(storedRefreshToken.AccountId, 1);
            //var rToken = await generateRefreshToken(storedRefreshToken.AccountId, origin);

            return new AuthenticationResult
            {
                Success = true,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                RefreshToken = refreshToken
            };
            //return await GenerateAuthenticationResultForUserAsync(Convert.ToInt32(validatedToken.Claims.Single(t => t.Type == "accountId").Value));
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token,int type)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = type == 1 ? Encoding.ASCII.GetBytes(_jwtSetting.Secret) : Encoding.ASCII.GetBytes(_jwtSetting.Refresh);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidateLifetime = true
            };

            var claims = tokenHandler.ValidateToken(token, tokenValidationParameters, out var tokenSecure);
            var Id = claims.Claims.Single(x => x.Type == "Id").Value;

            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidation, out var validatedToken);
                //if (!IsJwtWithValidationSecurityAlgorithm(validatedToken))
                //{
                //    return null;
                //}
                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidationSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                    jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                        StringComparison.InvariantCultureIgnoreCase);
        }

        private void CreatePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                        return false;
                }
            }

            return true;
        }

        private string randomTokenString()
        {
            var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private void sendAlreadyRegisteredEmail(string email, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
                message = $@"<p>If you don't know your password please visit the <a href=""{origin}/account/forgot-password"">forgot password</a> page.</p>";
            else
                message = "<p>If you don't know your password you can reset it via the <code>/accounts/forgot-password</code> api route.</p>";

            _emailService.Send(
                to: email,
                subject: "Sign-up Verification API - Email Already Registered",
                html: $@"<h4>Email Already Registered</h4>
                         <p>Your email <strong>{email}</strong> is already registered.</p>
                         {message}"
            );
        }

        public async Task ResendVerificationEmail(string email, string origin)
        {
            if (!string.IsNullOrWhiteSpace(email) && await AccountExistsAsync(email))
            {
                var user =await _userManager.FindByEmailAsync(email);
                if (!user.EmailConfirmed)
                {
                    AccountDto model = new AccountDto
                    {
                        Email = email,
                        SecurityStamp = await _userManager.GenerateEmailConfirmationTokenAsync(user)
                    };

                    sendVerificationEmail(model, origin);
                    return;
                }
                throw new UserFriendlyErrorPageException(
                    string.Format(_authResouces.EmailAlreadlyConfirm().Description, email),
                    _authResouces.EmailAlreadlyConfirm().Code
                    );
            }
            throw new ArgumentNullException();
        }

        public async Task<bool> LogoutAsync(int accountId)
        {
            if (await _refreshTokenRepository.ExistsAsync(x => x.AccountId == accountId))
                return await _refreshTokenRepository.DeleteAsync(await _refreshTokenRepository.GetSingleAsync(x => x.AccountId == accountId));

            return false;
        }

        public async Task<AccountDto> GetByEmailAsync(string email)
        {
            if (!await AccountExistsAsync(email))
                throw new UserFriendlyErrorPageException(
                    string.Format(_authResouces.AccountNotExist().Description, email),
                    _authResouces.AccountNotExist().Description);

            var spec = new AccountSpecification(x => x.Email == email);
            var entity = await _accountRepository.GetSingleAsync(spec);
            return entity.ToAccountModel<AccountDto>();
        }

        public async Task<bool> ChangePassword(int id,ResetPasswordRequest model)
        {
            var account =await _accountRepository.GetByIdAsync(id);
            var result = await _userManager.ChangePasswordAsync(account, model.OldPassword, model.Password);

            return result.Succeeded;
        }


    }
}
