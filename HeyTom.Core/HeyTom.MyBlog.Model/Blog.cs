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

        public short IsTop { get; set; }
    }

    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class BlogTagModel
    {

        public int BlogId { get; set; }

        public int TagId { get; set; }

        public string TagName { get; set; }
    }

}
