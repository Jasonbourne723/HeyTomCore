using System;
using System.Collections.Generic;
using System.Text;

namespace HeyTom.MyBlog.Model
{
    public class Author
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string GzOpenId { get; set; }

        public string Icon { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Motto { get; set; }
        public string MiniOpenId { get; set; }
        public string QrCode { get; set; }
        public short IsDel { get; set; }

    }
}
