using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;

// namespace API.Data
// {
public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<AppUser> Users { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<UserLike> UserLikes { get; set; }
    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserLike>()
            .HasKey(k => new { k.sourceUserId, k.likedUserId });

        builder.Entity<UserLike>()
            .HasOne(s => s.sourceUser)
            .WithMany(l => l.likedUsers)
            .HasForeignKey(s => s.sourceUserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<UserLike>()
            .HasOne(s => s.likedUser)
            .WithMany(l => l.likedByUsers)
            .HasForeignKey(s => s.likedUserId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.Entity<Message>()
            .HasOne(m => m.sender)
            .WithMany(appUser => appUser.massegeSent)
            .HasForeignKey( appUser => appUser.senderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Message>()
            .HasOne( m => m.recipient)
            .WithMany( appUser => appUser.massageRecieved)
            .HasForeignKey( m => m.recipientId)
            .OnDelete(DeleteBehavior.Restrict);
        }

}