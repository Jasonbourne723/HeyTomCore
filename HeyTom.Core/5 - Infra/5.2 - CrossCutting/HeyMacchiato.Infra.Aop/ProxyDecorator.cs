using System.Collections.Generic;
using System.Reflection;

namespace HeyMacchiato.Infra.Aop
{
    /// <summary>
    /// 动态代理装饰类
    /// </summary>
    public class ProxyDecorator : DispatchProxy
    {

        private object _target { get; set; }

        private List<IInterceptor> _interceptors { get; set; } = new List<IInterceptor>();

        public object Create<T>(object impl)
        {
            object proxy = Create<T, ProxyDecorator>();
            ((ProxyDecorator)proxy)._target = impl;

            return proxy;
        }

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            _interceptors?.ForEach(ea =>
            {
                ea.Before(_target,_target.GetType().GetMethod(targetMethod.Name),args);
            });
            var result = targetMethod.Invoke(_target, args);
            _interceptors?.Reverse();
            _interceptors?.ForEach(ea =>
             {
                 ea.After(_target, _target.GetType().GetMethod(targetMethod.Name), args);
             });
            return result;
        }

        public void SetConfigureOption(ConfigureOption option)
        {
            if (option == null)
            {
                return;
            }
            _interceptors = option.Interceptors;
        }
    }
}
