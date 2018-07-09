using Livet;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using StudyWpf.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reactive.Linq;
using static StudyWpf.Models.Constants;

namespace StudyWpf.ViewModels
{
    public class Test3ViewModel : ViewModel
    {
        public int Id { get; set; }
        public int Test2Id { get; set; }
        [Required]
        public ReactiveProperty<string> Name { get; set; }
        public ReactiveProperty<bool> IsOk { get; set; }
        public ReactiveProperty<bool> SendEnabled { get; set; }
        public ResultStatus ResultStatus { get; set; }

        private Test3Entity Model;
        public Test3ViewModel()
        { }
        public Test3ViewModel(Test3Entity test3)
        {
            Model = test3;

            Id = test3.Id;
            Test2Id = test3.Test2Id;
            Name = test3
                .ToReactivePropertyAsSynchronized(x => x.Name, ignoreValidationErrorValue: true)
                .SetValidateAttribute(()=>this.Name)
                .AddTo(CompositeDisposable);

            SendEnabled = test3
                .ToReactivePropertyAsSynchronized(x => x.SendEnabled, ignoreValidationErrorValue: false)
                .AddTo(CompositeDisposable);
            IsOk = test3
                .ToReactivePropertyAsSynchronized(x => x.IsOk, ignoreValidationErrorValue: true)
                .AddTo(CompositeDisposable);

            IsOk.Subscribe(x=> { Console.WriteLine("IsOk @ VM : " + x);  });

        }
    }
}