using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Manager_Alpha.Models;


namespace Test_Manager_Alpha.Controllers
{
	public class TestCaseController : Controller
	{
		ApplicationContext db;

		public TestCaseController(ApplicationContext context)
		{
			db = context;
		}

		public async Task<IActionResult> Index(int testCaseId)
		{
			TestCase? testCase = await db.TestCases.Include(p => p.Steps).Include(p => p.Suite).ThenInclude(p => p.Project).FirstOrDefaultAsync(p => p.Id == testCaseId);
			return View(testCase);
		}

		public async Task<IActionResult> Edit(int id)
		{
			if (id != null)
			{
				TestCase? testCase = await db.TestCases.Include(p => p.Steps).Include(p => p.Suite).ThenInclude(p => p.Project).FirstOrDefaultAsync(p => p.Id == id);
				if (testCase != null)
					return View(testCase);
			}
			return NotFound();
		}


		[HttpPost]
		public async Task<IActionResult> Edit(int id, string name, string description, List<Step> steps)
		{
			TestCase? testCase = await db.TestCases.Include(p => p.Steps).FirstOrDefaultAsync(p => p.Id == id);

			if (testCase != null)
			{
				testCase.Name = name;
				testCase.Description = description;

				int counter = 0;
				foreach (Step? step in steps) 
				{
					testCase.Steps[counter].Action = step.Action;
					testCase.Steps[counter].ExpectedResult = step.ExpectedResult;
					db.Steps.Update(testCase.Steps[counter]);
					counter++;
				}



				db.TestCases.Update(testCase);

				await db.SaveChangesAsync();

				return RedirectToAction("Index", new { testCaseId = id });
			}

			return NotFound();
		}

		public async Task<IActionResult> AddStep(int testCaseId)
		{
			TestCase? testCase = await db.TestCases.Include(p => p.Suite).ThenInclude(p => p.Project).FirstOrDefaultAsync(p => p.Id == testCaseId);

			Step? newStep = new Step() { Action = " ", ExpectedResult = " " };

			testCase.Steps.Add(newStep);
			db.Steps.Add(newStep);

			await db.SaveChangesAsync();

			return RedirectToAction("Edit", testCase);
		}

		public IActionResult DeleteStep(int testCaseId, int stepId)
		{
			Step? step = db.Steps.FirstOrDefault(p => p.Id == stepId);

			if (step != null)
			{
				db.Steps.Remove(step);
				db.SaveChanges();
			}

			return RedirectToAction("Edit", new { id = testCaseId });
		}

		//public async void Refresh(int id)
		//{
		//	TestCase? testCase = await db.TestCases.Include(p => p.Steps).Include(p => p.Suite).ThenInclude(p => p.Project).FirstOrDefaultAsync(p => p.Id == id);
		//	//await this.hubContext.Clients.
		//}
	}
}
