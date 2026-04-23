using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>ItemCondition management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ItemConditionController : ControllerBase
    {
        private readonly GenericRepository<ItemCondition> _repo;

        public ItemConditionController(AppDbContext context)
            => _repo = new GenericRepository<ItemCondition>(context);

        /// <summary>Get all active ItemCondition records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<ItemCondition>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get ItemCondition by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<ItemCondition>.Fail($"ItemCondition with Id {id} not found."));
            return Ok(ApiResponse<ItemCondition>.Ok(item));
        }

        /// <summary>Create a new ItemCondition</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ItemConditionRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<ItemCondition>.Fail("Validation failed."));

            var entity = new ItemCondition
            {
                ItemId = r.ItemId,
                ItemCopyId = r.ItemCopyId,
                ItemCategoryId = r.ItemCategoryId,
                ConditionDescription = r.ConditionDescription,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<ItemCondition>.Created(created));
        }

        /// <summary>Update an existing ItemCondition</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] ItemConditionRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<ItemCondition>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.ItemId = r.ItemId;
                entity.ItemCopyId = r.ItemCopyId;
                entity.ItemCategoryId = r.ItemCategoryId;
                entity.ConditionDescription = r.ConditionDescription;
            });

            if (updated is null)
                return NotFound(ApiResponse<ItemCondition>.Fail($"ItemCondition with Id {id} not found."));

            return Ok(ApiResponse<ItemCondition>.Updated(updated));
        }

        /// <summary>Soft-delete a ItemCondition</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"ItemCondition with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"ItemCondition deleted successfully."));
        }

        /// <summary>Get total count of active ItemCondition records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
