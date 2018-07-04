using Livet;
using StudyWpf.Models;

namespace StudyWpf.ViewModels
{
    public class TestViewModel: ViewModel
    {
        public int Id { get; private set; }
        public int TestModelId { get; private set; }
        public string Name { get; private set; }

        public TestEntity.ResultStatusDefinition ResultStatus { get; set; }

    }
}