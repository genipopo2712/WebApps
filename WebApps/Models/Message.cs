namespace WebApps.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string MemberId { get; set; }
        public string Avatar { get; set; }
        public string Fullname { get; set; }
        public string Content { get; set; }
        public DateTime MessageDate { get; set; }

    }
}
