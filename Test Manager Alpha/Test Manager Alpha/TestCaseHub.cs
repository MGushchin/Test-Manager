using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Test_Manager_Alpha.Models;

namespace Test_Manager_Alpha
{
    public class TestCaseHub: Hub
    {
		ApplicationContext db;

        public TestCaseHub(ApplicationContext context)
		{
			db = context;
		}

        public async Task GetTestCaseData(string testCaseId)
        {
			int intId = int.Parse(testCaseId);
			TestCase? testCase = await db.TestCases.Include(p => p.Steps).FirstOrDefaultAsync(p => p.Id == intId);
			await this.Clients.All.SendAsync("ReceivedTestCase", testCase);
		}

		public async Task GetStepData()
		{
			//int intId = int.Parse(id);
			int intId = int.Parse("2");
			TestCase? testCase = await db.TestCases.Include(p => p.Steps).FirstOrDefaultAsync(p => p.Id == intId);
			List<Step> steps = testCase.Steps;

			//List<Step> steps = new List<Step>();

			//foreach (var step in testCase.Steps)
			//	steps.Add(new Step { Id = step.Id, Action = step.Action, ExpectedResult = step.ExpectedResult });

			//steps.Add(new Step() { Id = 1, Action = "Action 1", ExpectedResult = "expected res 1"});
			//steps.Add(new Step() { Id = 2, Action = "Action 2", ExpectedResult = "expected res 2" });
			//steps.Add(new Step() { Id = 2, Action = "Action 3", ExpectedResult = "expected res 3" });

			await this.Clients.All.SendAsync("ReceivedSteps", testCase);
		}

		public async Task SendTestCase()
        {
            TestCase? testCase = await db.TestCases.FirstOrDefaultAsync(p => p.Id == 1);
            await this.Clients.All.SendAsync("ReceivedTestCase", testCase);
        }

		public async Task SaveSteps(string testCaseId, string[] actions, string[] results)
		{
			int id = int.Parse(testCaseId);
			TestCase? testCase = await db.TestCases.Include(p => p.Steps).FirstOrDefaultAsync(p => p.Id == id);

			if (testCase != null)
			{
				for(int i=0; i < testCase.Steps.Count; i++)
				{
					testCase.Steps[i].Action = actions[i];
					testCase.Steps[i].ExpectedResult = results[i];
					db.Steps.Update(testCase.Steps[i]);
				}

				db.TestCases.Update(testCase);

				await db.SaveChangesAsync();
			}

			await GetTestCaseData(testCaseId);
		}

		public async Task MoveStepUp(string id)
		{

		}

		public async Task MoveStepDown(string id)
		{

		}

		public async Task DeleteStep(string id)
		{

		}
	}
}
