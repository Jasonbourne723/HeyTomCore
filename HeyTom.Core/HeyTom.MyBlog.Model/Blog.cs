using System;

namespace HeyTom.MyBlog.Model
{
    public class Blog
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AuthorId { get; set; }

        public int CategoryId { get; set; }

        public string Content { get; set; }


        public DateTime CreateDate { get; set; }

        public short Status { get; set; }

        public short IsDel { get; set; }
    }

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

}
