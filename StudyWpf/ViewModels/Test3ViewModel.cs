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
        public ReactiveProperty<string> Name { get; set; }
        public ReactiveProperty<bool> IsOk { get; set; }
        public ReactiveProperty<bool> SendEnabled { get; set; }
        public ResultStatus ResultStatus { get; set; }
        public ReactiveProperty<bool> IsSelected {get; set;}

        public Test3Entity Model;
        public Test3ViewModel()
        { }
        public Test3ViewModel(Test3Entity test3)
        {
            Model = test3;
            
            if (test3 == null) { return; }
            Id = test3.Id;
            Test2Id = test3.Test2Id;
            Name = test3
                .ToReactivePropertyAsSynchronized(x => x.Name, ignoreValidationErrorValue: true)
                .AddTo(CompositeDisposable);

            SendEnabled = test3
                .ToReactivePropertyAsSynchronized(x => x.SendEnabled, ignoreValidationErrorValue: false)
                .AddTo(CompositeDisposable);
            IsOk = test3
                .ToReactivePropertyAsSynchronized(x => x.IsOk, ignoreValidationErrorValue: false)
                .AddTo(CompositeDisposable);

            IsSelected = test3
                .ToReactivePropertyAsSynchronized(o => o.IsSelected, convert: x => x, convertBack: x => x)
                .AddTo(CompositeDisposable);
        }
    }
}
// 1. IsSelectedの集約
// 1. 