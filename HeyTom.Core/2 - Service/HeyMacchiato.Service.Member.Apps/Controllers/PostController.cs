using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeyMacchiato.Application.Member.DTO;
using HeyMacchiato.Application.Member.Interface;
using HeyMacchiato.Infra.MvcCore;
using HeyMacchiato.Infra.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeyMacchiato.Service.Member.Apps.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : BaseController
    {
        private readonly IMemberApplication _memberApplication;

        public PostController(IMemberApplication memberApplication)
        {
            this._memberApplication = memberApplication;
        }

        /// <summary>
        /// 发帖
        /// </summary>
        /// <param name="createPostDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult Post([FromBody]CreatePostDTO createPostDTO)
        {
            var result = new ResultModel(1);
            return Wrapper(ref result,()=> {
                result = _memberApplication.CreatePost(createPostDTO);
            },true);
        }

        /// <summary>
        /// 获取个人帖子列表
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{memberId}")]
        [ProducesDefaultResponseType(typeof(TResultModel<List<PostDTO>>))]
        public IActionResult Get(int memberId)
        {
            var result = new TResultModel<List<PostDTO>>(1);
            return this.Wrapper(ref result,()=> {
                result = _memberApplication.GetPost(memberId);
            },true);
        }
    }
}