using SqlSugar;
using System;

namespace HeyTom.Resources.Model
{
    [SugarTable("photo_category")]
    public class PhotoCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        public int UserId { get; set; }
    }
}
