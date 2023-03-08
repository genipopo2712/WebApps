using Dapper;
using System.Data;

namespace WebApps.Models
{
    public class ContactRepository : Repository, IContactRepository
    {
        public ContactRepository(IDbConnection connection) : base(connection)
        {
        }

        public int Add(Contact obj)
        {
            string sql = "INSERT INTO Contact(Name, Email, Subject, Message) VALUES (@Name, @Email, @Subject, @Message)";
            return connection.Execute(sql, new
            {
                Name = obj.Name,
                Email = obj.Email,
                Subject = obj.Subject,
                Message = obj.Message
            });
        }

        public TotalTimeLine CountContactsWithNew(string memberId)
        {
            return connection.QueryFirstOrDefault<TotalTimeLine>("CountContactsWithNew" , new { MemberId=memberId}, commandType:CommandType.StoredProcedure);
        }

        public Contact GetContactById(string memberId, int id)
        {
            return connection.QueryFirstOrDefault<Contact>("GetContactById", new { MemberId = memberId, Id = id }, commandType: CommandType.StoredProcedure);

        }

        public IEnumerable<Contact> GetContacts()
        {
            return connection.Query<Contact>("GetContacts", commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Contact> GetContactWithNew(string memberId)
        {
            return connection.Query<Contact>("GetContactWithNew", new { MemberId = memberId }, commandType: CommandType.StoredProcedure);

        }
    }
}
