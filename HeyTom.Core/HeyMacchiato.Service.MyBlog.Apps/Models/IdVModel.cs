using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeyMacchiato.Service.MyBlog.Apps.Models
{
    public class IdVModel
    {
        public int Id { get; set; }
    }

    public class AddBlogVModel
    {
        public string name { get; set; }

        public string content { get; set; }
    }

    public class UpdateBlogVModel
    {
        public int id { get; set; }

        public string name { get; set; }

        public string content { get; set; }
    }


}
