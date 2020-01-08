using System;
using System.Collections.Generic;
using System.Text;

namespace HeyMacchiato.Service.QuartzTask
{
	public  class ServiceRunner
	{
        private readonly QuartzService _quartzService;

        public ServiceRunner(QuartzService quartzService)
        {
            this._quartzService = quartzService;
        }
        public ServiceRunner()
        {
        }

        /// <summary>
        /// 服务开始
        /// </summary>
        public void Start()
        {
            _quartzService.Start();
            _quartzService.StartConsume();
        }

        /// <summary>
        /// 服务结束
        /// </summary>
        public void Stop()
        {
        }
    }
}
