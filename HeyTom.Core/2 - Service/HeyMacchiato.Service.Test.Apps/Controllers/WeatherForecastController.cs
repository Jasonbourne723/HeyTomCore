using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeyMacchiato.Domain.Member.Repository;
using HeyMacchiato.Infra.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;

namespace HeyMacchiato.Service.Test.Apps.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            var aa = "";
            try
            {
                aa += "1";


                try
                {
                    throw new Exception("lli");
                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    aa += "2";
                }
            }
            catch (Exception)
            {
                aa += "3";
            }
            finally
            {
                aa += "4";
            }
            _logger.LogInformation($"{DateTime.Now} hello serilog");
            _logger.LogError($"Error  serilog  {DateTime.Now}");
            Test();
            return aa;
        }

        private void Test()
        {
            var itemNumber = new { name = "sunzi", age = 3 };
            var itemCount = 999;
            Log.Information("Processing item {ItemNumber} of {ItemCount}", itemNumber, itemCount);
        }
    }
}
