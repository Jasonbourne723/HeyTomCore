using System;
using System.Threading;
using System.Threading.Tasks;
using HeyMacchiato.Infra.CsRedis;
using HeyMacchiato.service.MessageNotity.Apps.Controllers;
using HeyMacchiato.service.MessageNotity.Apps.Models;
using HeyMachiato.Infra.RabbitMq;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace HeyMacchiato.service.MessageNotity.Apps.BgService
{
    public class SubscribeService : BackgroundService
    {
        private readonly IRabbitMqService _rabbitMqService;
        private readonly IHubContext<CommentHub> _hubContext;

        public SubscribeService(IRabbitMqService rabbitMqService,
                                IHubContext<CommentHub> hubContext)
        {
            _rabbitMqService = rabbitMqService;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() =>
            {

                _rabbitMqService.Subscribe<CommentMessageNotity>(ea =>
                {
                    var key = "user:connection:1";
                    var connectionId = new CsRedisBase().Get(key);
                    _hubContext.Clients.Client(connectionId).SendAsync("Test", ea);
                    Console.WriteLine(JsonConvert.SerializeObject(ea));
                });
            });

        }
    }
}
