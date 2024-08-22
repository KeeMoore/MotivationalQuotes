using MotivationalQuotes.Models;
using Microsoft.EntityFrameworkCore;

namespace MotivationalQuotes.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<FavoriteQuote> FavoriteQuotes { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }


    }
}
