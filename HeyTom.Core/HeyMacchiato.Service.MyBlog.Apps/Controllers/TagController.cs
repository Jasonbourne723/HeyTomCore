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
    public class TagController : BaseController
    {
        private readonly ITagRepository _tagRepository;

        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }


        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(TResultModel<List<TagVModel>>))]
        [NoAuthorizationAction]
        public IActionResult GetAll()
        {
            var result = new TResultModel<List<TagVModel>>(1);
            return this.Wrapper(ref result, () => {

                var tags = _tagRepository.GetList(50);
                result.TModel = new List<TagVModel>();
                tags?.ForEach(ea=> {
                    result.TModel.Add(new TagVModel() { 
                        tagId = ea.Id,
                        tagName = ea.Name
                    });
                });
            }, true);
        }
    }
}
