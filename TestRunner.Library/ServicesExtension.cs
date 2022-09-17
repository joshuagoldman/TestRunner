using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TestRunner.Library
{
	public static class ServicesExtension
	{
        public static void AddTestCaseClass<T>(this IServiceCollection services)
			where T : ITestRunInfo
        {
			var testType = GetTestTypeByAttributeName<T>();

			var methods = typeof(T).GetMethods();

			foreach (var methodInfo in methods)
			{
				var testMethodAttribute = methodInfo.GetCustomAttribute<TestCaseAttribute>();

				if(testMethodAttribute != null)
				{
					string testName = testMethodAttribute.TestName;

                    var instanceParams = new object[]
                        {
                            testType,
                            testName,
                            methodInfo
                        };
                    var instance = Activator.CreateInstance(typeof(T), instanceParams);

                    services.AddSingleton<ITestRunInfo>((T)instance);
                }
			}



            
        }
        public static void AddGetTestCaseFactory(this IServiceCollection services)
		{
			services.AddSingleton<Func<TestType,string,ITestRunInfo>>(serviceProvider => (testTypeComp,testNameComp) =>
			{
				var relevantInstances = serviceProvider.GetServices<ITestRunInfo>();
				if (relevantInstances != null)
				{
					var relevantTest = relevantInstances.FirstOrDefault(testClass => GetTestTypeByAttributeName(testClass) == testTypeComp && 
                                                                                     testClass.TestName == testNameComp);
					if (relevantTest != null)
						return relevantTest;
					else
                        throw new Exception($"Nämen hörruduru, det finns inte ens några tjänster med typ {nameof(ITestRunInfo)} där testtyp är {testTypeComp}. Skärp dig för fan!");

                }
				else
					throw new Exception($"Nämen hörruduru, det finns inte ens några tjänster med typ {nameof(ITestRunInfo)}. Skärp dig för fan!");
			});

            services.AddSingleton<Func<TestType, string, ITestRunMethodInfo>>(serviceProvider => (testTypeComp, testNameComp) =>
            {
                var relevantInstances = serviceProvider.GetServices<ITestRunMethodInfo>();
                if (relevantInstances != null)
                {
                    var relevantTest = relevantInstances.FirstOrDefault(testClass => GetTestTypeByAttributeName(testClass) == testTypeComp &&
                                                                                     testClass.TestName == testNameComp);
                    if (relevantTest != null)
                        return relevantTest;
                    else
                        throw new Exception($"Nämen hörruduru, det finns inte ens några tjänster med typ {nameof(ITestRunMethodInfo)} där testtyp är {testTypeComp}. Skärp dig för fan!");

                }
                else
                    throw new Exception($"Nämen hörruduru, det finns inte ens några tjänster med typ {nameof(ITestRunMethodInfo)}. Skärp dig för fan!");
            });

            services.AddSingleton<IGetByTestTypeFactory<ITestRunMethodInfo>, GetByTestTypeFactory<ITestRunMethodInfo>>();
            services.AddSingleton<IGetByTestTypeFactory<ITestRunInfo>, GetByTestTypeFactory<ITestRunInfo>>();
		}

        public static void AddGetTestCasesFactory(this IServiceCollection services)
        {
            services.AddSingleton<Func<TestType, List<ITestRunInfo>>>(serviceProvider => testTypeComp =>
            {
                var relevantInstances = serviceProvider.GetServices<ITestRunInfo>();
                if (relevantInstances != null)
                {
                    return relevantInstances.Where(tRuninfo => tRuninfo.Type == testTypeComp).ToList();
                }
                else
                    throw new Exception($"Nämen hörruduru, det finns inte ens några tjänster med typ {nameof(ITestRunInfo)}. Skärp dig för fan!");
            });

            services.AddSingleton<IGetSeveralByTestTypeFactory<ITestRunInfo>, GetSeveralByTestTypeFactory<ITestRunInfo>>();
        }

		private static TestType GetTestTypeByAttributeName<T>()
		{
			var testClassAttr = typeof(T).GetConstructors().First().GetCustomAttribute<TestClassAttribute>();
			if (testClassAttr != null)
			{
				return testClassAttr.TestType;
            }
			else
				throw new Exception($"Du måste lägga till TestClass attributet för klass {typeof(T).Name}!");
		}

        private static TestType GetTestTypeByAttributeName<T>(T classInstance)
        {
            var testClassAttr = classInstance!.GetType().GetCustomAttribute<TestClassAttribute>();
            if (testClassAttr != null)
            {
                return testClassAttr.TestType;
            }
            else
                throw new Exception($"Du måste lägga till TestClass attributet för klass {classInstance.GetType().Name}!");
        }
    }
}
