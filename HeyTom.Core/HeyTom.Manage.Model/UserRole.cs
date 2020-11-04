using SqlSugar;

namespace HeyTom.Manage.Model
{
    [SugarTable("user_role")]
    public class UserRole
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }
    }
}
