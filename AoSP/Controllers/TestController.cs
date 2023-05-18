using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AoSP.Controllers;

public class TestController : Controller
{
    private readonly ApplicationContext _context;

    public TestController(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Users.ToListAsync());
    }
}