using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestRunner.Library
{
	public class TestRunMethodInfo : ITestRunMethodInfo
	{
		private readonly object classInstance;
		public string TestName { get; }
		public readonly MethodInfo MethodInfo;

		public TestRunMethodInfo(object classInstance, 
								 TestType testType,
								 string testName,
								 MethodInfo methodInfo)
		{
			this.classInstance = classInstance;

            TestType = testType;
			TestName = testName;
			MethodInfo = methodInfo;
		}

		public TestType TestType { get; }

		public async Task Invoke()
		{
            await (Task)MethodInfo.Invoke(classInstance, null);
		}
	}
}
