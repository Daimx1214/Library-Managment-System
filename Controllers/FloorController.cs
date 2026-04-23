using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>Floor management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class FloorController : ControllerBase
    {
        private readonly GenericRepository<Floor> _repo;

        public FloorController(AppDbContext context)
            => _repo = new GenericRepository<Floor>(context);

        /// <summary>Get all active Floor records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<Floor>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get Floor by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<Floor>.Fail($"Floor with Id {id} not found."));
            return Ok(ApiResponse<Floor>.Ok(item));
        }

        /// <summary>Create a new Floor</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] FloorRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Floor>.Fail("Validation failed."));

            var entity = new Floor
            {
                Name = r.Name,
                Code = r.Code,
                BuildingId = r.BuildingId,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<Floor>.Created(created));
        }

        /// <summary>Update an existing Floor</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] FloorRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Floor>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.Name = r.Name;
                entity.Code = r.Code;
                entity.BuildingId = r.BuildingId;
            });

            if (updated is null)
                return NotFound(ApiResponse<Floor>.Fail($"Floor with Id {id} not found."));

            return Ok(ApiResponse<Floor>.Updated(updated));
        }

        /// <summary>Soft-delete a Floor</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"Floor with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"Floor deleted successfully."));
        }

        /// <summary>Get total count of active Floor records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
