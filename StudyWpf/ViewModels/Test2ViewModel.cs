using Livet;
using StudyWpf.Models;

namespace StudyWpf.ViewModels
{
    public class Test2ViewModel : ViewModel
    {
        public int Id { get; set; }
        public int TestModelId { get; set; }
        public string Name { get; set; }
        public Test2Entity.Test2Status ResultStatus { get; set; }
    }
}