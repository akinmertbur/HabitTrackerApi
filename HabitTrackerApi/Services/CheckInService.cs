using HabitTrackerApi.Data;
using HabitTrackerApi.DTOs;
using HabitTrackerApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace HabitTrackerApi.Services {
    public class CheckInService : ICheckInService {
        private readonly HabitTrackerDbContext _context;

        public CheckInService(HabitTrackerDbContext context) {
            _context = context;
        }

        public async Task<CheckInReadDto?> AddCheckInAsync(CheckInCreateDto dto) {
            bool exists = await _context.HabitCheckIns
                .AnyAsync(c => c.HabitId == dto.HabitId && c.Date == dto.Date);

            if (exists) {
                return null;
            }
            
            
            var now = DateTime.UtcNow;

            var entity = new HabitCheckIn {
                HabitId = dto.HabitId,
                Date = dto.Date,
                Note = dto.Note?.Trim(),
                CreatedAt = now
            };

            _context.HabitCheckIns.Add(entity);
            await _context.SaveChangesAsync();

            return MapToReadDto(entity);
        }

        public async Task<bool> DeleteCheckInAsync(int id) {
            var entity = await _context.HabitCheckIns.FirstOrDefaultAsync(c => c.Id == id);
            if (entity is null) return false;

            _context.HabitCheckIns.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CheckInReadDto>> GetByHabitIdAsync(int habitId) {
            var items = await _context.HabitCheckIns
                .AsNoTracking()
                .Where(c => c.HabitId == habitId)
                .OrderByDescending(c => c.Date)
                .ToListAsync();

            return items.Select(MapToReadDto);
        }

        private static CheckInReadDto MapToReadDto(HabitCheckIn item) {
            return new CheckInReadDto {
                Id = item.Id,
                HabitId = item.HabitId,
                Date = item.Date,
                Note = item.Note,
                CreatedAt = item.CreatedAt
            };
        }
    }
}
