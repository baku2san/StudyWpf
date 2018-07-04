using Livet;
using Reactive.Bindings;
using StudyWpf.Models;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace StudyWpf.ViewModels
{
    public class MainWindowViewModel: ViewModel
    {
        private LogicModel model = new LogicModel();

        public ReactiveProperty<TestViewModel> SelectedTest { get; set; }
        public ReactiveProperty<Test2ViewModel> SelectedTest2 { get; set; }

        public ReadOnlyReactiveCollection<TestViewModel> Tests { get; set; }
        public ReadOnlyReactiveCollection<Test2ViewModel> Test2s { get; set; }
        public MainWindowViewModel()
        {
            Console.WriteLine(nameof(MainWindowViewModel));

            SelectedTest = ReactiveProperty.FromObject(model, x => x.SelectedTest, convert: x => Mapper.Map<TestViewModel>(x), convertBack: x => Mapper.Map<TestEntity>(x))
                .AddTo(CompositeDisposable);
            SelectedTest2 = new ReactiveProperty<Test2ViewModel>();

            Tests = model
                .Tests
                .ToReadOnlyReactiveCollection(to=> Mapper.Map<TestViewModel>(to))
                .AddTo(CompositeDisposable);

            Test2s = model
                .Test2s
                .ToReadOnlyReactiveCollection(to => Mapper.Map<Test2ViewModel>(to))
                .AddTo(CompositeDisposable);
        }
    }
}
