using System.ComponentModel.DataAnnotations;

namespace HeyMacchiato.Service.Manage.Apps.Models
{
    public class MenuVModel
    {
        [Required]
        public int id { get; set; }
        [Required(ErrorMessage ="路由名称不能为空")]
        public string name { get; set; }
        [Required(ErrorMessage = "路由路径不能为空")]
        public string path { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string fileName { get; set; }

        public int parentId { get; set; }

        public string icon { get; set; }

    }



    public class MenuIdVModel
    {
        [Required]
        public int id { get; set; }
    }


    public class AddMenuVModel
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string path { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string fileName { get; set; }

        public int parentId { get; set; }

        public string icon { get; set; }
    }

    public class AddMainMenuVModel
    {
        [Required]
        public string title { get; set; }

        public string icon { get; set; }
    }

    public class UpdateMainMenuVModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string title { get; set; }

        public string icon { get; set; }
    }

}
