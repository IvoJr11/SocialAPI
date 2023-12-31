﻿using Microsoft.EntityFrameworkCore;
using SocialAPI.Models;
using SocialApi_NET7.Models;

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
		public DbSet<Like> Likes { get; set; }
		public DbSet<Comment> Comments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Post>()
				.HasOne(u => u.Author)
				.WithMany(u => u.Posts)
				.HasForeignKey(u => u.AuthorId);
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
            modelBuilder.Entity<Like>()
                .HasKey(l => new { l.UserID, l.PostID, l.CommentID });
			modelBuilder.Entity<Comment>()
				.HasOne(c => c.Post)
				.WithMany()
				.HasForeignKey(c => c.PostId);
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Author)
                .WithMany()
                .HasForeignKey(c => c.AuthorId);

        }
    }
}
