using System.ComponentModel.DataAnnotations;

namespace HabitTrackerApi.DTOs {
    public class HabitUpdateDto {
        [Required]
        [StringLength(80, MinimumLength = 3)]
        public string Name { get; set; } = null!;

        [StringLength(300)]
        public string? Description { get; set; }

        [Range(1, 7)]
        public int TargetPerWeek { get; set; }

        public bool IsArchived { get; set; }
    }
}
