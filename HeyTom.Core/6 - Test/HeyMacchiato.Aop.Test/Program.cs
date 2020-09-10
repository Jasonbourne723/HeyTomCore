using HeyMacchiato.Infra.Aop;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text.Json.Serialization;

namespace HeyMacchiato.Aop.Test
{
    class Program
    {
        static void Main(string[] args)
        {

            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appSettings.json"), false, true);
            var configuration = configurationBuilder.Build();


            var services = new ServiceCollection();

            services.Configure<TestOption>(configuration);
            //AOP
            services.AddScopeByProxy<ITestService, TestService>(option =>
            {
                option.AddInterceptor<DefaultInterceptor>();
            });
            var serviceProvider = services.BuildServiceProvider();

            var _testService = serviceProvider.GetService<ITestService>();
            _testService.Execute();

            //单例配置选项 除非代码更改配置 更改配置文件 不会变化
            var option = serviceProvider.GetService<IOptions<TestOption>>();
            //代码更改及 修改配置文件 都会触发变化
            var optionMonitor = serviceProvider.GetService<IOptionsMonitor<TestOption>>();
            // 每次新请求进入时 会重新获取配置
            var optionSnapshot = serviceProvider.GetService<IOptionsSnapshot<TestOption>>();
            Console.WriteLine($"{option.Value.Name} {option.Value.Age}");
            Console.WriteLine($"{optionMonitor.CurrentValue.Name} {optionMonitor.CurrentValue.Age}");
            Console.WriteLine($"{optionSnapshot.Value.Name} {optionSnapshot.Value.Age}");

            Console.ReadKey();

            Console.WriteLine($"{option.Value.Name} {option.Value.Age}");
            Console.WriteLine($"{optionMonitor.CurrentValue.Name} {optionMonitor.CurrentValue.Age}");
            Console.WriteLine($"{optionSnapshot.Value.Name} {optionSnapshot.Value.Age}");

            IFileProvider fileProvider = new PhysicalFileProvider(@"F:\文档");
            var file = fileProvider.GetFileInfo("aa.json");
            string filestr = "";
            using (var stream = file.CreateReadStream())
            {
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);
                filestr = System.Text.Encoding.UTF8.GetString(bytes);
            }
            var object1 = JsonConvert.DeserializeObject<JObject>(filestr);
            var properties = object1.Properties();
            var nmae =  object1.Value<string>("name");
            Console.WriteLine(filestr);
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

    public class TestOption
    {
        public string Name { get; set; }

        public string Age { get; set; }
    }
}
