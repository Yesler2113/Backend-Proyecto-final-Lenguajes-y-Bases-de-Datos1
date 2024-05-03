using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Red_Social_Proyecto.Entities;
using Microsoft.AspNetCore.Identity.UI.Services;
using Red_Social_Proyecto.ResetPassword;
using MyProjectEmailSender = Red_Social_Proyecto.ResetPassword.IEmailSender;


namespace Red_Social_Proyecto.Controllers
{
    [Route("api/reset")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UsersEntity> _userManager;
        private readonly MyProjectEmailSender _emailSender;

        public AccountController(UserManager<UsersEntity> userManager, MyProjectEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                // No revelar si el usuario no existe o no ha confirmado su email
                return Ok("Please check your email to reset your password.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action(
                nameof(ResetPassword),
                "Account",
                new { token, email = user.Email },
                protocol: HttpContext.Request.Scheme);

            await _emailSender.SendEmailAsync(
                user.Email,
                "Reset Password",
                $"Please reset your password by clicking <a href='{callbackUrl}'>here</a>."
            );

            return Ok("Please check your email to reset your password.");
        }
        [HttpGet("reset-password")]
        public IActionResult ResetPassword(string token, string email)
        {
            // Retornar una vista o una respuesta que incluya el token y el email
            // Si estás usando un frontend separado, redirige con los parámetros
            return Redirect($"https://yourfrontend.com/reset-password?token={Uri.EscapeDataString(token)}&email={Uri.EscapeDataString(email)}");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return BadRequest("Invalid request");
            }

            var result = await _userManager.ResetPasswordAsync(user, request.ResetCode, request.NewPassword);
            if (result.Succeeded)
            {
                return Ok("Your password has been reset successfully.");
            }

            return BadRequest("Error resetting password");
        }

    }
}
