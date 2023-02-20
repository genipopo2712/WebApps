using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using WebApps.Models;

namespace WebApps
{
    public class ChatHub : Hub
    {
        IMemberRepository memberRepository;
        IMessageRepository messageRepository;
        public ChatHub(IMessageRepository messageRepository, IMemberRepository memberRepository)
        {
            this.memberRepository = memberRepository;
            this.messageRepository = messageRepository;
        }
        public void Send(string user, string message)
        {
            Message obj = new Message
            {
                MemberId= user,
                Content= message
            };
            int ret = messageRepository.Add(obj);
            if (ret > 0)
            {
                Member member = memberRepository.GetMemberById(user);
                obj.Avatar= member.Avatar;
                obj.Fullname=member.Fullname;
                obj.MessageDate= DateTime.Now;
            }
            //broadcast chat with send all
            Clients.All.SendAsync("recMsg",user,obj);
            //private chat with specific ppl
            //group chat in group
            //Save massage
        }
    }
}
