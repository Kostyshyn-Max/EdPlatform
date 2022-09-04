using EdPlatform.Business.Models;
using EdPlatform.Business.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace EdPlatform.App.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        [ViewData]
        public UserModel UserData { get; set; }

        [BindProperty]
        public UserInputModel Input { get; set; }
        public class UserInputModel
        {
            public int UserId { get; set; }
            public string Login { get; set; }
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userService.GetUser(User.FindFirst("Login")?.Value);
            UserData = user;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _userService.UpdateUser(new UserModel() { Email = Input.Email, Login = Input.Login, UserId = Input.UserId });

            var user = await _userService.GetUser(Input.Login);
            UserData = user;

            //var identity = new ClaimsIdentity(User.Identity);

            //var claim = (from c in User.Claims
            //             where c.Type == "Login"
            //             select c).Single();

            //identity.RemoveClaim(claim);
            //identity.AddClaim(new Claim("Login", user.Login));

            await HttpContext.SignOutAsync();

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("Login", user.Login),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim("UserId", user.UserId.ToString())
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperty = new AuthenticationProperties { };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperty
            );

            return RedirectToPage("Index");
        }
    }
}
