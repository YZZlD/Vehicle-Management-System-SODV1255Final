using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class BaseController : Controller
{
    //Base controller is used to validate that a session exists to protect routes behind login

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if(HttpContext.Session.GetInt32("UserID") == null)
        {
            // Redirect to login if user session is not found from key
            context.Result = RedirectToAction("Login", "Auth");
            return;
        }

        base.OnActionExecuting(context);
    }
}
