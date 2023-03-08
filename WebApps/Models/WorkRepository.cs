using Dapper;
using System.Data;

namespace WebApps.Models
{
    public class WorkRepository : Repository, IWorkRepository
    {
        public WorkRepository(IDbConnection connection) : base(connection)
        {

        }

        public int AddWork(Work obj)
        {
            return connection.Execute("INSERT INTO Work(WorkName) VALUES (@Name)", new
            {
                Name=obj.WorkName
            });
        }

        public IEnumerable<Work> GetWorks()
        {
            return connection.Query<Work>("GetWorks", commandType: CommandType.StoredProcedure);
        }
        public int EditChecked(int id)
        {
            return connection.Execute("EditWorkChecked", new { Id = id }, commandType: CommandType.StoredProcedure);
        }
    }
}
