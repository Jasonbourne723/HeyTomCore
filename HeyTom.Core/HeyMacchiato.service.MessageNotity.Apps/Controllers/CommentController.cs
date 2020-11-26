using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using HeyMacchiato.Infra.CsRedis;
using HeyMacchiato.Infra.Jwt;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace HeyMacchiato.service.MessageNotity.Apps.Controllers
{
    [Route("api/[controller]")]
    public class commentController : ControllerBase
    {
        private readonly IHubContext<CommentHub> _hubContext;

        public commentController(IHubContext<CommentHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Post()
        {
          //  _hubContext.Clients.Client(MyMemory.connectionIds[0]).SendAsync("Test", "aaaaaaaaa");
            //await _hubContext.Clients.All.SendAsync("Test", "nihaoa");
            //  await _hubContext.Client(MyMemory.connectionIds[0]).SendAsync("Test", "nihaoa1");
        }
    }

    public class CommentHub : Hub
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public CommentHub(IHttpContextAccessor httpContextAccessor,
                            TokenValidationParameters tokenValidationParameters)
        {
            _httpContextAccessor = httpContextAccessor;
            _tokenValidationParameters = tokenValidationParameters;
        }

        public async Task Send1()
        {
             await Clients.All.SendAsync("Test","aa");
            return;
        }


        public override async Task OnConnectedAsync()
        {
            string tokenStr = _httpContextAccessor.HttpContext.Request.Query["access_token"];
            tokenStr = tokenStr.Substring("Bearer ".Length).Trim();
            var jwtToken = JwtHelper.SerializeJwt(tokenStr, _tokenValidationParameters);
            var payLoad = jwtToken.Payload;
            var userId = int.Parse(payLoad.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            var key = $"user:connection:{userId}";
            new CsRedisBase().set(key, Context.ConnectionId);
            await base.OnConnectedAsync();
        }

    }
}
