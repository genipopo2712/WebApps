namespace WebApps.Models
{
    public class Member
    {
        public string MemberId { get; set;}
        public string Username { get; set; }
        public string? Password { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Avatar { get; set; }

        public Gender Gender { get; set; }
    }
}
