using HabitTrackerApi.DTOs;
using HabitTrackerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace HabitTrackerApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class CheckInsController : ControllerBase {
        private readonly ICheckInService _checkInService;

        public CheckInsController(ICheckInService checkInService) {
            _checkInService = checkInService;
        }

        /// <summary>Logs a completion for a habit on a specific date.</summary>
        /// <response code="201">Check-in successful.</response>
        /// <response code="409">User already checked in for this date.</response>
        [HttpPost]
        [ProducesResponseType(typeof(CheckInReadDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CheckInReadDto>> AddCheckIn([FromBody] CheckInCreateDto dto) {
            var added = await _checkInService.AddCheckInAsync(dto);

            // Fix: Handle the case where the check-in already exists
            if (added is null) {
                return Conflict(new { message = "You have already checked in for this habit on this date." });
            }

            return CreatedAtAction(
                nameof(GetByHabitId),
                new { habitId = added.HabitId },
                added);
        }

        /// <summary>Deletes a specific check-in record.</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCheckIn(int id) {
            var deleted = await _checkInService.DeleteCheckInAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }

        /// <summary>Returns all check-in history for a specific habit.</summary>
        [HttpGet("habit/{habitId:int}")]
        [ProducesResponseType(typeof(IEnumerable<CheckInReadDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CheckInReadDto>>> GetByHabitId(int habitId) {
            var item = await _checkInService.GetByHabitIdAsync(habitId);
            if (item is null) return NotFound();

            return Ok(item);
        }
    }
}
