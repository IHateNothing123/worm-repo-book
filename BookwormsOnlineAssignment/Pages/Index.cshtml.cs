using Microsoft.AspNetCore.Authorization;
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


        public void OnGet()
        {

        }
    }
}