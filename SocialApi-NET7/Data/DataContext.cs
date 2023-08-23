using Microsoft.EntityFrameworkCore;
using SocialAPI.Models;

namespace SocialAPI.Data
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) 
		{
		}
		public DbSet<User> Users { get; set; }
		public DbSet<Post> Posts { get; set; }
		public DbSet<User> Followers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
				.Property(x => x.UUID)
				.HasColumnType("uuid")
				.HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<Post>()
                .Property(x => x.UUID)
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");
            modelBuilder.Entity<Post>()
				.HasOne(u => u.Author)
				.WithMany(u => u.Posts)
				.HasForeignKey(u => u.Id);
			modelBuilder.Entity<Followers>()
				.HasKey(f => new { f.FollowerID, f.FollowingID });
			modelBuilder.Entity<Followers>()
				.HasOne(f => f.Follower)
				.WithMany(u => u.FollowingList)
				.HasForeignKey(f => f.FollowerID);
			modelBuilder.Entity<Followers>()
				.HasOne(f => f.Following)
				.WithMany(u => u.FollowersList)
				.HasForeignKey(f => f.FollowingID);
		}
    }
}
