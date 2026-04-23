using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>LibraryShelf management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LibraryShelfController : ControllerBase
    {
        private readonly GenericRepository<LibraryShelf> _repo;

        public LibraryShelfController(AppDbContext context)
            => _repo = new GenericRepository<LibraryShelf>(context);

        /// <summary>Get all active LibraryShelf records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<LibraryShelf>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get LibraryShelf by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<LibraryShelf>.Fail($"LibraryShelf with Id {id} not found."));
            return Ok(ApiResponse<LibraryShelf>.Ok(item));
        }

        /// <summary>Create a new LibraryShelf</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] LibraryShelfRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<LibraryShelf>.Fail("Validation failed."));

            var entity = new LibraryShelf
            {
                Code = r.Code,
                LibraryRackId = r.LibraryRackId,
                MaxCapacity = r.MaxCapacity,
                NoOfItems = r.NoOfItems,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<LibraryShelf>.Created(created));
        }

        /// <summary>Update an existing LibraryShelf</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] LibraryShelfRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<LibraryShelf>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.Code = r.Code;
                entity.LibraryRackId = r.LibraryRackId;
                entity.MaxCapacity = r.MaxCapacity;
                entity.NoOfItems = r.NoOfItems;
            });

            if (updated is null)
                return NotFound(ApiResponse<LibraryShelf>.Fail($"LibraryShelf with Id {id} not found."));

            return Ok(ApiResponse<LibraryShelf>.Updated(updated));
        }

        /// <summary>Soft-delete a LibraryShelf</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"LibraryShelf with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"LibraryShelf deleted successfully."));
        }

        /// <summary>Get total count of active LibraryShelf records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
