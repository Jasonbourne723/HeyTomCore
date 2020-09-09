using System.Collections.Generic;

namespace HeyMacchiato.Infra.Aop
{
    public class ConfigureOption
    {

        public ConfigureOption()
        {
        }

        public List<IInterceptor> Interceptors { get; } = new List<IInterceptor>();

        public void AddInterceptor<T>() where T : IInterceptor, new()
        {
            Interceptors.Add(new T());
        }
    }
}
