using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>LibraryRacks management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LibraryRacksController : ControllerBase
    {
        private readonly GenericRepository<LibraryRacks> _repo;

        public LibraryRacksController(AppDbContext context)
            => _repo = new GenericRepository<LibraryRacks>(context);

        /// <summary>Get all active LibraryRacks records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<LibraryRacks>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get LibraryRacks by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<LibraryRacks>.Fail($"LibraryRacks with Id {id} not found."));
            return Ok(ApiResponse<LibraryRacks>.Ok(item));
        }

        /// <summary>Create a new LibraryRacks</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] LibraryRacksRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<LibraryRacks>.Fail("Validation failed."));

            var entity = new LibraryRacks
            {
                Code = r.Code,
                Description = r.Description,
                LibrarySectionId = r.LibrarySectionId,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<LibraryRacks>.Created(created));
        }

        /// <summary>Update an existing LibraryRacks</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] LibraryRacksRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<LibraryRacks>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.Code = r.Code;
                entity.Description = r.Description;
                entity.LibrarySectionId = r.LibrarySectionId;
            });

            if (updated is null)
                return NotFound(ApiResponse<LibraryRacks>.Fail($"LibraryRacks with Id {id} not found."));

            return Ok(ApiResponse<LibraryRacks>.Updated(updated));
        }

        /// <summary>Soft-delete a LibraryRacks</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"LibraryRacks with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"LibraryRacks deleted successfully."));
        }

        /// <summary>Get total count of active LibraryRacks records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
