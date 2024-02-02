using System.Net;
using BookwormsOnlineAssignment.Models;
using BookwormsOnlineAssignment.ViewModels;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace BookwormsOnlineAssignment.Pages
{
    [ValidateAntiForgeryToken]
    public class LoginModel : PageModel
	{
		[BindProperty]
		public Login LModel { get; set; }

		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly UserManager<ApplicationUser> userManager;
		public LoginModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
		{
			this.signInManager = signInManager;
			this.userManager = userManager;
		}

        //private readonly IHttpContextAccessor contxt;
        //public LoginModel(IHttpContextAccessor httpContextAccessor)
        //{
        //    contxt = httpContextAccessor;
        //}

        public void OnGet()
		{
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
				if (!ReCaptchaPassed(Request.Form["foo"]))
				{
					ModelState.AddModelError(string.Empty, "You failed the CAPTCHA.");
					return Page();
				}

				var identityResult = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password, LModel.RememberMe, true);
				if (identityResult.Succeeded)
				{
					var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
					var protector = dataProtectionProvider.CreateProtector("MySecretKey");
					var user = await userManager.FindByEmailAsync(LModel.Email);

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

				if (identityResult.IsLockedOut)
				{
                    ModelState.AddModelError("", "Account has been locked. Please wait a few minutes and try again.");
                }

                ModelState.AddModelError("", "Username or Password incorrect");
			}
			return Page();
		}

		public static bool ReCaptchaPassed(string gRecaptchaResponse)
		{
			HttpClient httpClient = new HttpClient();

			var res = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret=6LcVzGIpAAAAAKhenfMa_2qyFdoQPKYz1ig3puyS&response={gRecaptchaResponse}").Result;

			if (res.StatusCode != HttpStatusCode.OK)
			{
				return false;
			}
			string JSONres = res.Content.ReadAsStringAsync().Result;
			dynamic JSONdata = JObject.Parse(JSONres);

			if (JSONdata.success != "true" || JSONdata.score <= 0.5m)
			{
				return false;
			}

			return true;
		}
	}
}
