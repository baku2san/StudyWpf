﻿using Livet;
using Reactive.Bindings;
using StudyWpf.Models;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace StudyWpf.ViewModels
{
    public class MainWindowViewModel: ViewModel
    {
        private LogicModel model = new LogicModel();

        public ReactiveProperty<TestViewModel> SelectedTest { get; set; }
        public ReadOnlyReactiveCollection<TestViewModel> Tests { get; set; }

        public ReactiveProperty<Test2ViewModel> SelectedTest2 { get; set; }
        public ReadOnlyReactiveCollection<Test2ViewModel> Test2s { get; set; }

        public ReactiveProperty<Test3ViewModel> SelectedTest3 { get; set; }
        public ReadOnlyReactiveCollection<Test3ViewModel> Test3s { get; set; }

        public ReactiveCommand TestCommand { get; }
        public ReactiveCommand TestCommand2 { get; }
        public ReactiveCommand TestCommand3 { get; }
        public ReactiveProperty<bool> Command3Enabled { get; }

        public ReactiveCommand SearchCommand { get; }
        public ReactiveProperty<string> SearchWordTxt { get; }

        public MainWindowViewModel()
        {
            Console.WriteLine(nameof(MainWindowViewModel));

            SelectedTest = ReactiveProperty.FromObject(model, x => x.SelectedTest, convert: x => Mapper.Map<TestViewModel>(x), convertBack: x => Mapper.Map<TestEntity>(x))
                .AddTo(CompositeDisposable);
            SelectedTest2 = ReactiveProperty.FromObject(model, x => x.SelectedTest2, convert: x => Mapper.Map<Test2ViewModel>(x), convertBack: x => Mapper.Map<Test2Entity>(x))
                .AddTo(CompositeDisposable);

            SelectedTest3 = this.model
                .ToReactivePropertyAsSynchronized(x=> x.SelectedTest3, convert: x=> new Test3ViewModel(x), convertBack: x=>Mapper.Map<Test3Entity>(x), ignoreValidationErrorValue: true)
                .AddTo(CompositeDisposable);
            //SelectedTest3 = ReactiveProperty.FromObject(model, x => x.SelectedTest3, convert: x => Mapper.Map<Test3ViewModel>(x), convertBack: x => Mapper.Map<Test3Entity>(x))
            //    .AddTo(CompositeDisposable);

            Tests = model
                .Tests
                .ToReadOnlyReactiveCollection(to => Mapper.Map<TestViewModel>(to))
                .AddTo(CompositeDisposable);

            Test2s = model
                .Test2s
                .ToReadOnlyReactiveCollection(to => Mapper.Map<Test2ViewModel>(to))
                .AddTo(CompositeDisposable);

            Test3s = model
                .Test3s
                .ToReadOnlyReactiveCollection(to => new Test3ViewModel(to))
                .AddTo(CompositeDisposable);

            // IsOkの変化を、受け取れるようにしないと使いづらいね
            this.TestCommand = this.SelectedTest
                .CombineLatest(SelectedTest2, (test, test2) => !string.IsNullOrEmpty(test?.Name) && test2?.IsOk.Value == true)
                .ToReactiveCommand();
            this.TestCommand.Subscribe(async _ => UpdateData());

            this.model.Test3s.ObserveElementProperty(ox=>ox.IsOk)
                .Subscribe(_=> { RaisePropertyChanged(nameof(SelectedTest3)); Console.WriteLine("__ model _ " + this.SelectedTest3.Value?.IsOk.Value); });
            //this.Test3s.ObserveElementObservableProperty(x => x.IsOk).Subscribe(_ => { RaisePropertyChanged(nameof(Test3s)); Console.WriteLine("??" + this.SelectedTest3.Value?.IsOk.Value); });
            //this.Test3s.ObserveElementProperty(x=>x.IsOk).Subscribe(_ => { RaisePropertyChanged(nameof(Test3s)); Console.WriteLine("?2" + this.SelectedTest3.Value?.IsOk.Value); });

            this.TestCommand2 = this
                .ObserveProperty(s=>s.SelectedTest3)
                .Select(s=>s.Value?.IsOk?.Value == true)
                .ToReactiveCommand(false)
                .AddTo(CompositeDisposable);
            this.TestCommand2.Subscribe(async _ => UpdateData());

            this.SearchWordTxt = ReactiveProperty.FromObject(model, x => x.SearchWordTxt, convert: x => x, convertBack: x => x)
                .AddTo(CompositeDisposable);

            this.SearchCommand = new ReactiveCommand();
            this.SearchCommand.Subscribe(_ => Console.WriteLine(this.SearchWordTxt.Value + " is searching."));
        }
        public async void UpdateData()
        {
            // 非同期化は、Model側ですべき？
            Console.WriteLine(nameof(UpdateData));
        }
    }
}
