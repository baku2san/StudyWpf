using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public TestEntity _SelectedTest = new TestEntity();
        public TestEntity SelectedTest
        {
            get { return _SelectedTest; }
            set
            {
                if (_SelectedTest == value) return;
                _SelectedTest = value;
            }
        }
        public ObservableCollection<TestEntity> Tests;

        public Test2Entity _SelectedTest2 = new Test2Entity();
        public Test2Entity SelectedTest2
        {
            get { return _SelectedTest2; }
            set
            {
                if (_SelectedTest2 == value) return;
                _SelectedTest2 = value;
            }
        }
        public ObservableCollection<Test2Entity> Test2s;

        public LogicModel()
        {

            Tests = new ObservableCollection<TestEntity>(testRepository.Tests());
            Test2s = new ObservableCollection<Test2Entity>(testRepository.Test2s());

            
        }
    }
}
