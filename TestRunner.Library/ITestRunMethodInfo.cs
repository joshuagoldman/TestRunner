using System.Threading.Tasks;

namespace TestRunner.Library
{
	public interface ITestRunMethodInfo
	{
		Task Invoke();
        string TestName { get; }
        TestType TestType { get; }
    }
}