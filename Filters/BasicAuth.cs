using Microsoft.AspNetCore.Mvc.Core;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ShipVista.Filters;
public class BasicAuthenticationAttribute : ActionFilterAttribute
{
    public string BasicRealm { get; set; }
    protected string Username { get; set; }
    protected string Password { get; set; }

    public BasicAuthenticationAttribute(string username, string password)
    {
        this.Username = username;
        this.Password = password;
    }

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var req = filterContext.HttpContext.Request;
        string auth = req.Headers["Authorization"];
        Console.WriteLine(auth);
        if (!String.IsNullOrEmpty(auth))
        {
            var cred = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(auth.Substring(6))).Split(':');
            var user = new { Name = cred[0], Pass = cred[1] };
            if (user.Name == Username && user.Pass == Password) return;
        }
        var res = filterContext.HttpContext.Response;
        res.Headers["WWW-Authenticate"] = String.Format("Basic realm=\"{0}\"", BasicRealm ?? "Ryadel");
        filterContext.Result = new UnAuthorizedResulst();
    }
}