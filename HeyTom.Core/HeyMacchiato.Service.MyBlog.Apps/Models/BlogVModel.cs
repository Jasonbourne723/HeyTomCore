using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeyMacchiato.Service.MyBlog.Apps.Models
{
    public class BlogVModel
    {
        public int id { get; set; }

        public string name { get; set; }

        public string content { get; set; }

        public DateTime createDate { get; set; }

        public int categoryId { get; set; }

        public string categoryName { get; set; }

        public int authorId { get; set; }

        public string authorName { get; set; }

        public short status { get; set; }

        public short isDel { get; set; }

    }

    public class AuthorVModel
    {
        public int id { get; set; }

        public string name { get; set; }

        public string gzOpenId { get; set; }

        public string icon { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string motto { get; set; }
        public string miniOpenId { get; set; }
        public string qrCode { get; set; }
        public short isDel { get; set; }
    }
}
