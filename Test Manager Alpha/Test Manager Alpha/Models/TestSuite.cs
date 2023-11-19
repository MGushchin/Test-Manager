using System.ComponentModel.DataAnnotations;

namespace Test_Manager_Alpha.Models
{
    public class TestSuite
    {
        public int? Id { get; set; }
        [Required]
        public string? Name { get; set; }

        public Project? Project { get; set; }
        public List<TestCase> TestCases { get; set; } = new();


    }
}
