using System;

namespace HeyMacchiato.Service.Manage.Apps.Models
{
    public class UserVModel
    {
        public int id { get; set; }

        public string name { get; set; }

        public string email { get; set; }

        public short status { get; set; }

        public string remark { get; set; }

        public DateTime createDate { get; set; }

        public string icon { get; set; }

        public int roleId { get; set; }

        public string roleName { get; set; }
    }

    public class AddUserVModel
    {

        public string name { get; set; }

        public string email { get; set; }

        public short status { get; set; }

        public string remark { get; set; }

        public int roleId { get; set; }

    }

    public class UserStatusVModel
    {
        public int userId { get; set; }

        public short status { get; set; }
    }
}
