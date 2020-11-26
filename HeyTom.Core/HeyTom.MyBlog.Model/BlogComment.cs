using SqlSugar;
using System;

namespace HeyTom.MyBlog.Model
{
    [SugarTable("blog_comment")]
    public class BlogComment {
        public int Id { get; set; }

        public int BlogId { get; set; }

        public int BackId { get; set; }


        public string Content { get; set; }

        public DateTime CreateDate { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public int BackUserId { get; set; }

        public string BackUserName { get; set; }
    }
    [SugarTable("blog_tag")]
    public class BlogTag
    {
        public int Id { get; set; }

        public int BlogId { get; set; }

        public int TagId { get; set; }
    }

}
