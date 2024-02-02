using BookwormsOnlineAssignment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookwormsOnlineAssignment.Pages
{
    public class LogoutNowModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        public LogoutNowModel(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToPage("Login");
        }
    }
}
