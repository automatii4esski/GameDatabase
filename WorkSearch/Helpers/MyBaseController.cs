using Microsoft.AspNetCore.Mvc;

namespace WorkSearch.Helpers
{
    public class MyBaseController : Controller
    {
        public IActionResult TryRedirectBack(string reserveActionName = "Index")
        {
            var back = Request.Headers["Referer"].ToString();
            return back == "" ? RedirectToAction(reserveActionName) : Redirect(back);
        }
    }
}
