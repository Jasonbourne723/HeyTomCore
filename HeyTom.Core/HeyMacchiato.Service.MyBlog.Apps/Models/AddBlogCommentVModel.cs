using System;
using System.ComponentModel.DataAnnotations;

namespace HeyMacchiato.Service.MyBlog.Apps.Models
{
    public class AddBlogCommentVModel
    {
        [Required]
        public int blogId { get; set; }

        public int backId { get; set; }

        [Required]
        public string content { get; set; }
        [Required]
        public int userId { get; set; }
        [Required]
        public string userName { get; set; }

        public int backuserId { get; set; }

        public string backuserName { get; set; }
    }

    public class BlogCommentVModel
    {
        public int id { get; set; }
        public int blogId { get; set; }

        public int backId { get; set; }

        public DateTime createDate { get; set; }
        public string content { get; set; }


        public int userId { get; set; }

        public string userName { get; set; }

        public int backuserId { get; set; }

        public string backuserName { get; set; }
    }

}
