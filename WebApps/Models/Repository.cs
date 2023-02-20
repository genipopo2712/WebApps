using System.Data;

namespace WebApps.Models
{
    public abstract class Repository
    {
        protected IDbConnection connection;
        public Repository(IDbConnection connection) 
        {
            this.connection= connection;
        }
    }
}
