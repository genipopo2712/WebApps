using Dapper;
using System.Data;

namespace WebApps.Models
{
    public class MemberInRoleRepository : Repository, IMemberInRoleRepository
    {
        public MemberInRoleRepository(IDbConnection connection) : base(connection)
        {
        }

        public int Save(MemberInRole obj)
        {
            return connection.Execute("SaveMemberInRole", obj, commandType: CommandType.StoredProcedure);

        }
    }
}
