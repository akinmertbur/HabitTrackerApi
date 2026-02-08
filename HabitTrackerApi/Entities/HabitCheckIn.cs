using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HabitTrackerApi.Entities {
    public class HabitCheckIn {
        [Key]
        public int Id { get; set; }

        public int HabitId { get; set; }

        [ForeignKey("HabitId")]
        public HabitTrackerItem Habit { get; set; } = null!;

        public DateOnly Date { get; set; }

        [StringLength(200)]
        public string? Note { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
