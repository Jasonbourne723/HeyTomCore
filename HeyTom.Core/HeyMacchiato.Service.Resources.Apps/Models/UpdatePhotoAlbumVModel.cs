using System.ComponentModel.DataAnnotations;

namespace HeyMacchiato.Service.Resources.Apps.Models
{
    public class UpdatePhotoAlbumVModel
    {
        [Required(ErrorMessage = "id为必填项")]
        public int id { get; set; }
        [Required(ErrorMessage = "名称为必填项")]
        public string name { get; set; }
        [Required(ErrorMessage = "封片图片为必填项")]
        public string img { get; set; }
        [Required(ErrorMessage = "描述为必填项")]
        public string description { get; set; }
        [Required(ErrorMessage = "分类为必填项")]
        public int categoryId { get; set; }

    }
}
