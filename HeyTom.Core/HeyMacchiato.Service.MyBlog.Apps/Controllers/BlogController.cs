using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HeyMacchiato.Infra.Filter;
using HeyMacchiato.Infra.MvcCore;
using HeyMacchiato.Infra.Util;
using HeyMacchiato.Service.MyBlog.Apps.Models;
using HeyTom.MyBlog.Model;
using HeyTom.MyBlog.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HeyMacchiato.Service.MyBlog.Apps.Controllers
{
    [Route("api/[controller]")]
    public class BlogController : BaseController
    {

        private readonly ILogger<BlogController> _logger;
        private readonly IBlogRepository _blogRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ITagRepository _tagRepository;
        private Dictionary<string, string> dicBlog = new Dictionary<string, string>() {
            { "id","Id"},
            { "name","Name"},
            { "status","Status"},
            {"createDate","CreateDate"},
            {"categoryId","CategoryId"},
            { "authorId","AuthorId"},
            { "isDel","IsDel"},
            { "isTop","IsTop"},
        };


        public BlogController(ILogger<BlogController> logger,
                            IBlogRepository blogRepository,
                            ICategoryRepository categoryRepository,
                            IAuthorRepository authorRepository,
                            ITagRepository tagRepository
            )
        {
            _logger = logger;
            _blogRepository = blogRepository;
            _categoryRepository = categoryRepository;
            _authorRepository = authorRepository;
            _tagRepository = tagRepository;
        }

        /// <summary>
        /// 获取博客列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesDefaultResponseType(typeof(PageResultModel<BlogVModel>))]
        [NoAuthorizationAction]

        public IActionResult List([FromBody] ViewParam param)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () =>
            {
                var listparam = ConvertRequest.Convert(param, dicBlog);
                if (param.Filter?.Exists(x => x.Field == "tagId") ?? false)
                {
                    var tagId = param.Filter.FirstOrDefault(x => x.Field == "tagId").Value;
                    var blogTags = _tagRepository.GetBlogByTagIds(new List<int>() { int.Parse(tagId) });
                    if (blogTags != null && blogTags.Count > 0)
                    {
                        listparam.Filter.Add(new FilterModel()
                        {
                            Field = "id",
                            DbField = "Id",
                            Connector = "and ",
                            Operator = "in",
                            Value = string.Join(",", blogTags.Select(x => x.BlogId))
                        });
                    }
                    else
                    {
                        return;
                    }
                }
                var r = _blogRepository.GetPageResult<BlogVModel>(listparam);
                var categroys = _categoryRepository.GetInUse();
                var author = _authorRepository.GetById(1);
                var tags = _tagRepository.GetByBlogIds(r.TModel?.Select(x => x.id)?.ToList());
                r.TModel?.ForEach(ea =>
                {
                    ea.categoryName = categroys?.FirstOrDefault(x => x.Id == ea.categoryId)?.Name;
                    ea.authorName = author?.Name;
                    ea.tags = tags?.FindAll(x => x.BlogId == ea.id)?.Select(x => new TagVModel() { tagId = x.TagId, tagName = x.TagName }).ToList();
                });
                result = r;
            }, false);
        }

        /// <summary>
        /// 查询单个文章
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesDefaultResponseType(typeof(TResultModel<BlogVModel>))]
        [NoAuthorizationAction]
        public IActionResult GetOne([FromBody] IdVModel param)
        {
            var result = new TResultModel<BlogVModel>(1);
            return this.Wrapper(ref result, () =>
            {
                var blog = _blogRepository.GetById(param.Id);
                if (blog == null)
                {
                    result = new TResultModel<BlogVModel>(-1, "未找到该文章");
                    return;
                }
                var category = _categoryRepository.GetById(blog.CategoryId);
                var author = _authorRepository.GetById(blog.AuthorId);
                result.TModel = new BlogVModel()
                {
                    id = blog.Id,
                    authorId = blog.AuthorId,
                    status = blog.Status,
                    categoryId = blog.CategoryId,
                    content = blog.Content,
                    isDel = blog.IsDel,
                    name = blog.Name,
                    createDate = blog.CreateDate,
                    categoryName = category?.Name,
                    authorName = author?.Name,
                    isTop = blog.IsTop
                };
            }, true);


        }

        /// <summary>
        /// 新增文章
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult Add([FromBody] AddBlogVModel param)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () =>
            {
                _blogRepository.Add(new Blog()
                {
                    AuthorId = 1,
                    CategoryId = param.categoryId,
                    Content = param.content,
                    CreateDate = DateTime.Now,
                    IsDel = 0,
                    Name = param.name,
                    Status = param.status,
                    IsTop = param.isTop
                });
            }, true);
        }

        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult Update([FromBody] UpdateBlogVModel param)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () =>
            {

                var blog = _blogRepository.GetById(param.id);
                if (blog == null)
                {
                    result.ResultNo = -1;
                    result.Message = "未找到该文章";
                    return;
                };
                blog.Name = param.name;
                blog.Content = param.content;
                blog.CategoryId = param.categoryId;
                blog.Status = param.status;
                blog.IsTop = param.isTop;
                result = _blogRepository.Update(blog);
            }, true);
        }


        /// <summary>
        /// 删除文章
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
                result = _blogRepository.Remove(param.Id);
            }, true);
        }

    }


}
