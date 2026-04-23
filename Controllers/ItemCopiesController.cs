using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>ItemCopies management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ItemCopiesController : ControllerBase
    {
        private readonly GenericRepository<ItemCopies> _repo;

        public ItemCopiesController(AppDbContext context)
            => _repo = new GenericRepository<ItemCopies>(context);

        /// <summary>Get all active ItemCopies records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<ItemCopies>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get ItemCopies by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<ItemCopies>.Fail($"ItemCopies with Id {id} not found."));
            return Ok(ApiResponse<ItemCopies>.Ok(item));
        }

        /// <summary>Create a new ItemCopies</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ItemCopiesRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<ItemCopies>.Fail("Validation failed."));

            var entity = new ItemCopies
            {
                ItemCopyId = r.ItemCopyId,
                EdititonId = r.EdititonId,
                NoOfCopies = r.NoOfCopies,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<ItemCopies>.Created(created));
        }

        /// <summary>Update an existing ItemCopies</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] ItemCopiesRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<ItemCopies>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.ItemCopyId = r.ItemCopyId;
                entity.EdititonId = r.EdititonId;
                entity.NoOfCopies = r.NoOfCopies;
            });

            if (updated is null)
                return NotFound(ApiResponse<ItemCopies>.Fail($"ItemCopies with Id {id} not found."));

            return Ok(ApiResponse<ItemCopies>.Updated(updated));
        }

        /// <summary>Soft-delete a ItemCopies</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"ItemCopies with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"ItemCopies deleted successfully."));
        }

        /// <summary>Get total count of active ItemCopies records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
