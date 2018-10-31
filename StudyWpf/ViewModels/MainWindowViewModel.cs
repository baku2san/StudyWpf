using Livet;
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
using System.ComponentModel;

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

        public ReactiveCommand ActionCommand { get; }

        public ReactiveCommand SearchCommand { get; }
        public ReactiveProperty<string> SearchWordTxt { get; }

        public ReadOnlyReactiveCollection<bool> IsOk2s { get; set; }
        public ReactiveProperty<int> IsOk3Count { get; set; }
        public ReactiveProperty<SenderEventArgsPair<Test3Entity, PropertyChangedEventArgs>> WatchIsOk3 { get; }

        public MainWindowViewModel()
        {
            Console.WriteLine(nameof(MainWindowViewModel));

            SelectedTest = ReactiveProperty.FromObject(model, x => x.SelectedTest, convert: x => Mapper.Map<TestViewModel>(x), convertBack: x => Mapper.Map<TestEntity>(x))
                .AddTo(CompositeDisposable);
            SelectedTest2 = ReactiveProperty.FromObject(model, x => x.SelectedTest2, convert: x => Mapper.Map<Test2ViewModel>(x), convertBack: x => Mapper.Map<Test2Entity>(x))
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
            model.Test3s.ObserveAddChanged()
                .Delay(TimeSpan.FromMilliseconds(500))
                .Subscribe(_ =>
            {

                RaisePropertyChanged(nameof(SelectedTest3));
            });
            IsOk3Count = new ReactiveProperty<int>(0);
            this.model.Test3s
                .ObserveElementPropertyChanged()
                .Where(w => w.EventArgs.PropertyName == nameof(Test3Entity.IsOk))
                .Subscribe(_ => IsOk3Count.Value = Test3s.Where(w => w.IsOk.Value).Count());

            SelectedTest3 = this.model
                .ToReactivePropertyAsSynchronized(x => x.SelectedTest3, convert: x => {
                    Console.WriteLine(x?.Id + " / " + Test3s.Aggregate("", (z,y)=> z + y.Id));
                    return Test3s.FirstOrDefault(f => f.Id == x?.Id);
                    }, 
                    convertBack: x => x?.Model, ignoreValidationErrorValue: false )
                .AddTo(CompositeDisposable);
            //SelectedTest3
            //    .Delay(TimeSpan.FromMilliseconds(500))
            //    .Subscribe(_ => Console.WriteLine(_?.Id + " as selected@View " + Test3s?.Count));


            // IsOkの変化を、受け取れるようにしないと使いづらいね
            this.TestCommand = this.SelectedTest
                .CombineLatest(SelectedTest2, (test, test2) => !string.IsNullOrEmpty(test?.Name) && test2?.IsOk.Value == true)
                .ToReactiveCommand();
            this.TestCommand.Subscribe(async _ => UpdateData());

            //this.model.Test3s.ObserveResetChanged()
            //    .Subscribe(_=>
            //    {
            //        Console.WriteLine( this.SelectedTest3.Value?.Id + " as Id@View");
            //    });
            //this.Test3s.ObserveElementObservableProperty(x => x.IsOk).Subscribe(_ => { RaisePropertyChanged(nameof(Test3s)); Console.WriteLine("??" + this.SelectedTest3.Value?.IsOk.Value); });
            //this.Test3s.ObserveElementProperty(x=>x.IsOk).Subscribe(_ => { RaisePropertyChanged(nameof(Test3s)); Console.WriteLine("?2" + this.SelectedTest3.Value?.IsOk.Value); });

            this.TestCommand2 = this
                .ObserveProperty(s=>s.SelectedTest3)
                .Select(s=>s.Value?.IsOk?.Value == true)
                .ToReactiveCommand(false)
                .AddTo(CompositeDisposable);
            this.TestCommand2.Subscribe(async _ => UpdateData());

            this.ActionCommand = new ReactiveCommand().AddTo(CompositeDisposable);
            this.ActionCommand
                .Subscribe(_ => Console.WriteLine($"{_} __"));

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
