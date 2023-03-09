namespace WebApps.Models
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetRoles();
        int Add(Role obj);
        IEnumerable<RoleChecked> GetRolesChecked(string id);

    }
}
