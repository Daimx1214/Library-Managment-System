using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>FineCategory management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class FineCategoryController : ControllerBase
    {
        private readonly GenericRepository<FineCategory> _repo;

        public FineCategoryController(AppDbContext context)
            => _repo = new GenericRepository<FineCategory>(context);

        /// <summary>Get all active FineCategory records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<FineCategory>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get FineCategory by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<FineCategory>.Fail($"FineCategory with Id {id} not found."));
            return Ok(ApiResponse<FineCategory>.Ok(item));
        }

        /// <summary>Create a new FineCategory</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] FineCategoryRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<FineCategory>.Fail("Validation failed."));

            var entity = new FineCategory
            {
                FineCategoryName = r.FineCategoryName,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<FineCategory>.Created(created));
        }

        /// <summary>Update an existing FineCategory</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] FineCategoryRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<FineCategory>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.FineCategoryName = r.FineCategoryName;
            });

            if (updated is null)
                return NotFound(ApiResponse<FineCategory>.Fail($"FineCategory with Id {id} not found."));

            return Ok(ApiResponse<FineCategory>.Updated(updated));
        }

        /// <summary>Soft-delete a FineCategory</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"FineCategory with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"FineCategory deleted successfully."));
        }

        /// <summary>Get total count of active FineCategory records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
