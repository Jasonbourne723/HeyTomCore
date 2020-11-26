using HeyMacchiato.Domain.Core.IRepository;
using HeyMacchiato.Infra.SqlSugar;
using HeyMacchiato.Infra.Util;
using HeyTom.Resources.Model;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace HeyTom.Resources.Repository
{
    public class PhotoAlbumRepository : BaseRepository<PhotoAlbum>, IPhotoAlbumRepository
    {
        public PhotoAlbumRepository()
        {
            _db = new BaseDbContext<PhotoAlbum>("db_resources");
        }

        public ResultModel Remove(int id)
        {
            return new ResultModel(_multipleDb.Updateable<PhotoAlbum>().SetColumns(x => new PhotoAlbum() { IsDel = 1 }).Where(it => it.Id == id).ExecuteCommand());
        }

        public new ResultModel Update(PhotoAlbum entity)
        {
            return new ResultModel(_multipleDb.Updateable<PhotoAlbum>().SetColumns(x => new PhotoAlbum()
            {
                Name = entity.Name,
                Description = entity.Description,
                Img = entity.Img,
                CategoryId = entity.CategoryId
            }).Where(it => it.Id == entity.Id && it.UserId == entity.UserId).ExecuteCommand());
        }

        public override PageResultModel<T1> GetPageResult<T1>(ListParam listParam)
        {
            int totalCount = 0;
            List<IConditionalModel> where = GetWhere(listParam);
            string select = GetSelect(listParam);
            string sort = GetSort(listParam, ref select);
            var result = new PageResultModel<T1>();
            result.TModel = _multipleDb.Queryable<PhotoAlbum, PhotoCategory>((a, b) => new object[] { JoinType.Left, a.CategoryId == b.Id }).Where(where).OrderBy(sort).Select<T1>(select).ToPageList(listParam.PageIndex, listParam.PageSize, ref totalCount);
            result.RecordCount = totalCount;
            result.PageCount = (int)Math.Ceiling(((decimal)totalCount / listParam.PageSize));
            return result;
        }
    }

    public interface IPhotoAlbumRepository : IBaseRepository<PhotoAlbum>
    {

        ResultModel Remove(int id);

        new ResultModel Update(PhotoAlbum entity);

        
    }



}
