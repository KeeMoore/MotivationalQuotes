using MotivationalQuotes.Models;
using Microsoft.EntityFrameworkCore;

namespace MotivationalQuotes.Context;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public ApplicationContext(DbContextOptions options) : base(options) { }
}