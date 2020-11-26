using System.ComponentModel.DataAnnotations;

namespace HeyMacchiato.Service.Resources.Apps.Models
{
    public class PhotoAlbumIdVModel
    {
        [Required(ErrorMessage = "id为必填项")]
        public int id { get; set; }
    }
}
