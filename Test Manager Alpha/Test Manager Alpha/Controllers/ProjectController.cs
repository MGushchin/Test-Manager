using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Manager_Alpha.Models;
using Test_Manager_Alpha.ViewModels;

namespace Test_Manager_Alpha.Controllers
{
    public class ProjectController : Controller
    {
        ApplicationContext db;

        public ProjectController(ApplicationContext context)
        {
            db = context;
        }

        public async Task<IActionResult> ShowInfo(string projectName)
        {
            Project? project = await db.Projects.Include(p => p.TestSuites).ThenInclude(p => p.TestCases).FirstOrDefaultAsync(p => p.Name == projectName);
            return View(project);
        }

        public async Task<IActionResult> AddTestSuite(string projectName)
        {
            Project? project = await db.Projects.FirstOrDefaultAsync(p => p.Name == projectName);
            TestSuiteWithProject model = new TestSuiteWithProject() { ParentProject = project };
            return View(model);
        }

        public async Task<IActionResult> AddTestCase(int testSuiteId)
        {
            TestSuite? suite = await db.TestSuites.Include(p => p.Project).FirstOrDefaultAsync(p => p.Id == testSuiteId);
            return View(suite);
        }

        [HttpPost]
        public async Task<IActionResult> AddTestSuite(TestSuiteWithProject model, int projectId)
        {
            Project? project = await db.Projects.FirstOrDefaultAsync(p => p.Id == projectId);

            if (!ModelState.IsValid)
            {
                model.ParentProject = project;
                return View(model);
            }

            model.Suite.Project = project;

            db.TestSuites.Add(model.Suite);

            project.TestSuites.Add(model.Suite);

            await db.SaveChangesAsync();

            return RedirectToAction("ShowInfo", "Project", new { projectName = project.Name });
        }

        [HttpPost]
        public async Task<IActionResult> CreateTestCase(string suiteName, string testCaseName, string testCaseDescription)
        {
            TestSuite? suite = await db.TestSuites.Include(p => p.Project).FirstOrDefaultAsync(p => p.Name == suiteName);
            TestCase testCase = new TestCase() { Name = testCaseName, Description = testCaseDescription, Suite = suite };

            db.TestCases.Add(testCase);

            await db.SaveChangesAsync();

            return RedirectToAction("ShowInfo", "Project", new { projectName = suite.Project.Name });
        }

    }
}
