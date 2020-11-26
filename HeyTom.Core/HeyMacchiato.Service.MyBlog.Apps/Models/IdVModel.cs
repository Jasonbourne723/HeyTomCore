using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HeyMacchiato.Service.MyBlog.Apps.Models
{
    public class IdVModel
    {
        [Required(ErrorMessage ="Id不能为空")]
        public int Id { get; set; }
    }

    public class AddBlogVModel
    {
        [Required(ErrorMessage = "文章标题不能为空")]
        public string name { get; set; }

        [Required(ErrorMessage = "文章内容不能为空")]
        public string content { get; set; }
        public int categoryId { get; set; }

        public short status { get; set; }

        public short isTop { get; set; }
    }

    public class UpdateBlogVModel
    {
        [Required(ErrorMessage = "Id不能为空")]
        public int id { get; set; }
        [Required(ErrorMessage = "文章标题不能为空")]
        public string name { get; set; }
        [Required(ErrorMessage = "文章内容不能为空")]
        public string content { get; set; }
        public int categoryId { get; set; }
        public short status { get; set; }

        public short isTop { get; set; }

    }

    public class FileNameVModel
    {
        public string fileName { get; set; }
    }


}
