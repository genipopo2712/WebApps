namespace WebApps.Models
{
    public interface IStatisticRepository
    {
        int CountQuestions();
        double QuestionOverAnswerRate();
        int CountUsers();
        int CountComments();
        IEnumerable<Monthly> GetTotalPostsByYear(int year);
        IEnumerable<Pair> GetTotalPostsByType();
    }
}
