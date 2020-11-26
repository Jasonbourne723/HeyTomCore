﻿using SqlSugar;
using System;

namespace HeyTom.Resources.Model
{
    [SugarTable("photo_album")]
    public class PhotoAlbum
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Img { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public int UserId { get; set; }

        public DateTime CreateDate { get; set; }

        public short IsDel { get; set; }

        public int PhotoNum { get; set; }
    }
}
