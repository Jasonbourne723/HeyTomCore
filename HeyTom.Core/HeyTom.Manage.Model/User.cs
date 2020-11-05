using System;

namespace HeyTom.Manage.Model
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public short Status { get; set; }

        public string Remark { get; set; }

        public string Pwd { get; set; }

        public  string Icon { get; set; }

        public DateTime CreateDate { get; set; }


    }
}
