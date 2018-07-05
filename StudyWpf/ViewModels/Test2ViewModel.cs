using Livet;
using StudyWpf.Models;
using static StudyWpf.Models.Constants;

namespace StudyWpf.ViewModels
{
    public class Test2ViewModel : ViewModel
    {
        public int Id { get; set; }
        public int TestModelId { get; set; }
        public string Name { get; set; }
        public ResultStatus ResultStatus { get; set; }
    }
}