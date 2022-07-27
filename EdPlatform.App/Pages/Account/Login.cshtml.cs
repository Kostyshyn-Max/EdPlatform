using EdPlatform.App.Models;
using EdPlatform.Business.Models;
using EdPlatform.Business.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Claims;

namespace EdPlatform.App.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        private readonly IUserService _userService;

        public LoginModel(ILogger<LoginModel> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }


        [TempData]
        public string? ErrorMessage { get; set; }

        [BindProperty]
        public InputModel? Input { get; set; }
        public class InputModel
        {
            [Required]
            public string? Login { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string? Password { get; set; }
        }

        public async Task OnGetAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user =  await _userService.Login(new UserLoginModel() { Login = Input?.Login, Password = Input?.Password });

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                    return Page();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("Login", user.Login),
                    new Claim(ClaimTypes.Role, "User")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperty = new AuthenticationProperties { };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperty
                );

                return Redirect("/");
            }

            return Page();
        }
    }
}
