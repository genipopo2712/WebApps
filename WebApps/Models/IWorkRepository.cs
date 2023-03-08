namespace WebApps.Models
{
    public interface IWorkRepository
    {
        IEnumerable<Work> GetWorks();
        int AddWork(Work obj);
        int EditChecked(int id);
    }
}
