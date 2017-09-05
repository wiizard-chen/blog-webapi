namespace blogModel
{
    public class Privilege
    {
        public int Id { get; set; }
        public string PrivilegeName { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}