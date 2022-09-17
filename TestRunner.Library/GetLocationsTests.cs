
using System.Reflection;
using System.Threading.Tasks;

namespace TestRunner.Library
{
	public class GetLocationsTests : TestRunInfoBase, ITestRunInfo
    {
		[TestClass(TestType.Basic)]
		public GetLocationsTests(TestType testType,
								 string TestName,
								 MethodInfo methodInfo) : base(testType, 
															   TestName,
                                                               methodInfo) { }

		[TestCase("First Test")]
		public async Task GetLocation()
		{
			await RunTest();
		}

		[TestCase("Second Test")]
		public async Task GetLocations()
		{
			await RunTest();
		}
	}
}
