namespace API.Entities
{
    public class UserLike
    {
        public int sourceUserId { get; set; }
        public int likedUserId { get; set; }
        public AppUser sourceUser { get; set; }
        public AppUser likedUser { get; set; }
    }
}