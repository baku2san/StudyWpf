using AutoMapper;
using Livet;
using Reactive.Bindings;
using StudyWpf.Models;
using StudyWpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StudyWpf
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Mapper.Initialize(cfg => { 
                //cfg.AddProfiles(Assembly.GetExecutingAssembly());
                cfg.CreateMap<TestEntity, TestViewModel>()
                    .ForMember(dest=> dest.CompositeDisposable, opt=>opt.Ignore())
                    .ForMember(dest=> dest.Messenger, opt=>opt.Ignore())
                    .ReverseMap()
                    .ForMember(dest=> dest.SheetId, opt=>opt.Ignore())
                    ;
                cfg.CreateMap<Test2Entity, Test2ViewModel>()
                    .ForPath(dest => dest.Name.Value, source => source.MapFrom(s=>s.Name))
                    .ForPath(dest => dest.SendEnabled.Value, source => source.MapFrom(s=>s.SendEnabled))
                    .ForMember(dest => dest.CompositeDisposable, opt => opt.Ignore())
                    .ForMember(dest => dest.Messenger, opt => opt.Ignore())
                    .ReverseMap()
                    .ForPath(dest => dest.Name, source => source.MapFrom(s=>s.Name.Value))
                    .ForPath(dest => dest.SendEnabled, source => source.MapFrom(s=>s.SendEnabled.Value))
                    ;
            });
            Mapper.AssertConfigurationIsValid();

            DispatcherHelper.UIDispatcher = Dispatcher;
            UIDispatcherScheduler.Initialize();

            InitializeComponent();

        }
    }
}
