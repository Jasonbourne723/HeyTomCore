using SqlSugar;

namespace HeyTom.Manage.Model
{
    [SugarTable("role_menu")]
    public class RoleMenu {
        public int Id { get; set; }

        public int RoleId { get; set; }

        public int MenuId { get; set; }
    }
}
