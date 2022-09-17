using System;
using System.Collections.Generic;

namespace TestRunner.Library
{
	public class GetSeveralByTestTypeFactory<T> : IGetSeveralByTestTypeFactory<T>
	{
		private readonly Func<TestType, List<T>> _func;

		public GetSeveralByTestTypeFactory(Func<TestType, List<T>> func)
		{
			_func = func;
		}

		public List<T> GetByTestType(TestType testType)
		{
			return _func(testType);
		}
	}
}
