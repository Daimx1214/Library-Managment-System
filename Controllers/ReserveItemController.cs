using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>ReserveItem management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ReserveItemController : ControllerBase
    {
        private readonly GenericRepository<ReserveItem> _repo;

        public ReserveItemController(AppDbContext context)
            => _repo = new GenericRepository<ReserveItem>(context);

        /// <summary>Get all active ReserveItem records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<ReserveItem>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get ReserveItem by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<ReserveItem>.Fail($"ReserveItem with Id {id} not found."));
            return Ok(ApiResponse<ReserveItem>.Ok(item));
        }

        /// <summary>Create a new ReserveItem</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ReserveItemRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<ReserveItem>.Fail("Validation failed."));

            var entity = new ReserveItem
            {
                ItemInfoId = r.ItemInfoId,
                DurationId = r.DurationId,
                NumberDuration = r.NumberDuration,
                LibraryMemberId = r.LibraryMemberId,
                IsAvailable = r.IsAvailable,
                ExpiryDate = r.ExpiryDate,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<ReserveItem>.Created(created));
        }

        /// <summary>Update an existing ReserveItem</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] ReserveItemRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<ReserveItem>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.ItemInfoId = r.ItemInfoId;
                entity.DurationId = r.DurationId;
                entity.NumberDuration = r.NumberDuration;
                entity.LibraryMemberId = r.LibraryMemberId;
                entity.IsAvailable = r.IsAvailable;
                entity.ExpiryDate = r.ExpiryDate;
            });

            if (updated is null)
                return NotFound(ApiResponse<ReserveItem>.Fail($"ReserveItem with Id {id} not found."));

            return Ok(ApiResponse<ReserveItem>.Updated(updated));
        }

        /// <summary>Soft-delete a ReserveItem</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"ReserveItem with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"ReserveItem deleted successfully."));
        }

        /// <summary>Get total count of active ReserveItem records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
