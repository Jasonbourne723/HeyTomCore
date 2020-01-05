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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MemberController : BaseController
    {
        private readonly IMemberApplication _memberApplication;

        public MemberController(IMemberApplication memberApplication)
        {
            this._memberApplication = memberApplication;
        }

        /// <summary>
        /// 获取会员信息
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [Route("{email}")]
        [HttpGet]
        [ProducesDefaultResponseType(typeof(MemberDTO))]
        public IActionResult Get(string email)
        {
            var result = new TResultModel<MemberDTO>(1,"success");
            return this.Wrapper(ref result,()=> {
                result =  _memberApplication.GetMemberByEmail(email);
            },true);
        }

        /// <summary>
        /// 修改会员基础资料
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult Put([FromBody]UpdateInfoDTO updateInfoDTO)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () => {
                result = _memberApplication.UpdateMemberInfo(updateInfoDTO);
            }, true);
        }
    }
}