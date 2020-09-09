using System;
using System.Linq;
using System.Reflection;

namespace HeyMacchiato.Infra.Aop
{
    /// <summary>
    /// 默认拦截器
    /// </summary>
    public class DefaultInterceptor : IInterceptor
    {

        public void Before(object target, MethodInfo method, object[] args)
        {
            var methodAttributes = method.GetCustomAttributes<AopAbstractAttribute>()?.ToList();
            var classAttributes = target.GetType().GetCustomAttributes<AopAbstractAttribute>()?.ToList();

            methodAttributes?.ForEach(ea =>
            {
                ea.Before(target, method, args);
            });
            classAttributes?.FindAll(x => !(methodAttributes?.Exists(y => x.GetType() == y.GetType())??false))?.ForEach(ea =>
            {
                ea.Before(target, method, args);
            });
            Console.WriteLine($"进入 类：{target.GetType().Name} 方法：{method.Name}");
        }

        public void After(object target, MethodInfo method, object[] args)
        {

            var methodAttributes = method.GetCustomAttributes<AopAbstractAttribute>()?.ToList();
            var classAttributes = target.GetType().GetCustomAttributes<AopAbstractAttribute>()?.ToList();

            methodAttributes?.ForEach(ea =>
            {
                ea.After(target, method, args);
            });
            classAttributes?.FindAll(x => !(methodAttributes?.Exists(y => x.GetType() == y.GetType()) ?? false))?.ForEach(ea =>
            {
                ea.After(target, method, args);
            });
            Console.WriteLine($"完成 类：{target.GetType().Name} 方法：{method.Name}");
        }
    }
}
