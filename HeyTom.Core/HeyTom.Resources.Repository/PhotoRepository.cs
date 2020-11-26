using HeyMacchiato.Domain.Core.IRepository;
using HeyMacchiato.Infra.SqlSugar;
using HeyMacchiato.Infra.Util;
using HeyTom.Resources.Model;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace HeyTom.Resources.Repository
{
    public class PhotoRepository : BaseRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository()
        {
            _db = new BaseDbContext<Photo>("db_resources");
        }

        public override PageResultModel<T1> GetPageResult<T1>(ListParam listParam)
        {
            int totalCount = 0;
            List<IConditionalModel> where = GetWhere(listParam);
            string select = GetSelect(listParam);
            string sort = GetSort(listParam, ref select);
            var result = new PageResultModel<T1>();
            result.TModel = _multipleDb.Queryable<Photo, PhotoAlbum>((a, b) => new object[] { JoinType.Left, a.AlbumId == b.Id }).Where(where).OrderBy(sort).Select<T1>(select).ToPageList(listParam.PageIndex, listParam.PageSize, ref totalCount);
            result.RecordCount = totalCount;
            result.PageCount = (int)Math.Ceiling(((decimal)totalCount / listParam.PageSize));
            return result;
        }

        public ResultModel UpdatePhoto(Photo photo)
        {
            return new ResultModel(_currentDb.AsUpdateable(photo).SetColumns(x => new Photo()
            {
                Name = photo.Name,
                AlbumId = photo.AlbumId
            }).Where(x => x.Id == photo.Id).ExecuteCommand());
        }
    }

    public interface IPhotoRepository : IBaseRepository<Photo>
    {
        ResultModel UpdatePhoto(Photo photo);
    }
}
