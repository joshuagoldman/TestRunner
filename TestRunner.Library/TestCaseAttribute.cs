using System;

namespace TestRunner.Library
{
	public class TestCaseAttribute : Attribute
	{
		public TestCaseAttribute(string testName)
		{
			TestName = testName;
		}

		public string TestName { get; }
	}
}
