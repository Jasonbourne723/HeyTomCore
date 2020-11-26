using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeyMacchiato.Service.Resources.Apps.Models
{
    public class PhotoAlbumVModel
    {
        public int id { get; set; }

        public string name { get; set; }

        public string img { get; set; }

        public string description { get; set; }

        public int categoryId { get; set; }

        public string categoryName { get; set; }

        public int userId { get; set; }

        public DateTime createDate { get; set; }

        public short isDel { get; set; }

        public int photoNum { get; set; }
    }

    public class PhotoVModel
    {
        public int id { get; set; }

        public string name { get; set; }

        public string path { get; set; }

        public DateTime createDate { get; set; }

        public int userId { get; set; }

        public int albumId { get; set; }

        public string  albumName { get; set; }
    }

    public class UpdatePhotoVModel
    {
        public int id { get; set; }

        public string name { get; set; }

        public int albumId { get; set; }
    }
}
