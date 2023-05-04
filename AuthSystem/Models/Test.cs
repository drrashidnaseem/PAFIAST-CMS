using AuthSystem.Models;
using DCMS.Lib.Data.Abstractions.Attributes;
namespace AuthSystem.Models {



    public class Test
    {
        [Key]
        public int TestId { get; set; }
        public string TestName { get; set; }
        public List<Test> TestList { get; set; }
        public List<Subject> Subjects { get; set; }
    }

  
}
