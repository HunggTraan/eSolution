using eSolutionTech.ApiIntegration;
using eSolutionTech.Manager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using eSolutionTech.ViewModels.Common;

namespace eSolutionTech.Manager.Controllers
{
  public class HomeController : BaseController
  {
    private readonly ILogger<HomeController> _logger;
    private readonly IUserApiClient _userApiClient;

    public HomeController(ILogger<HomeController> logger)
    {
      _logger = logger;
    }

    public IActionResult Index()
    {
      return View();
    }

    public IActionResult Privacy()
    {
      return View();
    }

    public IActionResult Unauthorized()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
