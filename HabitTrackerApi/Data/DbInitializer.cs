using Microsoft.EntityFrameworkCore;
using HabitTrackerApi.Entities;

namespace HabitTrackerApi.Data {
    public static class DbInitializer {
        public static async Task SeedAsync(HabitTrackerDbContext context) {
            // Apply migrations automatically
            await context.Database.MigrateAsync();

            // Only seed if habits table is empty
            if (await context.HabitTrackerItems.AnyAsync())
                return;

            var now = DateTime.UtcNow;
            var today = DateOnly.FromDateTime(now);

            var habits = new List<HabitTrackerItem>
            {
                new HabitTrackerItem
                {
                    Name = "Morning Meditation",
                    Description = "10 minutes of mindfulness",
                    TargetPerWeek = 7,
                    CreatedAt = now,
                    UpdatedAt = now,
                    CheckIns = new List<HabitCheckIn>
                    {
                        new HabitCheckIn { Date = today.AddDays(-1), Note = "Great start", CreatedAt = now },
                        new HabitCheckIn { Date = today, Note = "Feeling focused", CreatedAt = now }
                    }
                },
                new HabitTrackerItem
                {
                    Name = "Gym Workout",
                    Description = "Strength training or cardio",
                    TargetPerWeek = 3,
                    CreatedAt = now,
                    UpdatedAt = now,
                    CheckIns = new List<HabitCheckIn>
                    {
                        new HabitCheckIn { Date = today.AddDays(-2), Note = "Leg day", CreatedAt = now }
                    }
                },
                new HabitTrackerItem
                {
                    Name = "Read 20 Pages",
                    Description = "Currently reading: Atomic Habits",
                    TargetPerWeek = 5,
                    CreatedAt = now,
                    UpdatedAt = now
                    // No check-ins yet for this one
                }
            };

            await context.HabitTrackerItems.AddRangeAsync(habits);
            await context.SaveChangesAsync();
        }
    }
}
