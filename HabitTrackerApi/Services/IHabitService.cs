using HabitTrackerApi.DTOs;

namespace HabitTrackerApi.Services {
    public interface IHabitService {
        Task<IEnumerable<HabitReadDto>> GetAllAsync(bool? isArchived);

        Task<HabitReadDto?> GetByIdAsync(int id);

        Task<HabitReadDto> CreateAsync(HabitCreateDto dto);

        Task<bool> UpdateAsync(int id, HabitUpdateDto dto);

        Task<bool> DeleteAsync(int id);

        Task<bool> ArchiveAsync(int id);
    }
}
