using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>Room management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RoomController : ControllerBase
    {
        private readonly GenericRepository<Room> _repo;

        public RoomController(AppDbContext context)
            => _repo = new GenericRepository<Room>(context);

        /// <summary>Get all active Room records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<Room>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get Room by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<Room>.Fail($"Room with Id {id} not found."));
            return Ok(ApiResponse<Room>.Ok(item));
        }

        /// <summary>Create a new Room</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] RoomRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Room>.Fail("Validation failed."));

            var entity = new Room
            {
                Name = r.Name,
                Description = r.Description,
                Code = r.Code,
                EstablishedIn = r.EstablishedIn,
                FloorId = r.FloorId,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<Room>.Created(created));
        }

        /// <summary>Update an existing Room</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] RoomRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Room>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.Name = r.Name;
                entity.Description = r.Description;
                entity.Code = r.Code;
                entity.EstablishedIn = r.EstablishedIn;
                entity.FloorId = r.FloorId;
            });

            if (updated is null)
                return NotFound(ApiResponse<Room>.Fail($"Room with Id {id} not found."));

            return Ok(ApiResponse<Room>.Updated(updated));
        }

        /// <summary>Soft-delete a Room</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"Room with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"Room deleted successfully."));
        }

        /// <summary>Get total count of active Room records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
