namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string userName { get; set; }
        public byte[] passWordHash { get; set; }
        public byte[] passWordSalt { get; set; }
    }
}