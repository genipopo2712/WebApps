using Dapper;
using System.Data;

namespace WebApps.Models
{
    public class MessageRepository : Repository, IMessageRepository
    {
        public MessageRepository(IDbConnection connection) : base(connection)
        {
        }

        public int Add(Message obj)
        {
            string sql = "INSERT INTO Message(MemberId, Content) VALUES (@MemberId, @Content)";
            return connection.Execute(sql, new 
            { 
                MemberId = obj.MemberId,
                Content = obj.Content
            });
        }

        public IEnumerable<Message> GetMessages()
        {
            string sql = "SELECT Message.*, Avatar, Fullname FROM Message JOIN Member ON Message.MemberId = Member.MemberId";
            return connection.Query<Message>(sql);
        }

    }
}
