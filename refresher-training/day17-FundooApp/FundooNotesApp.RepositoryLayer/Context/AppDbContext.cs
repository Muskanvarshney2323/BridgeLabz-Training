using FundooNotesApp.ModelLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace FundooNotesApp.RepositoryLayer.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<NoteEntity> Notes { get; set; }

        public DbSet<LabelEntity> Labels { get; set; }
        public DbSet<ReminderEntity> Reminders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<NoteEntity>()
                .HasMany(n => n.Labels)
                .WithMany(l => l.Notes)
                .UsingEntity(j => j.ToTable("NoteLabels"));
        }
    }
}