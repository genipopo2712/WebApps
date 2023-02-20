using Dapper;
using System.Data;

namespace WebApps.Models
{
    public class StatisticRepository : Repository, IStatisticRepository
    {
        public StatisticRepository(IDbConnection connection) : base(connection)
        {
        }

        public int CountComments()
        {
            return connection.ExecuteScalar<int>("SELECT COUNT(1) FROM Comments");
        }

        public int CountQuestions()
        {
            //Use Dapper
            return connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Posts WHERE PostTypeId=1",commandTimeout: 120);
        }

        public int CountUsers()
        {
            return connection.ExecuteScalar<int>("SELECT COUNT(*) FROM Users");
        }

        public double QuestionOverAnswerRate()
        {
            return connection.ExecuteScalar<double>("QuestionOverAnswerRate", commandType: CommandType.StoredProcedure, commandTimeout:120);
        }
        public IEnumerable<Monthly> GetTotalPostsByYear(int year)
        {
            return connection.Query<Monthly>("GetTotalPostsByYear", new { Y = year }, commandType: CommandType.StoredProcedure, commandTimeout :60);
        }

        public IEnumerable<Pair> GetTotalPostsByType()
        {
            return connection.Query<Pair>("GetTotalPostsByType", commandType: CommandType.StoredProcedure, commandTimeout: 60);
        }
    }
}
