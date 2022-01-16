using Microsoft.AspNetCore.Mvc;
namespace ShipVista.Filters;
internal class UnAuthorizedResulst : ActionResult
{
    public override void ExecuteResult(ActionContext context)
    {
        // Set the response code to 403.
        context.HttpContext.Response.StatusCode = 403;
    }
}