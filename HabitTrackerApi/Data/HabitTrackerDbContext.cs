using Microsoft.EntityFrameworkCore;
using HabitTrackerApi.Entities;

namespace HabitTrackerApi.Data {
    public class HabitTrackerDbContext : DbContext {
        public HabitTrackerDbContext(DbContextOptions<HabitTrackerDbContext> options) : base(options) {
        }
        public DbSet<HabitTrackerItem> HabitTrackerItems => Set<HabitTrackerItem>();
        public DbSet<HabitCheckIn> HabitCheckIns => Set<HabitCheckIn>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HabitTrackerItem>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(80);

            modelBuilder.Entity<HabitTrackerItem>()
                .Property(x => x.Description)
                .HasMaxLength(300);

            modelBuilder.Entity<HabitTrackerItem>()
                .Property(x => x.IsArchived)
                .HasDefaultValue(false);

            modelBuilder.Entity<HabitCheckIn>()
                .Property(x => x.Note)
                .HasMaxLength(200);

            modelBuilder.Entity<HabitCheckIn>()
                .Property(x => x.Date)
                .IsRequired()
                .HasColumnType("date"); // Explicitly tell DB to only store the date

            // SPECIFY THE RELATIONSHIP (1 Habit -> Many CheckIns)
            modelBuilder.Entity<HabitCheckIn>()
                .HasOne(c => c.Habit)
                .WithMany(h => h.CheckIns)
                .HasForeignKey(c => c.HabitId)
                .OnDelete(DeleteBehavior.Cascade);

            // THE UNIQUE CONSTRAINT (One check-in per habit per day)
            // This creates a composite index on HabitId and Date
            modelBuilder.Entity<HabitCheckIn>()
                .HasIndex(c => new { c.HabitId, c.Date })
                .IsUnique();
        }
    }
}
