using System;

namespace TestRunner.Library
{
	public class TestClassAttribute : Attribute
	{
		public TestClassAttribute(TestType testType)
		{
			TestType = testType;
		}
		public TestType TestType { get; }
	}
}
