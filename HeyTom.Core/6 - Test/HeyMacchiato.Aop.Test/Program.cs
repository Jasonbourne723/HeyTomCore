using HeyMacchiato.Infra.Aop;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HeyMacchiato.Aop.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddScopeByProxy<ITestService, TestService>(option =>
            {
                option.AddInterceptor<DefaultInterceptor>();
            });
            var serviceProvider = services.BuildServiceProvider();

            var _testService = serviceProvider.GetService<ITestService>();
             _testService.Execute();

            Console.ReadKey();
        }
    }


    public interface ITestService
    {
        string Execute();
    }

    [Log]
    [Log1]
    public class TestService : ITestService
    {
        [Log]
        public string Execute()
        {
            Console.WriteLine("OK");
            return "OK";
        }
    }
}
