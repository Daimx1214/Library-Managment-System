using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>ItemCategory management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ItemCategoryController : ControllerBase
    {
        private readonly GenericRepository<ItemCategory> _repo;

        public ItemCategoryController(AppDbContext context)
            => _repo = new GenericRepository<ItemCategory>(context);

        /// <summary>Get all active ItemCategory records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<ItemCategory>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get ItemCategory by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<ItemCategory>.Fail($"ItemCategory with Id {id} not found."));
            return Ok(ApiResponse<ItemCategory>.Ok(item));
        }

        /// <summary>Create a new ItemCategory</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ItemCategoryRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<ItemCategory>.Fail("Validation failed."));

            var entity = new ItemCategory
            {
                ItemCategoryName = r.ItemCategoryName,
                ParentId = r.ParentId,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<ItemCategory>.Created(created));
        }

        /// <summary>Update an existing ItemCategory</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] ItemCategoryRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<ItemCategory>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.ItemCategoryName = r.ItemCategoryName;
                entity.ParentId = r.ParentId;
            });

            if (updated is null)
                return NotFound(ApiResponse<ItemCategory>.Fail($"ItemCategory with Id {id} not found."));

            return Ok(ApiResponse<ItemCategory>.Updated(updated));
        }

        /// <summary>Soft-delete a ItemCategory</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"ItemCategory with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"ItemCategory deleted successfully."));
        }

        /// <summary>Get total count of active ItemCategory records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
