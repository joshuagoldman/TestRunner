using System;

namespace TestRunner.Library
{
	public class GetByTestTypeFactory<T> : IGetByTestTypeFactory<T>
	{
		private readonly Func<TestType,string,T> _func;

		public GetByTestTypeFactory(Func<TestType, string,T> func)
		{
			_func = func;
		}

		public T GetByTestType(TestType testType, string testName)
		{
			return _func(testType, testName);
		}
	}
}
