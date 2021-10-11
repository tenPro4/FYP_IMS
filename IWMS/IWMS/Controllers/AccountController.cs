using BusinessLogic.Dtos.Account;
using BusinessLogic.Services.Interfaces;
using BusinessLogicShared.Request;
using IWMS.Dtos.Account;
using IWMS.ExceptionHandling;
using IWMS.Helpers;
using IWMS.Helpers.Localization;
using IWMS.Mappers.Account;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;

namespace IWMS.Controllers
{
    [TypeFilter(typeof(ControllerExceptionFilterAttribute))]
    [Produces("application/json")]
    public class AccountController : Controller
    {
        //private readonly IGenericControllerLocalizer<AccountController> _localizer;
        private readonly IStringLocalizer<AccountController> _localizer;
        private readonly IAuthenService AuthenService;
        private readonly IEmailService EmailService;
        private readonly IDepartmentService DepartmentService;

        public AccountController(IAuthenService authenService,
            IStringLocalizer<AccountController> localizer,
            IEmailService EmailService,
            IDepartmentService DepartmentService
            )
        {
            AuthenService = authenService;
            _localizer = localizer;
            this.EmailService = EmailService;
            this.DepartmentService = DepartmentService;
        }

        [HttpPost("api/account/register")]
        public async Task<ActionResult> Register([FromBody]AccountApiDto model)
        {
            var dto = model.ToAccountApiModel<AccountDto>();
            var createUser = await AuthenService.CreateAccountAsync(dto, Request.Headers["origin"]);

            if (!createUser)
                return BadRequest(createUser);

            return Ok();
        }

        [HttpPost("api/account/login")]
        public async Task<ActionResult> Login([FromBody]AccountApiDto model)
        {
            var dto = model.ToAccountApiModel<AccountDto>();
            var login = await AuthenService.SignInAsync(dto,ipAddress());

            if (!login.Success)
                return BadRequest(login);

            return Ok(login);
        }

        [HttpPost("api/account/validateEmail")]
        public async Task<IActionResult> ValidateEmail(ResetPasswordRequest req)
        {
            if(await AuthenService.VerifyEmail(req.Email, req.Token))
            {
                var acc = await AuthenService.GetByEmailAsync(req.Email);
                await DepartmentService.AssignToUncategoryDepartmentAsync(acc.Employee.EmployeeId);
                return Ok();
            }

            return BadRequest();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("api/account/logout")]
        public async Task<IActionResult> Logout()
        {
            var success = await AuthenService.LogoutAsync(Convert.ToInt16(HttpContext.GetUserId()));

            if (success)
                return NoContent();

            return BadRequest();
        }

        [HttpPost("api/account/resendVerifyEmail/{email}")]
        public async Task<IActionResult> ResendVerifyEmail([FromRoute]string email)
        {
            await AuthenService.ResendVerificationEmail(email, Request.Headers["origin"]);

            return Ok();
        }

        [HttpGet("api/account/refreshToken/{refreshToken}")]
        public async Task<IActionResult> Refresh([FromRoute]string refreshToken)
        {
            var authResponse = await AuthenService.RefreshAsync(refreshToken, Request.Headers["origin"]);

            if (!authResponse.Success)
            {
                return BadRequest(authResponse);
            }

            return Ok(authResponse);
        }

        [HttpGet("api/account/forgetPassword/{email}")]
        public async Task<IActionResult> ForgetPassword([FromRoute]string email)
        {
            if (!await AuthenService.AccountExistsAsync(email))
                return new NotFoundResult();

            await AuthenService.ForgotPassword(email, Request.Headers["origin"]);

            return Ok();
        }

        [HttpPost("api/account/resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordRequest req)
        {
            //if (!await AuthenService.AccountExistsAsync(req.Email))
            //    return new NotFoundResult();

            await AuthenService.ResetPassword(req);

            return Ok();
        }

        [HttpPost("api/account/changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody]ResetPasswordRequest req)
        {
            //if (!await AuthenService.AccountExistsAsync(req.Email))
            //    return new NotFoundResult();

            var result =await AuthenService.ChangePassword(Int32.Parse(HttpContext.GetUserId()),req);

            if (!result)
                return BadRequest();

            return Ok();
        }

        [HttpPost("api/account/testSendEmail")]
        public void TestSendEmail(string email)
        {
            EmailService.Send(email, "Test Send Email", @"<p>Send for testing purpose</p>");
        }

        [HttpGet("api/account/testLocalizer")]
        public string GetLocalizer()
        {
            return _localizer["EmailNotFound"];
        }

        [HttpPost("api/account/setLanguage")]
        public void SetLanguage(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

        //[HttpPost("api/user/uploadAvatar")]
        //public async Task<ActionResult> UploadImage(IFormFile file)
        //{

        //}
    }
}
