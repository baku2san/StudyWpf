using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoMapper;
using Livet;
using StudyWpf.ViewModels;

namespace StudyWpf.Models
{
    public class LogicModel: NotificationObject
    {
        private TestRepository testRepository = new TestRepository();

        public TestEntity _SelectedTest ;
        public TestEntity SelectedTest
        {
            get { return _SelectedTest; }
            set
            {
                if (_SelectedTest == value) return;
                _SelectedTest = value;
                RaisePropertyChanged(nameof(SelectedTest));
            }
        }
        public ObservableCollection<TestEntity> Tests;

        public Test2Entity _SelectedTest2;
        public Test2Entity SelectedTest2
        {
            get { return _SelectedTest2; }
            set
            {
                if (_SelectedTest2 == value) return;
                _SelectedTest2 = value;
                RaisePropertyChanged(nameof(SelectedTest2));
            }
        }
        public ObservableCollection<Test2Entity> Test2s;

        public Test3Entity _SelectedTest3;
        public Test3Entity SelectedTest3
        {
            get { return _SelectedTest3; }
            set
            {
                if (_SelectedTest3 == value) return;
                _SelectedTest3 = value;
                RaisePropertyChanged(nameof(SelectedTest3));
            }
        }
        public ObservableCollection<Test3Entity> Test3s;

        public LogicModel()
        {

            Tests = new ObservableCollection<TestEntity>(testRepository.Tests());
            Test2s = new ObservableCollection<Test2Entity>();
            Test3s = new ObservableCollection<Test3Entity>();

            PropertyChanged += Test1Selected;
            PropertyChanged += Test2Selected;
            PropertyChanged += Test3Selected;

        }

        private void Test1Selected(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(SelectedTest) || !(sender is LogicModel)) { return; }
            Test2s.Clear();
            foreach (var test2 in testRepository.Test2s(_SelectedTest))
            {
                Test2s.Add(test2);
            }
            SelectedTest2 = null;
        }
        private void Test2Selected(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(SelectedTest2) || !(sender is LogicModel)) { return; }
            Test3s.Clear();
            if (SelectedTest2 != null)
            {
                foreach (var test3 in testRepository.Test3s(SelectedTest2))
                {
                    Test3s.Add(test3);
                }
            }
            SelectedTest3 = null;
            Console.WriteLine(SelectedTest2?.IsOk + " / " + SelectedTest2?.SendEnabled);
            Console.WriteLine(Test2s.Select(s => s?.IsOk + " / " + s?.SendEnabled).Aggregate((a, b) => a + ", " + b));
        }
        private void Test3Selected(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(SelectedTest3) || !(sender is LogicModel)) { return; }
            Console.WriteLine("3: " + SelectedTest3?.IsOk + " / " + SelectedTest3?.SendEnabled);
            Console.WriteLine("3: " + Test3s.Select(s => s?.IsOk + " / " + s?.SendEnabled).Aggregate((a, b) => a + ", " + b));
        }
    }
}
