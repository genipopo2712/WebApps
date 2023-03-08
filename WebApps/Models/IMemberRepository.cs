namespace WebApps.Models
{
    public interface IMemberRepository
    {
        Member Login(LoginModel obj);
        Member GetMemberById(string id);
        int AddMemberIfNotExists(Member obj);
    }
}
