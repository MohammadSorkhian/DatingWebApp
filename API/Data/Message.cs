using System;
using API.Entities;

namespace API.Data
{
    public class Message
    {
        public int Id { get; set; }
        public int senderId { get; set; }
        public AppUser sender { get; set; }
        public int recipientId { get; set; }
        public AppUser recipient { get; set; }
        public string content { get; set; }
        public DateTime? dateRead { get; set; }
        public DateTime messageSent { get; set; } = DateTime.Now;
        public Boolean senderDeleted { get; set; }
        public Boolean recipientDeleted { get; set; }
    }
}