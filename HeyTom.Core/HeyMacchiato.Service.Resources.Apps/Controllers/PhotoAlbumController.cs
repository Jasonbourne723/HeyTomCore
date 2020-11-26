using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeyMacchiato.Infra.Filter;
using HeyMacchiato.Infra.MvcCore;
using HeyMacchiato.Infra.Util;
using HeyMacchiato.Service.Resources.Apps.Models;
using HeyTom.Resources.Model;
using HeyTom.Resources.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeyMacchiato.Service.Resources.Apps.Controllers
{
    [Route("api/[controller]")]
    public class PhotoAlbumController : BaseController
    {
        private readonly IPhotoAlbumRepository _photoAlbumRepository;
        private readonly IPhotoCategoryRepository _photoCategoryRepository;

        private Dictionary<string, string> dicPhotoAlbum = new Dictionary<string, string>() {
            { "id","a.Id"},
            { "name","a.Name"},
            { "img","a.Img"},
            {"createDate","a.CreateDate"},
            {"categoryId","a.CategoryId"},
            { "description","a.Description"},
            { "isDel","a.IsDel"},
            { "categoryName","b.name"},
            { "photoNum","a.PhotoNum"}
        };

        public PhotoAlbumController(IPhotoAlbumRepository photoAlbumRepository,
                                    IPhotoCategoryRepository photoCategoryRepository)
        {
            _photoAlbumRepository = photoAlbumRepository;
            _photoCategoryRepository = photoCategoryRepository;
        }

        /// <summary>
        /// 获取相册列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesDefaultResponseType(typeof(PageResultModel<PhotoAlbumVModel>))]

        public IActionResult List([FromBody] ViewParam param)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () =>
            {
                var listparam = ConvertRequest.Convert(param, dicPhotoAlbum);
                listparam.Filter.Add(new FilterModel()
                {
                    Field = "userId",
                    DbField = "a.UserId",
                    Connector = "and",
                    Operator = "=",
                    Value = _claimEntity.userId.ToString()
                });

                var r = _photoAlbumRepository.GetPageResult<PhotoAlbumVModel>(listparam);

                result = r;

            }, false);
        }

        /// <summary>
        /// 查询单个相册
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesDefaultResponseType(typeof(TResultModel<PhotoAlbumVModel>))]
        public IActionResult GetOne([FromBody] PhotoAlbumIdVModel param)
        {
            var result = new TResultModel<PhotoAlbumVModel>(1);
            return this.Wrapper(ref result, () =>
            {
                var photoAlbum = _photoAlbumRepository.GetById(param.id);
                if (photoAlbum == null)
                {
                    result.ResultNo = -1;
                    result.Message = "未找到该相册";
                    return;
                }
                var category = _photoCategoryRepository.GetById(photoAlbum.CategoryId);

                result.TModel = new PhotoAlbumVModel()
                {
                    categoryId = photoAlbum.CategoryId,
                    categoryName = category?.Name,
                    createDate = photoAlbum.CreateDate,
                    description = photoAlbum.Description,
                    id = photoAlbum.Id,
                    img = photoAlbum.Img,
                    isDel = photoAlbum.IsDel,
                    name = photoAlbum.Name,
                    photoNum = photoAlbum.PhotoNum,
                    userId = photoAlbum.UserId
                };
            }, true);

        }

        /// <summary>
        /// 新增相册
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult Add([FromBody] AddPhotoAlbumVModel param)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () =>
            {
                _photoAlbumRepository.Add(new HeyTom.Resources.Model.PhotoAlbum()
                {
                    Id = 0,
                    CategoryId = param.categoryId,
                    CreateDate = DateTime.Now,
                    Description = param.description,
                    Img = string.IsNullOrWhiteSpace(param.img) ? "" : param.img,
                    IsDel = 0,
                    Name = param.name,
                    PhotoNum = 0,
                    UserId = this._claimEntity.userId
                });
            }, true);
        }

        /// <summary>
        /// 修改相册
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult Update([FromBody] UpdatePhotoAlbumVModel param)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () =>
            {
                _photoAlbumRepository.Update(new PhotoAlbum()
                {
                    Id = param.id,
                    UserId = _claimEntity.userId,
                    CategoryId = param.categoryId,
                    Description = param.description,
                    Img = param.img,
                    Name = param.name
                });
            }, true);
        }


        /// <summary>
        /// 删除相册
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult Remove([FromBody] PhotoAlbumIdVModel param)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () =>
            {
                _photoAlbumRepository.Remove(param.id);
            }, true);
        }

    }
}
