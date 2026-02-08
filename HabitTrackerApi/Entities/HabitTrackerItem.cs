using System.ComponentModel.DataAnnotations;

namespace HabitTrackerApi.Entities {
    public class HabitTrackerItem {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 3)]
        public string Name { get; set; } = null!;

        [StringLength(300)]
        public string? Description { get; set; }

        [Range(1, 7)]
        public int TargetPerWeek { get; set; }

        public bool IsArchived { get; set; } = false;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public ICollection<HabitCheckIn> CheckIns { get; set; } = new List<HabitCheckIn>();
    }
}
