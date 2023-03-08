namespace WebApps.Models
{
    public interface IContactRepository
    {
        int Add(Contact obj);
        IEnumerable<Contact> GetContacts();
        Contact GetContactById(string memberId,int id);
        IEnumerable<Contact> GetContactWithNew(string memberId);
        TotalTimeLine CountContactsWithNew(string memberId);

    }
}
