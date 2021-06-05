using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext context;

        public MessageRepository(DataContext context)
        {
            this.context = context;
        }

        public void AddMessage(Message message)
        {
            this.context.Messages.Add(message);
        }

        public void DeleteMessage(Message message)
        {
            this.context.Messages.Remove(message);
        }

        public async Task<Message> GetMessage(int id)
        {
            return await this.context.Messages.FirstOrDefaultAsync( m => m.Id == id);
        }

        public async Task<ICollection<MessageDTO>> GetMessagesForUser(string userName, string container)
        {
            var quary = this.context.Messages
            .OrderByDescending(m => m.messageSent)
            .AsQueryable();

            switch (container)
            {
                case "inbox":
                    quary = quary.Where(m => m.recipient.userName == userName);
                    break;

                case "outbox":
                    quary = quary.Where(m => m.sender.userName == userName);
                    break;

                default:
                    quary = quary.Where(m => 
                    m.recipient.userName == userName && m.dateRead == null);
                    break;
            }

            return await quary.Select(m => new MessageDTO
            {
                Id = m.Id,
                senderId = m.senderId,
                senderUserName = m.sender.userName,
                senderPhotoUrl = m.sender.photo.FirstOrDefault(p => p.isMain).url,
                recipientPhotoUrl = m.recipient.photo.FirstOrDefault(p => p.isMain).url,
                content = m.content,
                dateRead = m.dateRead,
                messageSent = m.messageSent,
            }).ToListAsync();
        }

        public async Task<IEnumerable<MessageDTO>> GetMessageThread(string currentUsername, string recipientUsername)
        {
            var messages = await context.Messages
            .Include( u => u.sender).ThenInclude( p => p.photo )
            .Include( u => u.recipient).ThenInclude( p => p.photo )
            .Where( m => 
            (m.sender.userName == currentUsername && 
            m.recipient.userName == recipientUsername)
            ||
            ( m.recipient.userName == currentUsername && 
            m.sender.userName == recipientUsername))
            .OrderByDescending( m => m.messageSent)
            .Where( m => m.dateRead == null &&
            m.recipient.userName == currentUsername).ToListAsync();

            foreach (var messg in messages)
            {
                messg.dateRead = DateTime.Now;
            }

            await context.SaveChangesAsync();

            return messages.Select( m => new MessageDTO
            {
                Id = m.Id,
                senderId = m.senderId,
                senderUserName = m.sender.userName,
                senderPhotoUrl = m.sender.photo.FirstOrDefault( p => p.isMain ).url,
                recipientPhotoUrl = m.recipient.photo.FirstOrDefault( p => p.isMain ).url,
                content = m.content,
                dateRead = m.dateRead,
                messageSent = m.messageSent,
            });
        }

        public async Task<bool> SaveAllAsync()
        {
            return await this.context.SaveChangesAsync() > 0;
        }

    }
}