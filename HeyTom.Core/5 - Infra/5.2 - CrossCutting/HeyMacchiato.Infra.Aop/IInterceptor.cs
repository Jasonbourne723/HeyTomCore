using System;
using System.Reflection;

namespace HeyMacchiato.Infra.Aop
{
    /// <summary>
    /// 拦截器接口 
    /// </summary>
    public interface IInterceptor
    {
        void Before(object target, MethodInfo method, object[] args);

        void After(object target, MethodInfo method, object[] args);
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public  class LogAttribute : AopAbstractAttribute
    {
        public override void Before(object target, MethodInfo method, object[] args)
        {
            Console.WriteLine($"特性进入 类：{target.GetType().Name} 方法：{method.Name}");
            base.Before( target,  method, args);
        }

        public override void After(object target, MethodInfo method, object[] args)
        {
            Console.WriteLine($"特性走出 类：{target.GetType().Name} 方法：{method.Name}");
            base.After(target, method, args);
        }
    }


    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class Log1Attribute : AopAbstractAttribute
    {
        public override void Before(object target, MethodInfo method, object[] args)
        {
            Console.WriteLine($"特性 1 进入 类：{target.GetType().Name} 方法：{method.Name}");
            base.Before(target, method, args);
        }

        public override void After(object target, MethodInfo method, object[] args)
        {
            Console.WriteLine($"特性  1 走出 类：{target.GetType().Name} 方法：{method.Name}");
            base.After(target, method, args);
        }
    }
}
