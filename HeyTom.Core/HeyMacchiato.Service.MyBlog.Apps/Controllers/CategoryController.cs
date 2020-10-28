using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeyMacchiato.Infra.MvcCore;
using HeyMacchiato.Infra.Util;
using HeyMacchiato.Service.MyBlog.Apps.Models;
using HeyTom.MyBlog.Respository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeyMacchiato.Service.MyBlog.Apps.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// 获取所有分类
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(TResultModel<List<CategroyVModel>>))]
        public IActionResult GetAll()
        {
            var result = new TResultModel<List<CategroyVModel>>(1);
            return this.Wrapper(ref result, () =>
            {
                result.TModel = new List<CategroyVModel>();
                var categorys = _categoryRepository.GetInUse();
                categorys?.ForEach(ea =>
                {
                    result.TModel.Add(new CategroyVModel()
                    {
                        id = ea.Id,
                        isDel = ea.IsDel,
                        name = ea.Name,
                        remark = ea.Remark
                    });
                });
            }, false);
        }

        /// <summary>
        /// 新增分类
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult Add([FromBody] AddCategoryVModel param)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () =>
            {
                result =  _categoryRepository.Add(new HeyTom.MyBlog.Model.Category()
                {
                    Name = param.name,
                    IsDel = 0,
                    Remark = param.remark
                });
            }, true);
        }

       /// <summary>
       /// 删除分类
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
                result = _categoryRepository.Delete(param.Id);
            }, true);
        }

        /// <summary>
        /// 更新分类
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult Update([FromBody] UpdateCategoryVModel param)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () =>
            {
                result = _categoryRepository.Update(new HeyTom.MyBlog.Model.Category()
                {
                    Id = param.id,
                    Name = param.name,
                    Remark = param.remark,
                    IsDel = 0
                });
            }, true);
        }
    }
}
