using System;
using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class MemberDTO
    {
        public int Id { get; set; }
        public string userName { get; set; }
        public string photoUrl { get; set; }
        public int age { get; set; }
        public string knownAs { get; set; }
        public DateTime created { get; set; }
        public DateTime lastActive { get; set; } 
        public string gender { get; set; }
        public string introduction { get; set; }
        public string lookingFor { get; set; }
        public string interests { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public ICollection<Photo> photo { get; set; }

    }
}