using BookwormsOnlineAssignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookwormsOnlineAssignment.Pages
{
	[Authorize]
	public class ChangePasswordModel : PageModel
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public ChangePasswordModel(
			UserManager<ApplicationUser> _userManager,
			SignInManager<ApplicationUser> _signInManager)
		{
			this._userManager = _userManager;
			this._signInManager = _signInManager;
		}

		[BindProperty]
		public string CurrentPassword { get; set; }

		[BindProperty]
		public string NewPassword { get; set; }

		public async Task<IActionResult> OnPostAsync()
		{
			var user = await _userManager.GetUserAsync(User);
			if (user == null)
			{
				return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
			}

			// Validate current password
			var result = await _userManager.ChangePasswordAsync(user, CurrentPassword, NewPassword);
			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
				return Page();
			}

			// Password changed successfully
			await _signInManager.RefreshSignInAsync(user);
			return RedirectToPage("./Index");
		}

		public void OnGet()
		{
		}
	}
}
