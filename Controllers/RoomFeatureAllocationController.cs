using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>RoomFeatureAllocation management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RoomFeatureAllocationController : ControllerBase
    {
        private readonly GenericRepository<RoomFeatureAllocation> _repo;

        public RoomFeatureAllocationController(AppDbContext context)
            => _repo = new GenericRepository<RoomFeatureAllocation>(context);

        /// <summary>Get all active RoomFeatureAllocation records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<RoomFeatureAllocation>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get RoomFeatureAllocation by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<RoomFeatureAllocation>.Fail($"RoomFeatureAllocation with Id {id} not found."));
            return Ok(ApiResponse<RoomFeatureAllocation>.Ok(item));
        }

        /// <summary>Create a new RoomFeatureAllocation</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] RoomFeatureAllocationRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<RoomFeatureAllocation>.Fail("Validation failed."));

            var entity = new RoomFeatureAllocation
            {
                RoomId = r.RoomId,
                FeatureId = r.FeatureId,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<RoomFeatureAllocation>.Created(created));
        }

        /// <summary>Update an existing RoomFeatureAllocation</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] RoomFeatureAllocationRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<RoomFeatureAllocation>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.RoomId = r.RoomId;
                entity.FeatureId = r.FeatureId;
            });

            if (updated is null)
                return NotFound(ApiResponse<RoomFeatureAllocation>.Fail($"RoomFeatureAllocation with Id {id} not found."));

            return Ok(ApiResponse<RoomFeatureAllocation>.Updated(updated));
        }

        /// <summary>Soft-delete a RoomFeatureAllocation</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"RoomFeatureAllocation with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"RoomFeatureAllocation deleted successfully."));
        }

        /// <summary>Get total count of active RoomFeatureAllocation records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
