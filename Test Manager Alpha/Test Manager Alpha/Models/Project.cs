namespace Test_Manager_Alpha.Models
{
    public class Project
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<TestSuite>? TestSuites { get; set; } = new List<TestSuite>();
    }
}
