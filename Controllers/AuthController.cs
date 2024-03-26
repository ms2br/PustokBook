using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PustokBook.Areas.Admin.Helpers;
using PustokBook.Helpers;
using PustokBook.Models;
using PustokBook.Services.Interfaces;
using PustokBook.ViewModels.AuthVM;
using System.Web;

namespace PustokBook.Controllers
{
    public class AuthController : Controller
    {
        UserManager<User> _um { get; }
        SignInManager<User> _sign { get; }
        IEmailService _emailService { get; }
        RoleManager<IdentityRole> _role { get; }
        public AuthController(UserManager<User> um, SignInManager<User> sign, IEmailService emailService, RoleManager<IdentityRole> role)
        {
            _emailService = emailService;
            _um = um;
            _sign = sign;
            _role = role;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View(register);

            User user = new User
            {
                Email = register.Email,
                UserName = register.UserName,
                ProfilActiveImageUrl = await register.ProfilActiveImageUrl.SaveAsync(PathConstants.ProfilImage)
            };
            var result = await _um.CreateAsync(user, register.Password); ;

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(" ", item.Description);
                }
                return View(register);

            }
            var roleResult = await _um.AddToRoleAsync(user, Roles.SuperAdmin.ToString());
            sendEmailConfirmed(user).Wait();
            return RedirectToAction(nameof(Login));
        }

        public async Task<IActionResult> SendEmailConfirmed(string userName)
        {
            sendEmailConfirmed(await _um.FindByNameAsync(userName)).Wait();
            return Content("Send Email");
        }

        async Task<string> EditEmailTemplate(User user, string link)
        {
            using StreamReader stream = new StreamReader(Path.Combine(PathConstants.RootPath, "comfirEmail.html"));
            string template = stream.ReadToEnd();
            template = template.Replace("[[[username]]]", user.UserName);
            template = template.Replace("[[[link]]]", link);
            return template;
        }

        async Task sendEmailConfirmed(User user)
        {
            string token = await _um.GenerateEmailConfirmationTokenAsync(user);
            var link = Url.Action("EmailConfirmed", "Auth", new
            {
                userName = user.UserName,
                token = token
            }, Request.Scheme);
            string template = await EditEmailTemplate(user, link);
            _emailService.Send("Email Confirmation", template, user.Email);
        }

        public async Task<IActionResult> EmailConfirmed(string userName, string token)
        {
            var result = await _um.ConfirmEmailAsync(await _um.FindByNameAsync(userName), token);
            if (!result.Succeeded) return Problem();
            return Ok();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            User user;
            if (login.UserNameOrUserEmail.Contains("@"))
            {
                user = await _um.FindByEmailAsync(login.UserNameOrUserEmail);
            }
            else
            {
                user = await _um.FindByNameAsync(login.UserNameOrUserEmail);
            }

            if (user == null)
            {
                ModelState.AddModelError("", "Username or Email or password is incorrect");
                return View(login);
            }

            var result = await _sign.PasswordSignInAsync(user, login.Password, login.Remember, true);

            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(" ", $"You are banned from entering the system for {DateTime.Parse(user.LockoutEnd.ToString()).ToString("HH:mm")} minutes.");
                }
                else if (!user.EmailConfirmed)
                {
                    ViewBag.Link = $"Go to <a href='{Url.Action("SendEmailConfirmed", "Auth", new { userName = user.UserName })}'>Send Email</a>";
                }
                else
                {
                    ModelState.AddModelError("", "Username or Email or password is incorrect");
                }
                return View(login);
            }
            return RedirectToAction(nameof(Index), "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _sign.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<bool> CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(Roles)))
            {
                var result = await _role.CreateAsync(new IdentityRole
                {
                    Name = item.ToString()
                });
            }
            return true;
        }

        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            User user = await _um.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Auth");

            if (!await _um.CheckPasswordAsync(user, data.OldPassword))
            {
                ModelState.AddModelError("OldPassword", "Wrong Password");
                return View(data);
            }

            var passwordResult = await _um.ChangePasswordAsync(user, data.OldPassword, data.NewPassword);

            if (!passwordResult.Succeeded)
            {
                foreach (var item in passwordResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(data);
            }

            await _sign.RefreshSignInAsync(user);
            return View();
        }

        public async Task<IActionResult> ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordEmailVM forget)
        {
            User user = await _um.FindByEmailAsync(forget.Email);
            if (user == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            string token = await _um.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action("ResetPassword", "Auth", new
            {
                userName = user.UserName,
                token = HttpUtility.UrlEncode(token)
            }, Request.Scheme);
            string template = await EditEmailTemplate(user, link);
            _emailService.Send("Please reset your password", template, user.Email);
            return RedirectToAction("Login", "Auth");
        }

        [HttpGet("[action]/{userName}/{token}")]
        public IActionResult ResetPassword(string userName, string token)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(token)) return Problem(statusCode: 505);
            return View();
        }

        [HttpPost("[action]/{userName}/{token}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM reset, string userName, string token)
        {
            if (!ModelState.IsValid) return View(reset);
            User user = await _um.FindByNameAsync(userName);
            string t = token;
            string s = HttpUtility.UrlDecode(token);
            if (user == null) return Problem();
            var result = await _um.ResetPasswordAsync(user, HttpUtility.UrlDecode(token), reset.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(" ", item.Description);
                }
                return View(reset);
            }
            return RedirectToAction("Login", "Auth");
        }
    }
}
