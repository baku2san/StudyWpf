using Livet;
using StudyWpf.Models;
using static StudyWpf.Models.Constants;

namespace StudyWpf.ViewModels
{
    public class TestViewModel: ViewModel
    {
        public int Id { get; set; }
        public string Name { get;  set; }
        public ResultStatus ResultStatus { get; set; }

        public TestViewModel()
        {
            
        }

    }
}