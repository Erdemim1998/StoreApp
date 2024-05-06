using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Web.Controllers;

public class DashboardsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
