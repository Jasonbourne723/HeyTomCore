using System;

namespace HeyTom.Resources.Model
{
    public class Photo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public  string Path { get; set; }

        public DateTime CreateDate { get; set; }

        public int UserId { get; set; }

        public int AlbumId { get; set; }
    }
}
