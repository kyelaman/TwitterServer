using Microsoft.EntityFrameworkCore;
using TwitterServer.Models.Entities;

namespace TwitterServer
{
    public class TwitterContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public string DbPath { get; }

        public TwitterContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "twitter.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
        }
    }
}
