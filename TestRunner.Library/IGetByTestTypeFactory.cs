namespace TestRunner.Library
{
	public interface IGetByTestTypeFactory<T>
	{
		T GetByTestType(TestType testType, string testname);
	}
}