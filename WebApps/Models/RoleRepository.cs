using Dapper;
using System.Data;

namespace WebApps.Models
{
    public class RoleRepository : Repository, IRoleRepository
    {
        public RoleRepository(IDbConnection connection) : base(connection)
        {
        }
        public int Add(Role obj)
        {
            return connection.Execute("AddRole", new { Name = obj.RoleName }, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Role> GetRoles()
        {
            return connection.Query<Role>("SELECT * FROM Role");

        }

        public IEnumerable<RoleChecked> GetRolesChecked(string id)
        {
            return connection.Query<RoleChecked>("GetRoleWithChecked", new { Id = id }, commandType: CommandType.StoredProcedure);
        }
    }
}
