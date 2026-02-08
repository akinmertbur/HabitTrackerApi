using HabitTrackerApi.DTOs;

namespace HabitTrackerApi.Services {
    public interface ICheckInService {
        Task<CheckInReadDto> AddCheckInAsync(CheckInCreateDto dto);

        Task<bool> DeleteCheckInAsync(int id);

        Task<IEnumerable<CheckInReadDto>> GetByHabitIdAsync(int habitId);
    }
}
