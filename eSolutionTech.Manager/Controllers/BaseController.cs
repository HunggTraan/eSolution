using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using eSolutionTech.ViewModels.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eSolutionTech.Manager.Controllers
{
  public class BaseController : Controller
  {
    [Authorize]
    public override void OnActionExecuting(ActionExecutingContext context)
    {
      var sessions = context.HttpContext.Session.GetString("Token");

      var role = getRole();
      if (role == null)
      {
        RedirectToAction("Unauthorized", "Home");
      }
      ViewBag.Role = role;
      if (sessions == null)
      {
        context.Result = new RedirectToActionResult("Unauthorized", "Home", null);
      }
      base.OnActionExecuting(context);
    }

    public string getRole()
    {
      try
      {
        var claimsIdentity = (ClaimsIdentity)this.User.Identity;
        var claim = claimsIdentity.FindFirst(ClaimTypes.Role);
        if (claim == null) return string.Empty;
        string Role = claim.Value;
        return Role;
      }
      catch (Exception ex)
      {
        return string.Empty;
      }
    }
  }
}