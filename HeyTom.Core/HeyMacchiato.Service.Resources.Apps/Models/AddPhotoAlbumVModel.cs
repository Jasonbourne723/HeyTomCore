using System.ComponentModel.DataAnnotations;

namespace HeyMacchiato.Service.Resources.Apps.Models
{
    public class AddPhotoAlbumVModel
    {
        [Required(ErrorMessage ="名称为必填项")]
        public string name { get; set; }

        [Required(ErrorMessage = "描述为必填项")]
        public string description { get; set; }
        [Required(ErrorMessage = "分类为必填项")]
        public int categoryId { get; set; }

        public string img { get; set; }

    }
}
