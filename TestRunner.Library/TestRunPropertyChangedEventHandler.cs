using System.Reflection;

namespace TestRunner.Library
{
	public readonly struct TestRunPropertyChangedEventArgs
	{
		public TestRunPropertyChangedEventArgs(TestType testType, PropertyInfo propInfo, object propValue)
		{
			TestType = testType;
			PropInfo = propInfo;
			PropValue = propValue;
		}

		public TestType TestType { get; }
		public PropertyInfo PropInfo { get; }
		public object PropValue { get; }
	}
}
