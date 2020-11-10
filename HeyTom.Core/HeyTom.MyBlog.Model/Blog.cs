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

}
