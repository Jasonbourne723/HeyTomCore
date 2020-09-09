using System;
using System.Reflection;

namespace HeyMacchiato.Infra.Aop
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method)]
    public abstract class AopAbstractAttribute: Attribute
    {
        public virtual void Before(object target, MethodInfo method, object[] args)
        {
            return;
        }

        public virtual void After(object target, MethodInfo method, object[] args)
        {
            return;
        }
    }
}
