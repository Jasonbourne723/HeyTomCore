using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeyMacchiato.Infra.MvcCore;
using HeyMacchiato.Infra.Util;
using HeyMacchiato.Service.MyBlog.Apps.Models;
using HeyTom.MyBlog.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeyMacchiato.Service.MyBlog.Apps.Controllers
{
    [Route("api/[controller]")]
    public class AuthorController : BaseController
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        /// <summary>
        /// 获取作者
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(TResultModel<AuthorVModel>))]
        public IActionResult GetOne([FromBody] IdVModel param)
        {
            var result = new TResultModel<AuthorVModel>(1);
            return this.Wrapper(ref result, () =>
            {
                var author = _authorRepository.GetById(param.Id);
                if (author == null)
                {
                    result.ResultNo = -1;
                    result.Message = "未找到该作者";
                    return;
                }
                result.TModel = new AuthorVModel()
                {
                    id = author.Id,
                    email = author.Email,
                    gzOpenId = author.GzOpenId,
                    icon = author.Icon,
                    isDel = author.IsDel,
                    miniOpenId = author.MiniOpenId,
                    mobile = author.Mobile,
                    motto = author.Motto,
                    name = author.Name,
                    qrCode = author.QrCode
                };
            }, true);
        }

    }
}
