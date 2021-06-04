using System;

namespace API.DTOs
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public int senderId { get; set; }
        public string senderUserName { get; set; }
        public string senderPhotoUrl { get; set; }
        public string recipientPhotoUrl { get; set; }
        public string content { get; set; }
        public DateTime? dateRead { get; set; }
        public DateTime messageSent { get; set; } = DateTime.Now;
    }
}