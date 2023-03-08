using Dapper;
using System.Data;

namespace WebApps.Models
{
    public class MemberRepository : Repository, IMemberRepository
    {
        public MemberRepository(IDbConnection connection) : base(connection)
        {
        }

        public int AddMemberIfNotExists(Member obj)
        {
            return connection.Execute("AddMemberIfNotExists", obj ,  commandType: CommandType.StoredProcedure);

        }

        public Member GetMemberById(string id)
        {
            string sql = "SELECT Avatar, MemberId, Username, Fullname, Email, Gender FROM Member WHERE MemberId = @Id";
            return connection.QuerySingleOrDefault<Member>(sql, new
            {
                Id =id 
            });
        }

        public Member Login(LoginModel obj)
        {
            string sql = "SELECT Avatar, MemberId, Username, Fullname, Email, Gender FROM Member WHERE Username = @Usr AND Password = @Pwd";
            return connection.QuerySingleOrDefault<Member>(sql, new
            {
                Usr = obj.Usr,
                Pwd = Helper.Hash(obj.Pwd)
            }) ;
        }


    }
}
