using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using core_practice.Models;
using core_practice.Database;
using Microsoft.EntityFrameworkCore;

namespace core_practice.Controllers {
  public class HomeController : Controller {
    private readonly BaseContext _db;
    private readonly ILogger<HomeController> _logger;

    public HomeController(
      BaseContext context,
      ILogger<HomeController> logger) {
      _db = context;
      _logger = logger;
    }

    public async Task<IActionResult> Index() {
      return View(await _db.Users.ToListAsync());
    }

    public IActionResult Create() {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(User user) {
      _db.Users.Add(user);
      await _db.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    public IActionResult Privacy() {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
