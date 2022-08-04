using EdPlatform.Business.Models;
using EdPlatform.Business.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace EdPlatform.App.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ILogger<RegisterModel> _logger;
        private readonly IUserService _userService;
        public RegisterModel(ILogger<RegisterModel> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [BindProperty]
        public InputModel? Input { get; set; }
        public class InputModel
        {
            [Required]
            public string? Login { get; set; }
            [Required]
            [EmailAddress]
            public string? Email { get; set; }
            [Required]
            [DataType(DataType.Password)]
            [Compare(nameof(ConfirmPassword))]
            public string? Password { get; set; }
            [Required]
            public string? ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync()
        {
            await HttpContext.SignOutAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _userService.Register(new UserRegisterModel() { Email = Input?.Email, Login = Input?.Login, Password = Input?.Password });

                return Redirect("/");
            }

            return Page();
        }
    }
}
