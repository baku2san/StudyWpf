using Livet;
using Reactive.Bindings;
using StudyWpf.Models;
using System.ComponentModel.DataAnnotations;
using static StudyWpf.Models.Constants;

namespace StudyWpf.ViewModels
{
    public class Test2ViewModel : ViewModel
    {
        public int Id { get; set; }
        public int TestModelId { get; set; }
        [Required]
        public ReactiveProperty<string> Name { get; set; }
        public ReactiveProperty<bool> IsOk { get; set; }
        public ReactiveProperty<bool> SendEnabled { get; set; }
        public ResultStatus ResultStatus { get; set; }
    }
}