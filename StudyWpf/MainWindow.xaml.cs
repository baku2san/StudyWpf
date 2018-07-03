using AutoMapper;
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
                cfg.AddProfiles(Assembly.GetExecutingAssembly());
                cfg.CreateMap<TestEntity, TestViewModel>();
                cfg.CreateMap<Test2Entity, Test2ViewModel>();
            });

            InitializeComponent();

            var os = new TestContext();
            Console.WriteLine(os.Items.Count());
        }
    }
}
