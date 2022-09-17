namespace TestRunner.Library
{
	public interface ITestRunInfo
	{
		string Color { get; set; }
		string Result2Show { get; set; }
		bool Result2ShowExists { get; set; }
		TestState State { get; set; }
		string TestName { get; }
		TestType Type { get; }
        ITestRunMethodInfo TestRunMethodInfo { get; }

        event TestRunInfoBase.TestRunPropertyChanged? PropertyChanged;
	}
}