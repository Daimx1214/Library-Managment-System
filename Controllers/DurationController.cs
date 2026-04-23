using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>Duration management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class DurationController : ControllerBase
    {
        private readonly GenericRepository<Duration> _repo;

        public DurationController(AppDbContext context)
            => _repo = new GenericRepository<Duration>(context);

        /// <summary>Get all active Duration records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<Duration>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get Duration by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<Duration>.Fail($"Duration with Id {id} not found."));
            return Ok(ApiResponse<Duration>.Ok(item));
        }

        /// <summary>Create a new Duration</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] DurationRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Duration>.Fail("Validation failed."));

            var entity = new Duration
            {
                Category = r.Category,
                Code = r.Code,
                Description = r.Description,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<Duration>.Created(created));
        }

        /// <summary>Update an existing Duration</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] DurationRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Duration>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.Category = r.Category;
                entity.Code = r.Code;
                entity.Description = r.Description;
            });

            if (updated is null)
                return NotFound(ApiResponse<Duration>.Fail($"Duration with Id {id} not found."));

            return Ok(ApiResponse<Duration>.Updated(updated));
        }

        /// <summary>Soft-delete a Duration</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"Duration with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"Duration deleted successfully."));
        }

        /// <summary>Get total count of active Duration records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
