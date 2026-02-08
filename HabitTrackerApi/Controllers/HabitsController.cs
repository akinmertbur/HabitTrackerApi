using HabitTrackerApi.DTOs;
using HabitTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HabitTrackerApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class HabitsController : ControllerBase {
        private readonly IHabitService _habitService;

        public HabitsController(IHabitService habitService) {
            _habitService = habitService;
        }

        /// <summary>Returns all habits with optional archiving filter.</summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<HabitReadDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<HabitReadDto>>> GetAll(
            [FromQuery] bool? isArchived) {
            var result = await _habitService.GetAllAsync(isArchived);
            return Ok(result);
        }

        /// <summary>Returns a single habit by ID including its check-ins.</summary>
        /// <response code="200">Returns the habit.</response>
        /// <response code="404">Habit not found.</response>
        [HttpGet("{id:int}", Name = "GetHabitById")]
        [ProducesResponseType(typeof(HabitReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<HabitReadDto>> GetById(int id) {
            var item = await _habitService.GetByIdAsync(id);
            if (item is null) return NotFound();

            return Ok(item);
        }

        /// <summary>Creates a new habit.</summary>
        [HttpPost]
        [ProducesResponseType(typeof(HabitReadDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<HabitReadDto>> Create([FromBody] HabitCreateDto dto) {
            var created = await _habitService.CreateAsync(dto);

            return CreatedAtAction(
                "GetHabitById",
                new { id = created.Id },
                created);
        }

        /// <summary>Updates an existing habit.</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] HabitUpdateDto dto) {
            var updated = await _habitService.UpdateAsync(id, dto);
            if (!updated) return NotFound();

            return NoContent();
        }

        /// <summary>Deletes a habit and all associated check-ins.</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id) {
            var deleted = await _habitService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }

        /// <summary>Archives a habit so it no longer appears in the active list.</summary>
        [HttpPatch("{id:int}/archive")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Archive(int id) {
            var archived = await _habitService.ArchiveAsync(id);
            if (!archived) return NotFound();

            return NoContent();
        }
    }
}
