using System.Collections.Generic;

namespace TestRunner.Library
{
	public interface IGetSeveralByTestTypeFactory<T>
	{
		List<T> GetByTestType(TestType testType);
	}
}