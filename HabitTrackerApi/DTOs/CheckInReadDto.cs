namespace HabitTrackerApi.DTOs {
    public class CheckInReadDto {
        public int Id { get; set; }

        public int HabitId { get; set; }

        public DateOnly Date { get; set; }

        public string? Note { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
