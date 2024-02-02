using System.Web;
using BookwormsOnlineAssignment.Models;
using BookwormsOnlineAssignment.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookwormsOnlineAssignment.Pages
{
	[ValidateAntiForgeryToken]
	public class RegisterModel : PageModel
    {
        private UserManager<ApplicationUser> userManager { get; }
        private SignInManager<ApplicationUser> signInManager { get; }

        [BindProperty]
        public Register RModel { get; set; }

        public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public void OnGet()
        {
        }

        //Save data into the database
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
                var protector = dataProtectionProvider.CreateProtector("MySecretKey");

                var user = new ApplicationUser()
                {
                    UserName = RModel.Email.Trim(),
                    Email = RModel.Email.Trim(),
                    FirstName = RModel.FirstName.Trim(),
                    LastName = RModel.LastName.Trim(),
                    CreditCard = protector.Protect(RModel.CreditCard),
                    PhoneNumber = RModel.PhoneNumber,
                    BillingAddress = HttpUtility.HtmlEncode(RModel.BillingAddress.Trim()),
                    ShippingAddress = HttpUtility.HtmlEncode(RModel.ShippingAddress.Trim())
                };
                var result = await userManager.CreateAsync(user, RModel.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);

					HttpContext.Session.SetString("ID", user.Id);
					HttpContext.Session.SetString("Email", user.Email);
					HttpContext.Session.SetString("FirstName", user.FirstName);
					HttpContext.Session.SetString("LastName", user.LastName);
					HttpContext.Session.SetString("CreditCard", protector.Unprotect(user.CreditCard));
					HttpContext.Session.SetString("PhoneNumber", user.PhoneNumber);
					HttpContext.Session.SetString("BillingAddress", user.BillingAddress);
					HttpContext.Session.SetString("ShippingAddress", user.ShippingAddress);

					return RedirectToPage("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }

    }
}
