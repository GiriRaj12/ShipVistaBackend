using Microsoft.AspNetCore.Mvc.Filters;
using ShipVista.Models;

namespace ShipVista.Filters;

public class UserActionsFilter : ActionFilterAttribute{

    IUserService _iuserService;

    public UserActionsFilter(IUserService _iuserservice){
        this._iuserService = _iuserservice;
    }

    public override void OnActionExecuting(ActionExecutingContext filterContext){
        var req = filterContext.HttpContext.Request;
        string auth = req.Headers["Authorization"];
        if (!String.IsNullOrEmpty(auth))
        {
            Console.WriteLine(auth);
            var cred = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(auth.Substring(6))).Split(':');
            Console.WriteLine(cred[0] + ","+ cred[1]);
            APIResponse apiResponse = _iuserService.checkUserDetails(cred[0], cred[1]);
            if(apiResponse.isResponse){
                return;
            } 
        }
        
        var res = filterContext.HttpContext.Response;
        res.Headers["WWW-Authenticate"] = String.Format("Basic realm=\"{0}\"", "user_action_realm" ?? "Ryadel");
        filterContext.Result = new UnAuthorizedResulst();
    }

}