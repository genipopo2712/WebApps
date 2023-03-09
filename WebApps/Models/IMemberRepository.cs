namespace WebApps.Models
{
    public interface IMemberRepository
    {
        IEnumerable<Member> GetMembers();
        Member Login(LoginModel obj);
        Member GetMemberById(string id);
        int AddMemberIfNotExists(Member obj);
    }
}
