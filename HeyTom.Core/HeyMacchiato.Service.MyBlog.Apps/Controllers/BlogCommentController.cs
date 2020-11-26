using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeyMacchiato.Infra.Filter;
using HeyMacchiato.Infra.MvcCore;
using HeyMacchiato.Infra.Util;
using HeyMacchiato.Service.MyBlog.Apps.Models;
using HeyTom.MyBlog.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeyMacchiato.Service.MyBlog.Apps.Controllers
{
    [Route("api/[controller]")]
    public class BlogCommentController : BaseController
    {
        private readonly IBlogCommentRepository _blogCommentRepository;

        private Dictionary<string, string> dic = new Dictionary<string, string>()
        {
            { "id","Id"},
            { "blogId","BlogId"},
            { "backId","BackId"},
            { "content","Content"},
            { "createDate","CreateDate"},
            { "userId","UserId"},
            { "userName","UserName"},
            { "backuserId","BackUserId"},
            { "backuserName","BackUserName"}
        };

        public BlogCommentController(IBlogCommentRepository blogCommentRepository)
        {
            _blogCommentRepository = blogCommentRepository;
        }

        /// <summary>
        /// 获取博客评论列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesDefaultResponseType(typeof(PageResultModel<BlogCommentVModel>))]
        [NoAuthorizationAction]

        public IActionResult List([FromBody] ViewParam param)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () =>
            {
                var listParam = ConvertRequest.Convert(param, dic);
                result = _blogCommentRepository.GetPageResult<BlogCommentVModel>(listParam);
            }, false);
        }

        /// <summary>
        /// 新增文章评论
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult Add([FromBody] AddBlogCommentVModel param)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () =>
            {
                result = _blogCommentRepository.Add(new HeyTom.MyBlog.Model.BlogComment()
                {
                    BlogId = param.blogId,
                    UserId = param.userId,
                    BackId = param.backId,
                    BackUserId = param.backuserId,
                    BackUserName = param.backuserName,
                    Content = param.content,
                    CreateDate = DateTime.Now,
                    UserName = param.userName
                });
            }, true);
        }


        /// <summary>
        /// 删除文章评论
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult Remove([FromBody] IdVModel param)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () =>
            {
                _blogCommentRepository.Delete(param.Id);
            }, true);
        }
    }
}
