using HabitTrackerApi.Data;
using HabitTrackerApi.DTOs;
using HabitTrackerApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace HabitTrackerApi.Services {
    public class HabitService : IHabitService {
        private readonly HabitTrackerDbContext _context;

        public HabitService(HabitTrackerDbContext context) {
            _context = context;
        }

        public async Task<IEnumerable<HabitReadDto>> GetAllAsync(bool? isArchived) {
            IQueryable<HabitTrackerItem> query = _context.HabitTrackerItems
                .Include(h => h.CheckIns)
                .AsNoTracking();

            if (isArchived.HasValue) {
                query = query.Where(t => t.IsArchived == isArchived.Value);
            }

            query = query
                .OrderByDescending(t => t.CreatedAt);

            var items = await query.ToListAsync();

            return items.Select(MapToReadDto);
        }

        public async Task<HabitReadDto?> GetByIdAsync(int id) {
            var item = await _context.HabitTrackerItems
                .Include(h => h.CheckIns)
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            return item is null ? null : MapToReadDto(item);
        }

        public async Task<HabitReadDto> CreateAsync(HabitCreateDto dto) {
            var now = DateTime.UtcNow;

            var entity = new HabitTrackerItem {
                Name = dto.Name.Trim(),
                Description = dto.Description?.Trim(),
                TargetPerWeek = dto.TargetPerWeek,
                IsArchived = false,
                CreatedAt = now,
                UpdatedAt = now
            };

            _context.HabitTrackerItems.Add(entity);
            await _context.SaveChangesAsync();

            return MapToReadDto(entity);
        }

        public async Task<bool> UpdateAsync(int id, HabitUpdateDto dto) {
            var entity = await _context.HabitTrackerItems.FirstOrDefaultAsync(t => t.Id == id);
            if (entity is null) return false;

            entity.Name = dto.Name.Trim();
            entity.Description = dto.Description?.Trim();
            entity.TargetPerWeek = dto.TargetPerWeek;
            entity.IsArchived = dto.IsArchived;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id) {
            var entity = await _context.HabitTrackerItems.FirstOrDefaultAsync(t => t.Id == id);
            if (entity is null) return false;

            _context.HabitTrackerItems.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ArchiveAsync(int id) {
            var entity = await _context.HabitTrackerItems.FirstOrDefaultAsync(t => t.Id == id);
            if (entity is null) return false;

            if (!entity.IsArchived) {
                entity.IsArchived = true;
                entity.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }

            return true;
        }

        private static HabitReadDto MapToReadDto(HabitTrackerItem item) {
            return new HabitReadDto {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                TargetPerWeek = item.TargetPerWeek,
                IsArchived = item.IsArchived,
                CreatedAt = item.CreatedAt,
                UpdatedAt = item.UpdatedAt,
                CheckIns = item.CheckIns.Select(c => new CheckInReadDto {
                    Id = c.Id,
                    HabitId = c.HabitId,
                    Date = c.Date,
                    Note = c.Note,
                    CreatedAt = c.CreatedAt
                }).ToList()
            };
        }
    }
}
