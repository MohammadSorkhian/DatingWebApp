using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;

namespace API.Interfaces
{
    public interface IMessageRepository
    {
        void AddMessage(Message message);
        void DeleteMessage(Message message);
        Task<Message> GetMessage(int id);
        Task<ICollection<MessageDTO>> GetMessagesForUser(string userName, string container="unread");
        Task<IEnumerable<MessageDTO>> GetMessageThread(string currentUsername, string recipientUsername);
        Task<bool> SaveAllAsync();

        
    }
}