using BookwormsOnlineAssignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookwormsOnlineAssignment.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        //private readonly ILogger<IndexModel> _logger;

        //public IndexModel(ILogger<IndexModel> logger)
        //{
        //    _logger = logger;
        //}

        //private readonly IHttpContextAccessor contxt;
        //public IndexModel(IHttpContextAccessor httpContextAccessor)
        //{
        //    contxt = httpContextAccessor;
        //}


        private readonly SignInManager<ApplicationUser> signInManager;
        public IndexModel(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet()
        {
            var email = HttpContext.Session.GetString("Email");
            if (email == null)
            {
                // await signInManager.SignOutAsync();
                return RedirectToPage("LogoutNow");

            }
            
            // session is still valid
            return null;
        }
    }
}