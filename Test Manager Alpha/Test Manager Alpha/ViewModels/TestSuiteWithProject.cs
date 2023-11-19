using System.ComponentModel.DataAnnotations;
using Test_Manager_Alpha.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Test_Manager_Alpha.ViewModels
{
    public class TestSuiteWithProject
    {
        public Project? ParentProject { get; set; }
        public TestSuite? Suite { get; set; }
    }


}
