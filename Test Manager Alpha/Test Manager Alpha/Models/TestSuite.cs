namespace Test_Manager_Alpha.Models
{
    public class TestSuite
    {
        public int? Id { get; set; }
        public string? Name { get; set; }

        public Project? Project { get; set; }
        public List<TestCase> TestCases { get; set; } = new();


    }
}
