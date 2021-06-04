using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using API.Data;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string userName { get; set; }
        public byte[] passWordHash { get; set; }
        public byte[] passWordSalt { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string knownAs { get; set; }
        public DateTime created { get; set; } = DateTime.Now;
        public DateTime lastActive { get; set; } = DateTime.Now;
        public string gender { get; set; }
        public string introduction { get; set; }
        public string lookingFor { get; set; }
        public string interests { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public ICollection<Photo> photo { get; set; }
        public ICollection<UserLike> likedByUsers{ get; set; }  = new Collection<UserLike>();
        public ICollection<UserLike> likedUsers { get; set; } = new Collection<UserLike>();
        public ICollection<Message> massegeSent { get; set; }
        public ICollection<Message> massageRecieved { get; set; }


        public int GetAge()
        {
            var today = DateTime.Now;
            var age = today.Year - this.dateOfBirth.Year;
            if (this.dateOfBirth > today.AddYears(-age))
                age--;
            return age;
        }
        

    }
}