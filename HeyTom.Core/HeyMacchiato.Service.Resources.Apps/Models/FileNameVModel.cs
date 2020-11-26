using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeyMacchiato.Service.Resources.Apps.Models
{
    public class FileNameVModel
    {
        public string fileName { get; set; }
    }

    public class ImageFileVModel
    {
        public IFormFile file { get; set; }

        public int albumId { get; set; }
    }
}
