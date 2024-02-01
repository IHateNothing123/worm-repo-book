using System.Net;
using BookwormsOnlineAssignment.Models;
using BookwormsOnlineAssignment.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace BookwormsOnlineAssignment.Pages
{
	public class LoginModel : PageModel
	{
		[BindProperty]
		public Login LModel { get; set; }

		private readonly SignInManager<ApplicationUser> signInManager;
		public LoginModel(SignInManager<ApplicationUser> signInManager)
		{
			this.signInManager = signInManager;
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

				var identityResult = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password, LModel.RememberMe, false);
				if (identityResult.Succeeded)
				{
                    HttpContext.Session.SetString("StudentName", "Tim");
                    HttpContext.Session.SetInt32("StudentId", 50);
                    return RedirectToPage("Index");
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
