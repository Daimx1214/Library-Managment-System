using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>RoomTypeAllocation management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RoomTypeAllocationController : ControllerBase
    {
        private readonly GenericRepository<RoomTypeAllocation> _repo;

        public RoomTypeAllocationController(AppDbContext context)
            => _repo = new GenericRepository<RoomTypeAllocation>(context);

        /// <summary>Get all active RoomTypeAllocation records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<RoomTypeAllocation>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get RoomTypeAllocation by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<RoomTypeAllocation>.Fail($"RoomTypeAllocation with Id {id} not found."));
            return Ok(ApiResponse<RoomTypeAllocation>.Ok(item));
        }

        /// <summary>Create a new RoomTypeAllocation</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] RoomTypeAllocationRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<RoomTypeAllocation>.Fail("Validation failed."));

            var entity = new RoomTypeAllocation
            {
                RoomId = r.RoomId,
                RoomTypeId = r.RoomTypeId,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<RoomTypeAllocation>.Created(created));
        }

        /// <summary>Update an existing RoomTypeAllocation</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] RoomTypeAllocationRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<RoomTypeAllocation>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.RoomId = r.RoomId;
                entity.RoomTypeId = r.RoomTypeId;
            });

            if (updated is null)
                return NotFound(ApiResponse<RoomTypeAllocation>.Fail($"RoomTypeAllocation with Id {id} not found."));

            return Ok(ApiResponse<RoomTypeAllocation>.Updated(updated));
        }

        /// <summary>Soft-delete a RoomTypeAllocation</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"RoomTypeAllocation with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"RoomTypeAllocation deleted successfully."));
        }

        /// <summary>Get total count of active RoomTypeAllocation records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
