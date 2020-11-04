using System.Collections.Generic;

namespace HeyMacchiato.Service.Manage.Apps.Models
{
    public class RoleVModel
    {
        public int id { get; set; }

        public string name { get; set; }

        public string remark { get; set; }

        public short type { get; set; }
    }

    public class RoleIdVModel
    {
        public int roleId { get; set; }
    }

    public class AddRoleVModel
    {
        public string name { get; set; }

        public string remark { get; set; }
    }


    public class SetMenuPermissionVModel
    {
        public int roleId { get; set; }

        public List<int> menuIds { get; set; }
    }

  
}
