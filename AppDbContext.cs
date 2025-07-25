
using FolderWatcherApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FolderWatcherApp
{
    public class AppDbContext : DbContext
    {
        public DbSet<Setting> Settings { get; set; }
        public DbSet<MonitoredFolder> MonitoredFolders { get; set; }
        public DbSet<Recipient> Recipients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=folder_watcher.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure the one-to-many relationship
            modelBuilder.Entity<Setting>()
                .HasMany(s => s.MonitoredFolders)
                .WithOne(f => f.Setting)
                .HasForeignKey(f => f.SettingId);

            modelBuilder.Entity<Setting>()
                .HasMany(s => s.Recipients)
                .WithOne(r => r.Setting)
                .HasForeignKey(r => r.SettingId);
        }
    }
}
