using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>RoomType management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RoomTypeController : ControllerBase
    {
        private readonly GenericRepository<RoomType> _repo;

        public RoomTypeController(AppDbContext context)
            => _repo = new GenericRepository<RoomType>(context);

        /// <summary>Get all active RoomType records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<RoomType>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get RoomType by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<RoomType>.Fail($"RoomType with Id {id} not found."));
            return Ok(ApiResponse<RoomType>.Ok(item));
        }

        /// <summary>Create a new RoomType</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] RoomTypeRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<RoomType>.Fail("Validation failed."));

            var entity = new RoomType
            {
                Name = r.Name,
                Description = r.Description,
                Code = r.Code,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<RoomType>.Created(created));
        }

        /// <summary>Update an existing RoomType</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] RoomTypeRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<RoomType>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.Name = r.Name;
                entity.Description = r.Description;
                entity.Code = r.Code;
            });

            if (updated is null)
                return NotFound(ApiResponse<RoomType>.Fail($"RoomType with Id {id} not found."));

            return Ok(ApiResponse<RoomType>.Updated(updated));
        }

        /// <summary>Soft-delete a RoomType</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"RoomType with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"RoomType deleted successfully."));
        }

        /// <summary>Get total count of active RoomType records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
