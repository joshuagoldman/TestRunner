using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace TestRunner.Library
{
    public abstract class TestRunInfoBase
    {
        public delegate void TestRunPropertyChanged(TestRunPropertyChangedEventArgs testRunPropertyChangedEventHandler);
        public event TestRunPropertyChanged? PropertyChanged;

        public TestRunInfoBase(TestType testType, 
                               string testName,
                               MethodInfo methodInfo)
        {
            ClassPropsMetaData = this.GetType().GetProperties().ToList();
            TestName = testName;
            TestRunMethodInfo = new TestRunMethodInfo(
                this,
                testType,
                testName,
                methodInfo
            );
            Type = testType;
        }

        protected virtual async Task RunTest()
        {
            State = TestState.Running;
            Color = "yellow";
            await Task.Delay(3000);
            State = TestState.Passed;

            Result2Show = $"Test {TestName} passed!";
            Result2ShowExists = true;
            Color = "green";
        }

        private List<PropertyInfo> ClassPropsMetaData { get; }

        public virtual string TestName { get; }
        public ITestRunMethodInfo TestRunMethodInfo { get; }

        private string _color = "brown";
        public virtual string Color
        {
            get => _color;
            set
            {
                if (value != _color)
                {
                    _color = value;
                    OnPropertyChanged(nameof(Color));
                }
            }
        }

        private TestState _state = Library.TestState.NotStarted;
        public virtual TestState State
        {
            get => _state;
            set
            {
                if (value != _state)
                {
                    _state = value;
                    OnPropertyChanged(nameof(State));
                }
            }
        }

        private string _result2Show = "";
        public virtual string Result2Show
        {
            get => _result2Show;
            set
            {
                if (value != _result2Show)
                {
                    _result2Show = value;
                    OnPropertyChanged(nameof(Result2Show));
                }
            }
        }

        private bool _result2ShowExists = false;
        public virtual bool Result2ShowExists
        {
            get => _result2ShowExists;
            set
            {
                if (value != _result2ShowExists)
                {
                    _result2ShowExists = value;
                    OnPropertyChanged(nameof(Result2ShowExists));
                }
            }
        }

        public virtual TestType Type { get; }

        private void OnPropertyChanged(string propName)
        {
            var foundProp = ClassPropsMetaData.FirstOrDefault(prop => prop.Name == propName);
            if (foundProp != null)
            {
                var evArgs = new TestRunPropertyChangedEventArgs(
                    Type,
                    foundProp,
                    foundProp.GetValue(this, null)
                );

                PropertyChanged?.Invoke(evArgs);
            }
            else
                throw new Exception($"Could not find property {propName} in class {this.GetType().Name}");
        }

    }
}
