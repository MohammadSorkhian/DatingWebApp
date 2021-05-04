using System.Text.Json.Serialization;

namespace API.Entities
{
    public class Photo
    {
        public int id { get; set; }
        public string url { get; set; }
        public bool isMain { get; set; }
        public string publicId { get; set; }
        [JsonIgnore]
        public AppUser appUser { get; set; }
        public int appUserId { get; set; }
    }
}