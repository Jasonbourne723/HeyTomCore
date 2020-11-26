using HeyMacchiato.Infra.Util;
using System;

namespace HeyMachiato.Infra.RabbitMq
{
    /// <summary>
    /// rabbitMq接口
    /// </summary>
    public interface IRabbitMqService : IDisposable
    {
        ResultModel Publish<T>(T entity);

        void Subscribe<T>(Action<T> action);
    }
}
