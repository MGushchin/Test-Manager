using System.ComponentModel.DataAnnotations;

namespace Test_Manager_Alpha.Models
{
    public class Project
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "* Name not specified")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<TestSuite>? TestSuites { get; set; } = new List<TestSuite>();
    }
}
