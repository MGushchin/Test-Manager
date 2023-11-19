using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Manager_Alpha.Models;

namespace Test_Manager_Alpha.Controllers
{
	public class HomeController : Controller
	{
		ApplicationContext db;

		public HomeController(ApplicationContext context)
		{
			db = context;
		}

		public async Task<IActionResult> Index()
		{
			return View(await db.Projects.ToListAsync());
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Project project)
		{
			if (!ModelState.IsValid)
                return View(project);

            db.Projects.Add(project);
			await db.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		public IActionResult OpenProject(string? projectName)
		{
			return RedirectToAction("ShowInfo", "Project", new { projectName = projectName });
		}
	}
}
