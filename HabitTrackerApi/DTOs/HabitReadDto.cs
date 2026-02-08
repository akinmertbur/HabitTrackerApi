namespace HabitTrackerApi.DTOs {
    public class HabitReadDto {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int TargetPerWeek { get; set; }

        public bool IsArchived { get; set; } = false;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public List<CheckInReadDto> CheckIns { get; set; } = new();
    }
}
