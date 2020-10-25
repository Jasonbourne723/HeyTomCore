using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeyMacchiato.Infra.MvcCore;
using HeyMacchiato.Infra.Util;
using HeyMacchiato.Service.MyBlog.Apps.Models;
using HeyTom.MyBlog.Model;
using HeyTom.MyBlog.Respository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HeyMacchiato.Service.MyBlog.Apps.Controllers
{
    [Route("api/[controller]")]
    public class BlogController : BaseController
    {

        private readonly ILogger<BlogController> _logger;
        private readonly IBlogRepository _blogRepository;

        private Dictionary<string, string> dicBlog = new Dictionary<string, string>() {
            { "blogId","Id"},
            { "name","Name"},
            { "content","Content"},
            { "status","Status"},
        };


        public BlogController(ILogger<BlogController> logger,
                            IBlogRepository blogRepository
            )
        {
            _logger = logger;
            _blogRepository = blogRepository;
        }

        /// <summary>
        /// 获取博客列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesDefaultResponseType(typeof(PageResultModel<Blog>))]

        public IActionResult List([FromBody] ViewParam param)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () =>
            {
                var listparam = ConvertRequest.Convert(param, dicBlog);
                result = _blogRepository.GetPageResult<BlogVModel>(listparam);
            }, false);
           // return  _blogRepository.GetPageResult(null, param.PageIndex, param.PageSize);


        }

        /// <summary>
        /// 查询单个文章
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesDefaultResponseType(typeof(TResultModel<Blog>))]
        public TResultModel<Blog> GetOne([FromBody] IdVModel param)
        {
            var blog = _blogRepository.GetById(param.Id);
            if (blog == null)
            {
                return new TResultModel<Blog>(-1,"未找到该文章");
            }
            return new TResultModel<Blog>(1) {
                TModel = blog
            };
        }
    }

    public class BlogVModel
    {
        public int blogId { get; set; }

        public string name { get; set; }

        public string content { get; set; }

        public short status { get; set; }
    }
}
