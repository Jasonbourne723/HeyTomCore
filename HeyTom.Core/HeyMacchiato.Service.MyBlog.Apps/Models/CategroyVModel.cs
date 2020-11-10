using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace HeyMacchiato.Service.MyBlog.Apps.Models
{
    public class CategroyVModel
    {
        public int id { get; set; }

        public string name { get; set; }

        public string remark { get; set; }

        public short  isDel { get; set; }

    }

    public class AddCategoryVModel
    {
        [Required(ErrorMessage ="名称不能为空")]
        public string name { get; set; }

        public string remark { get; set; }
    }

    public class UpdateCategoryVModel
    {
        [Required(ErrorMessage ="Id不能为空")]
        public int id { get; set; }

        [Required(ErrorMessage = "名称不能为空")]
        public string name { get; set; }

        public string remark { get; set; }

    }
}
