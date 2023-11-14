using Microsoft.EntityFrameworkCore;

namespace Test_Manager_Alpha.Models
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<TestSuite> TestSuites { get; set; } = null!;
        public DbSet<TestCase> TestCases { get; set; } = null!;
		public DbSet<Step> Steps { get; set; } = null!;

		public ApplicationContext(DbContextOptions<ApplicationContext> options) 
            : base(options) 
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    string testProject1Name = "Genie2000";
        //    string testProject2Name = "Apex";

        //    TestSuite testSuite1 = new TestSuite() { Name = "Testing suite" };
        //    TestSuite testSuite2 = new TestSuite() { Name = "Testing suite" };

        //    TestSuites.AddRange(testSuite1, testSuite2);

        //    Project testProject1 = new Project() { Name = testProject1Name, Description = "Test description" };
        //    Project testProject2 = new Project() { Name = testProject2Name, Description = "Test description" };

        //    Projects.AddRange(testProject1, testProject2);

        //    SaveChanges();
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
