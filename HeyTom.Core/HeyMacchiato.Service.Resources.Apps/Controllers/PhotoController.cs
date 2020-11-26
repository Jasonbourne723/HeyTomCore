using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HeyMacchiato.Infra.MvcCore;
using HeyMacchiato.Infra.Util;
using HeyMacchiato.Service.Resources.Apps.Models;
using HeyTom.Resources.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HeyMacchiato.Service.Resources.Apps.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : BaseController
    {
        private readonly IConfiguration _configuration;
        private readonly IPhotoRepository _photoRepository;

        private Dictionary<string, string> _dicPhoto = new Dictionary<string, string>() {
            { "id","a.Id"},
            { "name","a.Name"},
            { "path","a.Path"},
            {"createDate","a.CreateDate"},
            {"albumId","a.AlbumId"},
            { "albumName","b.Name"},
            { "userId","a.UserId"}
        };

        public PhotoController(IConfiguration configuration,
                                IPhotoRepository photoRepository)
        {
            _configuration = configuration;
            _photoRepository = photoRepository;
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="imageFileVModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult AddImage([FromForm] ImageFileVModel imageFileVModel)
        {
            var result = new TResultModel<FileNameVModel>(1);
            return this.Wrapper(ref result, () =>
            {
                var file = imageFileVModel.file;
                var userId = _claimEntity.userId;
                var suffix = Path.GetExtension(file.FileName);//提取上传的文件文件后缀
                //if (this._pictureOptions.FileTypes.IndexOf(suffix) >= 0)//检查文件格式
                //{
                var fileName = $@"{Guid.NewGuid()}{suffix}";
                var baseUrl = _configuration["ImageUrl"];
                var imageUrl = Path.Combine(baseUrl, userId.ToString(), fileName);

                var path = Path.Combine($@"D:\Image", userId.ToString());
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var fullpath = Path.Combine(path, fileName);
                using (FileStream fs = System.IO.File.Create(fullpath))//注意路径里面最好不要有中文
                {
                    file.CopyTo(fs);//将上传的文件文件流，复制到fs中
                    fs.Flush();//清空文件流
                }
                _photoRepository.Add(new HeyTom.Resources.Model.Photo()
                {
                    AlbumId = imageFileVModel.albumId,
                    CreateDate = DateTime.Now,
                    Name = file.FileName,
                    Path = imageUrl,
                    UserId = userId
                });

                result.TModel = new FileNameVModel()
                {
                    fileName = imageUrl
                };
            }, true);
        }

        /// <summary>
        /// 获取相册列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesDefaultResponseType(typeof(PageResultModel<PhotoVModel>))]

        public IActionResult List([FromBody] ViewParam param)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () =>
            {
                var listparam = ConvertRequest.Convert(param, _dicPhoto);
                listparam.Filter.Add(new FilterModel()
                {
                    Field = "userId",
                    DbField = "a.UserId",
                    Connector = "and",
                    Operator = "=",
                    Value = _claimEntity.userId.ToString()
                });

                var r = _photoRepository.GetPageResult<PhotoVModel>(listparam);

                result = r;

            }, false);
        }


        /// <summary>
        /// 修改相册
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesDefaultResponseType(typeof(ResultModel))]
        public IActionResult Update([FromBody] UpdatePhotoVModel param)
        {
            var result = new ResultModel();
            return this.Wrapper(ref result, () =>
            {
                _photoRepository.UpdatePhoto(new HeyTom.Resources.Model.Photo()
                {
                    Id = param.id,
                    AlbumId = param.albumId,
                    Name = param.name
                });
            }, true);
        }
    }
}
