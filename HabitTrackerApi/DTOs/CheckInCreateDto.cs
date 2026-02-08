using System.ComponentModel.DataAnnotations;

namespace HabitTrackerApi.DTOs {
    public class CheckInCreateDto {
        [Required]
        public int HabitId { get; set; }

        public DateOnly Date { get; set; }
        public string? Note { get; set; }
    }
}
