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
            return await this.context.Messages
                .Include(m => m.sender)
                .Include( m => m.recipient)
                .FirstOrDefaultAsync( m => m.Id == id);
        }

        public async Task<ICollection<MessageDTO>> GetMessagesForUser(string userName, string container)
        {
            var quary = this.context.Messages
            .OrderByDescending(m => m.messageSent)
            .AsQueryable();

            switch (container)
            {
                case "inbox":
                    quary = quary.Where(m => 
                    m.recipient.userName == userName &&
                    m.recipientDeleted == false);
                    break;

                case "outbox":
                    quary = quary.Where(m => 
                    m.sender.userName == userName &&
                    m.senderDeleted == false);
                    break;

                default:
                    quary = quary.Where(m => 
                    m.recipient.userName == userName &&
                    m.dateRead == null &&
                    m.recipientDeleted == false);
                    break;
            }

            return await quary.Select(m => new MessageDTO
            {
                Id = m.Id,
                senderId = m.senderId,
                senderUsername = m.sender.userName,
                senderPhotoUrl = m.sender.photo.FirstOrDefault(p => p.isMain).url,
                recipientId = m.recipient.Id,
                recipientUsername = m.recipient.userName,
                recipientPhotoUrl = m.recipient.photo.FirstOrDefault(p => p.isMain).url,
                content = m.content,
                dateRead = m.dateRead,
                messageSent = m.messageSent,
            }).ToListAsync();
        }

        public async Task<IEnumerable<MessageDTO>> GetMessageThread(string currentUsername, string otherUsername)
        {
            var messages = await context.Messages
            .Include( u => u.sender).ThenInclude( p => p.photo )
            .Include( u => u.recipient).ThenInclude( p => p.photo )
            .Where( m => 
            (m.sender.userName == currentUsername && 
            m.recipient.userName == otherUsername &&
            m.senderDeleted == false)
            ||
            ( m.recipient.userName == currentUsername && 
            m.sender.userName == otherUsername &&
            m.recipientDeleted == false))
            .OrderByDescending( m => m.messageSent)
            // .Where( m => m.dateRead == null &&
            .ToListAsync();

            foreach (var messg in messages)
            {
                messg.dateRead = DateTime.Now;
            }

            await context.SaveChangesAsync();

            return messages.Select( m => new MessageDTO
            {
                Id = m.Id,
                senderId = m.senderId,
                senderUsername = m.sender.userName,
                senderPhotoUrl = m.sender.photo.FirstOrDefault( p => p.isMain ).url,
                recipientPhotoUrl = m.recipient.photo.FirstOrDefault( p => p.isMain ).url,
                recipientId = m.recipient.Id,
                recipientUsername = m.recipient.userName,
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